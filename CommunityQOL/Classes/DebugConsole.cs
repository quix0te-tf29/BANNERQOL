using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace CommunityQOL.Classes {
    public class DebugConsole {

        private static bool Debugging = true;
        private static DebugConsole _instance;

        protected DebugConsole() {
            InitConsole();
        }

        public static DebugConsole Instance() {
            if (_instance == null) { _instance = new DebugConsole(); }
            return _instance;
        }

        private void InitConsole() {
            AllocConsole();
        }

        public static void Log(object sender, String message) {
            if (Debugging) {
                if (_instance == null) {
                    Instance();
                }
                System.Console.WriteLine("[" + sender + "]: " + message);
            }
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
    }
}
