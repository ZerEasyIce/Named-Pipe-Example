using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Named_Pipe_Example
{
    public class clsNamedPipe : IDisposable
    {
        private string _pipeName;
        private bool _isServer;
        private NamedPipeServerStream _serverStream;
        private NamedPipeClientStream _clientStream;
        private CancellationTokenSource _cts;
        private Task _maintainConnectionTask;

        // เพิ่มตัวแปรสถานะเชื่อมต่อ
        private bool _isConnected = false;
        public bool IsConnected => _isConnected;

        public event Action<PipeData> OnDataReceived;

        private bool _isSending = false; // เพิ่ม flag ควบคุมการส่งข้อมูล

        public clsNamedPipe(string pipeName, bool isServer)
        {
            _pipeName = pipeName;
            _isServer = isServer;
            _cts = new CancellationTokenSource();
        }

        public async Task StartAsync()
        {
            _maintainConnectionTask = MaintainConnectionAsync();
            await _maintainConnectionTask;
        }

        private async Task MaintainConnectionAsync()
        {
            while (_cts != null && !_cts.Token.IsCancellationRequested)
            {
                try
                {
                    await ConnectAsync();

                    Debug.WriteLine($"{(_isServer ? "Server" : "Client")} connected on pipe {_pipeName}");
                    _isConnected = true;  // อัพเดตสถานะเชื่อมต่อ

                    await HandleCommunicationAsync();

                    Debug.WriteLine("Connection lost, will try to reconnect...");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error: {ex.Message}. Reconnecting...");
                }

                _isConnected = false;  // ตัดการเชื่อมต่อ อัพเดตสถานะ

                if (_cts == null) break;

                try
                {
                    await Task.Delay(1000, _cts.Token);
                }
                catch (OperationCanceledException)
                {
                    break;
                }
            }
        }

        private async Task ConnectAsync(int timeoutMilliseconds = 5000)
        {
            if (_isServer)
            {
                // สร้าง server pipe พร้อม PipeTransmissionMode.Message
                _serverStream?.Dispose();
                _serverStream = new NamedPipeServerStream(_pipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances,
                    PipeTransmissionMode.Message, PipeOptions.Asynchronous);

                while (!_cts.Token.IsCancellationRequested)
                {
                    var connectTask = _serverStream.WaitForConnectionAsync(_cts.Token);
                    if (await Task.WhenAny(connectTask, Task.Delay(timeoutMilliseconds, _cts.Token)) == connectTask)
                    {
                        break; // connected
                    }
                    else
                    {
                        Debug.WriteLine("Timeout waiting for client connection. Retrying...");
                        await Task.Delay(1000, _cts.Token);
                    }
                }
            }
            else
            {
                _clientStream?.Dispose();
                _clientStream = new NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous);

                while (!_cts.Token.IsCancellationRequested)
                {
                    try
                    {
                        await _clientStream.ConnectAsync(timeoutMilliseconds, _cts.Token);

                        // ตั้ง ReadMode เป็น Message หลังเชื่อมต่อ
                        _clientStream.ReadMode = PipeTransmissionMode.Message;

                        break; // connected
                    }
                    catch (OperationCanceledException)
                    {
                        throw;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Client connect failed: {ex.Message}. Retrying...");
                        await Task.Delay(1000, _cts.Token);
                    }
                }
            }
        }

        private async Task HandleCommunicationAsync()
        {
            var stream = _isServer ? (System.IO.Stream)_serverStream : _clientStream;

            var buffer = new byte[4096];
            var messageBuffer = new List<byte>();

            while (!_cts.Token.IsCancellationRequested && stream.IsConnected())
            {
                try
                {
                    int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length, _cts.Token);
                    if (bytesRead == 0)
                    {
                        // Connection closed
                        break;
                    }

                    messageBuffer.AddRange(buffer.Take(bytesRead));

                    bool isComplete = false;
                    if (stream is NamedPipeServerStream serverStream)
                    {
                        isComplete = serverStream.IsMessageComplete;
                    }
                    else if (stream is NamedPipeClientStream clientStream)
                    {
                        isComplete = clientStream.IsMessageComplete;
                    }

                    if (isComplete)
                    {
                        var data = DeserializePipeData(messageBuffer.ToArray(), messageBuffer.Count);
                        messageBuffer.Clear();

                        OnDataReceived?.Invoke(data);
                    }
                }
                catch (OperationCanceledException)
                {
                    break;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Communication error: {ex.Message}");
                    break;
                }
            }
        }


        public async Task<bool> SendAsync(PipeData data)
        {
            if (_isSending)
            {
                Debug.WriteLine("SendAsync skipped: already sending.");
                return false;
            }

            var stream = _isServer ? (System.IO.Stream)_serverStream : _clientStream;
            if (stream == null || !stream.IsConnected())
            {
                Debug.WriteLine("SendAsync failed: pipe is not connected.");
                return false;
            }

            try
            {
                _isSending = true;

                var bytes = SerializePipeData(data);
                await stream.WriteAsync(bytes, 0, bytes.Length, _cts.Token);
                await stream.FlushAsync(_cts.Token);

                return true;
            }
            catch (IOException ioEx)
            {
                Debug.WriteLine($"SendAsync IOException: {ioEx.Message}");
                // อาจเพิ่ม logic reconnect ที่นี่ถ้าต้องการ
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"SendAsync error: {ex.Message}");
                return false;
            }
            finally
            {
                _isSending = false;
            }
        }

        // แปลง PipeData เป็น byte[]
        private byte[] SerializePipeData(PipeData data)
        {
            var json = JsonConvert.SerializeObject(data);
            return Encoding.UTF8.GetBytes(json);
        }

        // แปลง byte[] กลับเป็น PipeData
        private PipeData DeserializePipeData(byte[] buffer, int length)
        {
            var json = Encoding.UTF8.GetString(buffer, 0, length);
            return JsonConvert.DeserializeObject<PipeData>(json);
        }

        public void Dispose()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
            _serverStream?.Dispose();
            _serverStream = null;

            _clientStream?.Dispose();
            _clientStream = null;
        }
    }

    // สมมุติ class ข้อมูล PipeData
    public class PipeData
    {
        // กำหนด properties ตามต้องการ
        public int pStatus { get; set; }
        public int pStep { get; set; }
    }

    // Extension method เช็คว่า stream ยังเชื่อมต่ออยู่ไหม
    public static class StreamExtensions
    {
        public static bool IsConnected(this System.IO.Stream stream)
        {
            if (stream is NamedPipeServerStream serverStream)
            {
                return serverStream.IsConnected;
            }
            else if (stream is NamedPipeClientStream clientStream)
            {
                return clientStream.IsConnected;
            }
            return false;
        }
    }
}
