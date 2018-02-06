
using System;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Rules_of_Survival_Fullscreen
{
    public class Mem
    {
        public static Process m_Process;
        public static IntPtr m_pProcessHandle;

        public static void Initialize(string ProcessName)
        {
            if ((uint)Process.GetProcessesByName(ProcessName).Length > 0U)
            {
                Mem.m_Process = Process.GetProcessesByName(ProcessName)[0];
            }
            else
            {
                int num = (int)MessageBox.Show("Open Ros.exe first");
                Environment.Exit(1);
            }
            Mem.m_pProcessHandle = Mem.OpenProcess(56, false, Mem.m_Process.Id);
        }
        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
    }
}
