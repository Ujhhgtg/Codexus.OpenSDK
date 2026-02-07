using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Archives.SevenZip;
using SharpCompress.Archives.Zip;

namespace Codexus.Game.Launcher.Utils;

// TODO: do not upgrade SevenCompress, API has changed
public static class CompressionUtil
{
    public static bool Extract7Z(string filePath, string outPath, Action<int> progressAction)
    {
        bool flag;
        try
        {
            var sevenZipArchive = SevenZipArchive.Open(filePath);
            try
            {
                sevenZipArchive.ExtractToDirectory(outPath, delegate(double dp) { progressAction((int)(dp * 100.0)); },
                    CancellationToken.None);
                flag = true;
            }
            finally
            {
                sevenZipArchive.Dispose();
            }
        }
        catch
        {
            flag = ExtractZip(filePath, outPath, progressAction);
        }

        return flag;
    }

    private static bool ExtractZip(string filePath, string outPath, Action<int> progressAction)
    {
        bool flag;
        try
        {
            var zipArchive = ZipArchive.Open(filePath);
            try
            {
                zipArchive.ExtractToDirectory(outPath, delegate(double dp) { progressAction((int)(dp * 100.0)); },
                    CancellationToken.None);
                flag = true;
            }
            finally
            {
                zipArchive.Dispose();
            }
        }
        catch
        {
            flag = false;
        }

        return flag;
    }

    public static async Task Extract7ZAsync(string archivePath, string outputDir, Action<int>? progress = null)
    {
        await Task.Run(delegate
        {
            var archive = ArchiveFactory.Open(archivePath);
            try
            {
                var num = archive.Entries.Count();
                var num2 = 0;
                foreach (var archiveEntry in archive.Entries)
                {
                    var flag = archiveEntry != null && !archiveEntry.IsDirectory && archiveEntry.Key != null;
                    if (flag)
                    {
                        var text = Path.Combine(outputDir, archiveEntry.Key);
                        var directoryName = Path.GetDirectoryName(text);
                        var flag2 = directoryName == null;
                        if (flag2) throw new ArgumentException("Invalid directory name");
                        var flag3 = !Directory.Exists(directoryName);
                        if (flag3) Directory.CreateDirectory(directoryName);

                        using var stream = archiveEntry.OpenEntryStream();
                        using var fileStream = File.Create(text);
                        stream.CopyTo(fileStream);
                    }

                    num2++;
                    if (progress != null) progress((int)(num2 / (double)num * 100.0));
                }
            }
            finally
            {
                archive.Dispose();
            }
        });
    }
}