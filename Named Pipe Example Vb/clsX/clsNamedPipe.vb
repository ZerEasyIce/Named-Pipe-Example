Imports System.IO
Imports System.IO.Pipes
Imports System.Text
Imports System.Threading
Imports Newtonsoft.Json

Namespace Named_Pipe_Example

    Public Class NamedPipeHelper
        Implements IDisposable

        Private _pipeName As String
        Private _isServer As Boolean
        Private _serverStream As NamedPipeServerStream
        Private _clientStream As NamedPipeClientStream
        Private _cts As CancellationTokenSource
        Private _maintainConnectionTask As Task

        ' ตัวแปรสถานะเชื่อมต่อ
        Private _isConnected As Boolean = False
        Public ReadOnly Property IsConnected As Boolean
            Get
                Return _isConnected
            End Get
        End Property

        Public Event OnDataReceived As Action(Of PipeData)

        ' เพิ่ม flag ควบคุมการส่งข้อมูลซ้อนกัน
        Private _isSending As Boolean = False

        Public Sub New(pipeName As String, isServer As Boolean)
            _pipeName = pipeName
            _isServer = isServer
            _cts = New CancellationTokenSource()
        End Sub

        Public Async Function StartAsync() As Task
            _maintainConnectionTask = MaintainConnectionAsync()
            Await _maintainConnectionTask
        End Function

        Private Async Function MaintainConnectionAsync() As Task
            While _cts IsNot Nothing AndAlso Not _cts.Token.IsCancellationRequested
                Try
                    Await ConnectAsync()

                    Debug.WriteLine($"{If(_isServer, "Server", "Client")} connected on pipe {_pipeName}")
                    _isConnected = True ' อัพเดตสถานะเชื่อมต่อ

                    Await HandleCommunicationAsync()

                    Debug.WriteLine("Connection lost, will try to reconnect...")
                Catch ex As Exception
                    Debug.WriteLine($"Error: {ex.Message}. Reconnecting...")
                End Try

                _isConnected = False ' ตัดการเชื่อมต่อ อัพเดตสถานะ

                If _cts Is Nothing Then Exit While

                Try
                    Await Task.Delay(1000, _cts.Token)
                Catch ex As OperationCanceledException
                    Exit While
                End Try
            End While
        End Function

        Private Async Function ConnectAsync(Optional timeoutMilliseconds As Integer = 5000) As Task
            If _isServer Then
                If _serverStream IsNot Nothing Then
                    _serverStream.Dispose()
                End If

                _serverStream = New NamedPipeServerStream(_pipeName, PipeDirection.InOut, NamedPipeServerStream.MaxAllowedServerInstances,
                                                          PipeTransmissionMode.Message, PipeOptions.Asynchronous)

                While Not _cts.Token.IsCancellationRequested
                    Dim connectTask = _serverStream.WaitForConnectionAsync(_cts.Token)
                    Dim completedTask = Await Task.WhenAny(connectTask, Task.Delay(timeoutMilliseconds, _cts.Token))
                    If completedTask Is connectTask Then
                        Exit While
                    Else
                        Debug.WriteLine("Timeout waiting for client connection. Retrying...")
                        Await Task.Delay(1000, _cts.Token)
                    End If
                End While
            Else
                If _clientStream IsNot Nothing Then
                    _clientStream.Dispose()
                End If

                _clientStream = New NamedPipeClientStream(".", _pipeName, PipeDirection.InOut, PipeOptions.Asynchronous)

                While Not _cts.Token.IsCancellationRequested
                    Try
                        Await _clientStream.ConnectAsync(timeoutMilliseconds, _cts.Token)

                        ' ตั้ง ReadMode เป็น Message หลังเชื่อมต่อ
                        _clientStream.ReadMode = PipeTransmissionMode.Message

                        Exit While
                    Catch ex As OperationCanceledException
                        Throw
                    Catch ex As Exception
                        Debug.WriteLine($"Client connect failed: {ex.Message}. Retrying...")
                    End Try
                    Await Task.Delay(1000, _cts.Token)
                End While
            End If
        End Function

        Private Async Function HandleCommunicationAsync() As Task
            Dim stream As IO.Stream = If(_isServer, CType(_serverStream, IO.Stream), _clientStream)

            Dim buffer(4095) As Byte
            Dim messageBuffer As New List(Of Byte)

            While Not _cts.Token.IsCancellationRequested AndAlso stream.IsConnected()
                Try
                    Dim bytesRead As Integer = Await stream.ReadAsync(buffer, 0, buffer.Length, _cts.Token)
                    If bytesRead = 0 Then
                        Exit While
                    End If

                    messageBuffer.AddRange(buffer.Take(bytesRead))

                    Dim pipeStreamServer = TryCast(stream, NamedPipeServerStream)
                    Dim pipeStreamClient = TryCast(stream, NamedPipeClientStream)

                    Dim isComplete As Boolean = False
                    If pipeStreamServer IsNot Nothing Then
                        isComplete = pipeStreamServer.IsMessageComplete
                    ElseIf pipeStreamClient IsNot Nothing Then
                        isComplete = pipeStreamClient.IsMessageComplete
                    End If

                    If isComplete Then
                        Dim data As PipeData = DeserializePipeData(messageBuffer.ToArray(), messageBuffer.Count)
                        messageBuffer.Clear()

                        RaiseEvent OnDataReceived(data)
                    End If

                Catch ex As OperationCanceledException
                    Exit While
                Catch ex As Exception
                    Debug.WriteLine($"Communication error: {ex.Message}")
                    Exit While
                End Try
            End While
        End Function

        ' ปรับ SendAsync ให้คืนค่า Boolean และป้องกันการส่งซ้อน
        Public Async Function SendAsync(data As PipeData) As Task(Of Boolean)
            If _isSending Then
                Debug.WriteLine("SendAsync skipped: already sending.")
                Return False
            End If

            Dim stream As IO.Stream = If(_isServer, CType(_serverStream, IO.Stream), _clientStream)
            If stream Is Nothing OrElse Not stream.IsConnected() Then
                Debug.WriteLine("SendAsync failed: pipe is not connected.")
                Return False
            End If

            Try
                _isSending = True

                Dim bytes() As Byte = SerializePipeData(data)

                Await stream.WriteAsync(bytes, 0, bytes.Length, _cts.Token)
                Await stream.FlushAsync(_cts.Token)

                Return True
            Catch ex As IOException
                Debug.WriteLine($"SendAsync IOException: {ex.Message}")
                Return False
            Catch ex As Exception
                Debug.WriteLine($"SendAsync error: {ex.Message}")
                Return False
            Finally
                _isSending = False
            End Try
        End Function

        ' แปลง PipeData เป็น byte[]
        Private Function SerializePipeData(data As PipeData) As Byte()
            Dim json As String = JsonConvert.SerializeObject(data)
            Return Encoding.UTF8.GetBytes(json)
        End Function

        ' แปลง byte[] กลับเป็น PipeData
        Private Function DeserializePipeData(buffer As Byte(), length As Integer) As PipeData
            Dim json As String = Encoding.UTF8.GetString(buffer, 0, length)
            Return JsonConvert.DeserializeObject(Of PipeData)(json)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            If _cts IsNot Nothing Then
                _cts.Cancel()
                ' ไม่รอ _maintainConnectionTask เพื่อป้องกันโปรแกรมค้าง
                _cts.Dispose()
                _cts = Nothing
            End If
            If _serverStream IsNot Nothing Then
                _serverStream.Dispose()
                _serverStream = Nothing
            End If
            If _clientStream IsNot Nothing Then
                _clientStream.Dispose()
                _clientStream = Nothing
            End If
        End Sub

    End Class

    ' สมมุติ class ข้อมูล PipeData
    Public Class PipeData
        Public Property pStatus As Integer
        Public Property pStep As Integer
    End Class

    ' Extension method เช็คว่า stream ยังเชื่อมต่ออยู่ไหม
    Public Module StreamExtensions
        <System.Runtime.CompilerServices.Extension()>
        Public Function IsConnected(stream As IO.Stream) As Boolean
            Dim serverStream = TryCast(stream, NamedPipeServerStream)
            If serverStream IsNot Nothing Then
                Return serverStream.IsConnected
            End If

            Dim clientStream = TryCast(stream, NamedPipeClientStream)
            If clientStream IsNot Nothing Then
                Return clientStream.IsConnected
            End If

            Return False
        End Function
    End Module

End Namespace