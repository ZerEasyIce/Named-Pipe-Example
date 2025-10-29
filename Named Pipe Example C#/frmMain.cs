using Named_Pipe_Example.modX;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Named_Pipe_Example.modX.modGlobal;

namespace Named_Pipe_Example
{
    public partial class frmMain : Form
    {
        private clsNamedPipe pipeHelper;
        private System.Windows.Forms.Timer timerProcess;
        private System.Windows.Forms.Timer DoProcess;
        private string chkType = QType.qInbound;

        public frmMain()
        {
            InitializeComponent();
            this.Text = modGlobal.confDevTitleName;
        }

        private int iStatus = 1;
        private int iStep = 0;
        private int ReceivediStatus;
        private int ReceivediStep;

        private bool _isSending = false;

        private async void TimerNamedPipe_Tick(object sender, EventArgs e)
        {
            try
            {
                if (pipeHelper == null || _isSending) return;

                tsslPipeStatus_Value.Text = pipeHelper.IsConnected ? "Connected" : "Not Connected";

                if (modGlobal.confDevDualMain == 1 && pipeHelper.IsConnected)
                {
                    var data = new PipeData { pStatus = iStatus, pStep = iStep };

                    try
                    {
                        _isSending = true;
                        bool sent = await pipeHelper.SendAsync(data);
                        if (!sent)
                        {
                            Debug.WriteLine("Data not sent: pipe busy or disconnected.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"SendAsync error: {ex.Message}");
                    }
                    finally
                    {
                        _isSending = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"TimerNamedPipe_Tick error: {ex.Message}");
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (modGlobal.confDevDualMain == 1)
                pipeHelper = new clsNamedPipe(modGlobal.confDevPipeName, isServer: true);
            else
                pipeHelper = new clsNamedPipe(modGlobal.confDevPipeName, isServer: false);

            pipeHelper.OnDataReceived += PipeHelper_OnDataReceived;

            _ = pipeHelper.StartAsync();

            timerProcess = new System.Windows.Forms.Timer();
            timerProcess.Interval = modGlobal.confTimerProcess;
            timerProcess.Tick += TimerNamedPipe_Tick;
            timerProcess.Start();

            DoProcess = new System.Windows.Forms.Timer();
            DoProcess.Interval = modGlobal.confTimerProcess;
            DoProcess.Tick += DoProcess_Tick;
            DoProcess.Start();

            lblCurrentStatus_Value.Text = iStatus.ToString();
            lblCurrentStep_Value.Text = iStep.ToString();
        }

        private async void SetCase(int step)
        {
            iStep = step;
            lblCurrentStep_Value.Text = iStep.ToString();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            iStatus = 98;
            iStep = 1;
            lblCurrentStatus_Value.Text = iStatus.ToString();
            lblCurrentStep_Value.Text = iStep.ToString();
        }

        private async void DoProcess_Tick(object sender, EventArgs e)
        {
            try
            {
                switch (chkType)
                {
                    case QType.qInbound:
                        switch (iStep)
                        {
                            case 1:
                                await Task.Delay(900);
                                SetCase(step: 2);
                                break;
                            case 2:
                                await Task.Delay(900);
                                SetCase(step: 3);
                                break;
                            case 3:
                                await Task.Delay(900);
                                SetCase(step: 4);
                                break;
                            case 4:
                                await Task.Delay(900);
                                SetCase(step: 5);
                                break;
                            case 5:
                                await Task.Delay(900);
                                SetCase(step: 6);
                                break;
                            case 6:
                                if (modGlobal.confDevDualMain == 2 && pipeHelper.IsConnected && ReceivediStatus != 1 && ReceivediStep <= 8)
                                {
                                    // รอจนกว่า G02 จะ Step > 8
                                    return;
                                }
                                else
                                {
                                    await Task.Delay(900);
                                    SetCase(step: 7);
                                }
                                break;
                            case 7:
                                await Task.Delay(900);
                                SetCase(step: 8);
                                break;
                            case 8:
                                await Task.Delay(900);
                                SetCase(step: 9);
                                break;
                            case 9:
                                await Task.Delay(900);
                                SetCase(step: 10);
                                break;
                            case 10:
                                await Task.Delay(900);
                                SetCase(step: 11);
                                break;
                            case 11:
                                await Task.Delay(900);
                                SetCase(step: 12);
                                break;
                            case 12:
                                await Task.Delay(900);
                                SetCase(step: 13);
                                break;
                            case 13:
                                await Task.Delay(900);
                                SetCase(step: 0);
                                iStatus = 1;
                                lblCurrentStatus_Value.Text = iStatus.ToString();
                                break;
                        }
                        break;
                    // case อื่นๆ ของ chkType
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"DoProcess_Tick error: {ex.Message}");
            }
        }

        private void PipeHelper_OnDataReceived(PipeData data)
        {
            // ตัวอย่าง: อัพเดต UI ต้อง invoke เพราะมาจาก thread อื่น
            if (InvokeRequired)
            {
                Invoke(new Action(() => PipeHelper_OnDataReceived(data)));
                return;
            }

            if (pipeHelper.IsConnected)
            {
                ReceivediStatus = data.pStatus;
                ReceivediStep = data.pStep;

                lblPipeStatus_Value.Text = data.pStatus.ToString();
                lblPipeStep_Value.Text = data.pStep.ToString();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            pipeHelper?.Dispose();
        }
    }

}
