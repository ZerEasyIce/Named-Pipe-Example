using Named_Pipe_Example.clsX;
using System;

namespace Named_Pipe_Example.modX
{
    public static class modGlobal
    {
        public static clsIni confIni = new clsIni(AppDomain.CurrentDomain.BaseDirectory + @"\confX\config.ini");

        // Timer
        public static int confTimerProcess = int.Parse(confIni.ReadValue("Timer", "Process"));
        public static int confTimerDash = int.Parse(confIni.ReadValue("Timer", "Dash"));
        public static int confTimerDelay = int.Parse(confIni.ReadValue("Timer", "Delay"));

        // Device  Detial
        public static string confDevTitleName = confIni.ReadValue("Machine", "TitleName");
        public static string confDevName = confIni.ReadValue("Machine", "MachineName");
        public static int confDevNo = int.Parse(confIni.ReadValue("Machine", "MachineNo"));
        public static int confDevCVToNo = int.Parse(confIni.ReadValue("Machine", "MachineCVToNo"));
        public static int confDevPriority = int.Parse(confIni.ReadValue("Machine", "WorkPriority"));
        public static string confDevDualName = confIni.ReadValue("Machine", "DualMachineName");
        public static int confDevDualMain = int.Parse(confIni.ReadValue("Machine", "MachineDualMain"));
        public static string confDevPipeName = confIni.ReadValue("Machine", "PipeName");

        public static class QType
        {
            public const string qInbound = "I";
            public const string qOutbound = "O";
            public const string qEject = "E";
        }
    }
}
