using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoterConsole
{
    internal static class App
    {
        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern bool AllocConsole();


        internal static void Install()
        {
#if DEBUG
            AllocConsole();
#endif
        }
    }
}
