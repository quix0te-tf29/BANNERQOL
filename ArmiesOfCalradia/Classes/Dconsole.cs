using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace ArmiesOfCalradia.Classes
{
    class Dconsole
    {
        private static Dconsole _instance;
        protected Dconsole()
        {
            InitConsole();
        }

        public static Dconsole Instance()
        {
            if (_instance == null)
            {
                _instance = new Dconsole();
            }
            return _instance;
        }

        private void InitConsole()
        {
            AllocConsole();
        }

        public void Log(object sender, String message)
        {
            System.Console.WriteLine("[" + sender + "]: " + message);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
