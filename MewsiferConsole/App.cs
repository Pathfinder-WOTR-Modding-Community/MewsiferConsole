using MewsiferConsole.Common;
using Newtonsoft.Json;
using static MewsiferConsole.Common.PipeContract;

namespace MewsiferConsole
{
  internal static class App
  {
    [System.Runtime.InteropServices.DllImport("kernel32.dll")]
    private static extern bool AllocConsole();

    public static MessageSource MessageSource = new();

    internal static void Install()
    {
#if DEBUG
        AllocConsole();
#endif

      var args = Environment.GetCommandLineArgs();

      if (args.Length > 1)
      {
        if (args[1] == "--game-server")
        {
          MessageSource.Source = new IPC.Server();
        }
        else if (File.Exists(args[1]))
        {
          MessageSource.Source = new MessagesFromFile(args[1]);
        }
      }

      if (MessageSource.Source == null)
      {
        Application.Run(new ChooseMessageSourceForm());
      }

      if (MessageSource.Source != null)
        Application.Run(new MewsiferConsole());

    }
  }
}
