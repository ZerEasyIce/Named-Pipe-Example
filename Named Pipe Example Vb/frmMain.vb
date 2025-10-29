Imports Named_Pipe_Example_Vb.Named_Pipe_Example

Public Class frmMain

    Private pipeHelper As clsNamedPipe
    Private timerProcess As System.Windows.Forms.Timer
    Private DoProcess As System.Windows.Forms.Timer
    Private chkType As String = QType.qInbound

    Private iStatus As Integer = 1
    Private iStep As Integer = 0
    Private ReceivediStatus As Integer
    Private ReceivediStep As Integer
    Private _isSending As Boolean = False

    'Constructor
    Public Sub New()
        ' โค้ดที่ต้องการให้ทำงานตอนสร้างอ็อบเจกต์
        InitializeComponent() ' ตัวนี้ต้องเรียกเพื่อโหลด UI ของฟอร์ม
        Me.Text = modGlobal.confDevTitleName
    End Sub

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeNamedPipe()

        DoProcess = New System.Windows.Forms.Timer()
        DoProcess.Interval = modGlobal.confTimerProcess
        AddHandler DoProcess.Tick, AddressOf DoProcess_Tick
        DoProcess.Start()

        lblCurrentStatus_Value.Text = iStatus.ToString()
        lblCurrentStep_Value.Text = iStep.ToString()

    End Sub

    Private Sub InitializeNamedPipe()
        If modGlobal.confDevDualMain = 1 Then
            pipeHelper = New clsNamedPipe(modGlobal.confDevPipeName, isServer:=True)
        Else
            pipeHelper = New clsNamedPipe(modGlobal.confDevPipeName, isServer:=False)
        End If

        AddHandler pipeHelper.OnDataReceived, Sub(data)
                                                  ' อัพเดต UI ต้อง invoke เพราะมาจาก thread อื่น
                                                  If InvokeRequired Then
                                                      Invoke(New Action(Sub()
                                                                            ReceivediStatus = data.pStatus
                                                                            ReceivediStep = data.pStep

                                                                            lblPipeStatus_Value.Text = data.pStatus.ToString()
                                                                            lblPipeStep_Value.Text = data.pStep.ToString()
                                                                        End Sub))
                                                  Else
                                                      ReceivediStatus = data.pStatus
                                                      ReceivediStep = data.pStep

                                                      lblPipeStatus_Value.Text = data.pStatus.ToString()
                                                      lblPipeStep_Value.Text = data.pStep.ToString()
                                                  End If
                                              End Sub

        ' เรียก StartAsync แบบไม่ต้องรอผล (fire and forget)
        Dim startTask As Task = pipeHelper.StartAsync()

        timerProcess = New System.Windows.Forms.Timer()
        timerProcess.Interval = modGlobal.confTimerProcess

        AddHandler timerProcess.Tick, Async Sub(sender As Object, e As EventArgs)
                                          Try
                                              If pipeHelper Is Nothing OrElse _isSending Then Return

                                              ' อัพเดตสถานะการเชื่อมต่อบน UI
                                              If InvokeRequired Then
                                                  Invoke(New Action(Sub()
                                                                        tsslPipeStatus_Value.Text = If(pipeHelper.IsConnected, "Connected", "Not Connected")
                                                                    End Sub))
                                              Else
                                                  tsslPipeStatus_Value.Text = If(pipeHelper.IsConnected, "Connected", "Not Connected")
                                              End If

                                              If modGlobal.confDevDualMain = 1 AndAlso pipeHelper.IsConnected Then
                                                  Dim data As New PipeData With {.pStatus = iStatus, .pStep = iStep}

                                                  Try
                                                      _isSending = True
                                                      Dim sent As Boolean = Await pipeHelper.SendAsync(data)
                                                      If Not sent Then
                                                          Debug.WriteLine("Data not sent: pipe busy or disconnected.")
                                                      End If
                                                  Catch ex As Exception
                                                      Debug.WriteLine($"SendAsync error: {ex.Message}")
                                                  Finally
                                                      _isSending = False
                                                  End Try
                                              End If
                                          Catch ex As Exception
                                              Debug.WriteLine($"TimerNamedPipe_Tick error: {ex.Message}")
                                          End Try
                                      End Sub
        timerProcess.Start()
    End Sub

    'Private Sub PipeHelper_OnDataReceived(data As PipeData)
    '    ' อัพเดต UI ต้อง Invoke เพราะมาจาก thread อื่น
    '    If Me.InvokeRequired Then
    '        Me.Invoke(New Action(Sub() PipeHelper_OnDataReceived(data)))
    '        Return
    '    End If

    '    If pipeHelper IsNot Nothing AndAlso pipeHelper.IsConnected Then
    '        ReceivediStatus = data.pStatus
    '        ReceivediStep = data.pStep

    '        lblPipeStatus_Value.Text = data.pStatus.ToString()
    '        lblPipeStep_Value.Text = data.pStep.ToString()
    '    End If
    'End Sub

    'Private Async Sub TimerNamedPipe_Tick(sender As Object, e As EventArgs)
    '    Try
    '        tsslPipeStatus_Value.Text = If(pipeHelper IsNot Nothing AndAlso pipeHelper.IsConnected, "Connected", "Not Connected")

    '        If pipeHelper Is Nothing OrElse _isSending Then Return

    '        If modGlobal.confDevDualMain = 1 Then
    '            Dim data As New PipeData With {
    '            .pStatus = iStatus,
    '            .pStep = iStep
    '        }

    '            Try
    '                _isSending = True
    '                Dim sent As Boolean = Await pipeHelper.SendAsync(data)
    '                If Not sent Then
    '                    Debug.WriteLine("Data not sent: pipe busy or disconnected.")
    '                End If
    '            Catch ex As Exception
    '                Debug.WriteLine($"SendAsync error: {ex.Message}")
    '            Finally
    '                _isSending = False
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        Debug.WriteLine($"TimerNamedPipe_Tick error: {ex.Message}")
    '    End Try
    'End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        iStatus = 98
        iStep = 1
        lblCurrentStatus_Value.Text = iStatus.ToString()
        lblCurrentStep_Value.Text = iStep.ToString()
    End Sub

    Private Async Sub DoProcess_Tick(sender As Object, e As EventArgs)
        Try
            Select Case chkType
                Case QType.qInbound
                    Select Case iStep
                        Case 1
                            Await Task.Delay(900)
                            SetCase([step]:=2)
                        Case 2
                            Await Task.Delay(900)
                            SetCase([step]:=3)
                        Case 3
                            Await Task.Delay(900)
                            SetCase([step]:=4)
                        Case 4
                            Await Task.Delay(900)
                            SetCase([step]:=5)
                        Case 5
                            Await Task.Delay(900)
                            SetCase([step]:=6)
                        Case 6
                            If modGlobal.confDevDualMain = 2 AndAlso pipeHelper IsNot Nothing AndAlso pipeHelper.IsConnected AndAlso ReceivediStatus <> 1 AndAlso ReceivediStep <= 8 Then
                                ' รอจนกว่า G02 จะ Step > 8
                                Return
                            Else
                                Await Task.Delay(900)
                                SetCase([step]:=7)
                            End If
                        Case 7
                            Await Task.Delay(900)
                            SetCase([step]:=8)
                        Case 8
                            Await Task.Delay(900)
                            SetCase([step]:=9)
                        Case 9
                            Await Task.Delay(900)
                            SetCase([step]:=10)
                        Case 10
                            Await Task.Delay(900)
                            SetCase([step]:=11)
                        Case 11
                            Await Task.Delay(900)
                            SetCase([step]:=12)
                        Case 12
                            Await Task.Delay(900)
                            SetCase([step]:=13)
                        Case 13
                            Await Task.Delay(900)
                            SetCase([step]:=0)
                            iStatus = 1
                            lblCurrentStatus_Value.Text = iStatus.ToString()
                    End Select
                Case Else
                    ' กรณีอื่นของ chkType
            End Select
        Catch ex As Exception
            Debug.WriteLine($"DoProcess_Tick error: {ex.Message}")
        End Try
    End Sub

    Private Sub SetCase([step] As Integer)
        iStep = [step]
        lblCurrentStep_Value.Text = iStep.ToString()
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        pipeHelper?.Dispose()
    End Sub
End Class
