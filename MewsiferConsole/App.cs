namespace MewsiferConsole
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
