using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MewsiferConsole.Common
{
  /// <summary>
  /// Helper for creating and reading log dumps (.mew)
  /// </summary>
  public static class MewFile
  {
    /// <summary>
    /// Compresses the contents of the logDumpFile into a .mew file for use in the console.
    /// </summary>
    public static void Write(string logDumpFile, string destinationFile)
    {
      using (var logDumpStream = File.Open(logDumpFile, FileMode.Open))
      {
        using (var mewFileStream = File.Create(destinationFile))
        {
          using (var compressor = new GZipStream(mewFileStream, CompressionLevel.Optimal))
          {
            logDumpStream.CopyTo(compressor);
          }
        }
      }
    }

    /// <summary>
    /// Decompresses the contents of mewFile into a temporary file and returns it's path.
    /// </summary>
    public static string Read(string mewFile)
    {
      var tempFile = Path.GetTempFileName();
      using (var mewFileStream = File.Open(mewFile, FileMode.Open))
      {
        using (var tempFileStream = File.Create(tempFile))
        {
          using (var decompressor = new GZipStream(mewFileStream, CompressionMode.Decompress))
          {
            decompressor.CopyTo(tempFileStream);
          }
        }
      }
      return tempFile;
    }
  }
}
