Module modGlobal

    Public confIni As New clsIni(Application.StartupPath & "\confX\config.ini")

    'Timer
    Public confTimerProcess As Integer = CInt(confIni.ReadValue("Timer", "Process"))
    Public confTimerDash As Integer = CInt(confIni.ReadValue("Timer", "Dash"))
    Public confTimerDelay As Integer = CInt(confIni.ReadValue("Timer", "Delay"))

    'Device  Detial
    Public confDevTitleName As String = confIni.ReadValue("Machine", "TitleName")
    Public confDevName As String = confIni.ReadValue("Machine", "MachineName")
    Public confDevNo As Integer = CInt(confIni.ReadValue("Machine", "MachineNo"))
    Public confDevCVToNo As Integer = CInt(confIni.ReadValue("Machine", "MachineCVToNo"))
    Public confDevPriority As Integer = CInt(confIni.ReadValue("Machine", "WorkPriority"))
    Public confDevDualName As String = confIni.ReadValue("Machine", "DualMachineName")
    Public confDevDualMain As Integer = CInt(confIni.ReadValue("Machine", "MachineDualMain"))
    Public confDevPipeName As String = confIni.ReadValue("Machine", "PipeName")

    Public Class QType
        Public Const qInbound As String = "I"
        Public Const qOutbound As String = "O"
        Public Const qEject As String = "E"
    End Class

End Module
