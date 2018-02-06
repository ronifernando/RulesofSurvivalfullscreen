using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Rules_of_Survival_Fullscreen
{
    public partial class Main : Form
    {
        public string WINDOW_NAME = "null";
        public Main()
        {
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    if (process.ProcessName == "ros")
                    {
                        WINDOW_NAME = process.MainWindowTitle;
                    }
                }
            }
            this.InitializeComponent();
        }

        public static IntPtr WinGetHandle(string wName)
        {
            IntPtr hwnd = IntPtr.Zero;
            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle.Contains(wName))
                {
                    hwnd = pList.MainWindowHandle;
                }
            }
            return hwnd;
        }

        [DllImport("User32.dll")]
        private static extern bool GetAsyncKeyState(Keys vKey);

        [DllImport("user32.dll")]
        private static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        private void Main_Load(object sender, EventArgs e){
            panel1.Parent = pictureBox1;
            label2.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label4.Parent = pictureBox1;
            IntPtr aaaa = WinGetHandle(WINDOW_NAME);
            const int GWL_STYLE = (-16);
            const int WS_VISIBLE = 0x10000000;
            SetWindowLong(aaaa, GWL_STYLE, (WS_VISIBLE));
            Rectangle resolution = Screen.PrimaryScreen.Bounds;
            MoveWindow(aaaa, resolution.Top, resolution.Left, resolution.Width, resolution.Height, true);
            ShowWindowAsync(aaaa, 3);

            this.FormBorderStyle = FormBorderStyle.None;
            Mem.Initialize("ros");
            if (Mem.m_pProcessHandle == IntPtr.Zero)
            {
                int num = (int)MessageBox.Show("Game Not Found", "Please Start Rules of Survival");
                this.Close();
            }
            else
            {
                int num = (int)MessageBox.Show("Done");
                Main.SetWindowLong(this.Handle, -20, Main.GetWindowLong(this.Handle, -20) | 524288 | 32);
            }
            new Thread(new ThreadStart(this.thread)).Start();
        }
       
 
        public void thread()
        {
            while (true)
            {
                this.DrawMenu();
                Thread.Sleep(100);
            }
        }

        public void DrawMenu()
        {
            if (Main.GetAsyncKeyState(Keys.F1))
            {
                int num = (int)MessageBox.Show("Game Not Found", "Please Start Rules of Survival");
            }
            if (Main.GetAsyncKeyState(Keys.F10))
            {
                Process[] procs = Process.GetProcessesByName("ros");
                foreach (Process proc in procs){ 
                    proc.Kill();
                }

                Application.Exit();
                Environment.Exit(1);
                this.Close();
            }
        }


    }  
}
