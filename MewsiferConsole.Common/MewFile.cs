using System.IO.Compression;
using System.IO;
using System;

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
    public static Stream Read(string mewFile)
    {
      string archiveType = "";
      using (var raw = File.OpenRead(mewFile))
      {
        byte[] bytes = new byte[4];
        if (raw.Length < 4)
          throw new Exception("truncated file");
        raw.Read(bytes, 0, 4);

        if (bytes[0] == 0x1f && bytes[1] == 0x8b)
          archiveType = "gzip";
        else
          archiveType = "zip";
      }

      if (archiveType == "gzip")
        return new GZipStream(File.Open(mewFile, FileMode.Open), CompressionMode.Decompress);
      else if (archiveType == "zip")
        return ZipFile.OpenRead(mewFile).Entries[0].Open();
      throw new Exception("Could not open stream");
    }
  }
}
