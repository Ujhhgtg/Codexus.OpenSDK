using System;
using System.Linq;
using System.Threading.Tasks;
using SharpCompress.Archives;
using SharpCompress.Common;

namespace Codexus.Game.Launcher.Utils;

public static class CompressionUtil
{
    public static bool ExtractArchive(string filePath, string outPath, Action<int>? progressAction = null)
    {
        try
        {
            using var archive = ArchiveFactory.OpenArchive(filePath);
            ExtractInternal(archive, outPath, progressAction);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public static async Task ExtractArchiveAsync(string filePath, string outputDir, Action<int>? progress = null)
    {
        // Offload to a background thread to keep UI responsive
        await Task.Run(() =>
        {
            using var archive = ArchiveFactory.OpenArchive(filePath);
            ExtractInternal(archive, outputDir, progress);
        });
    }
    
    public static bool Extract7Z(string filePath, string outPath, Action<int> progressAction) => ExtractArchive(filePath, outPath, progressAction);
    
    public static async Task Extract7ZAsync(string filePath, string outputDir, Action<int>? progress = null) =>  await ExtractArchiveAsync(filePath, outputDir, progress);

    private static void ExtractInternal(IArchive archive, string outputDir, Action<int>? progressAction)
    {
        // Filter entries to get only files (ignores directory entries as WriteToDirectory handles folder creation)
        var entries = archive.Entries.Where(e => !e.IsDirectory).ToList();
        var total = entries.Count;
        var current = 0;

        var extractionOptions = new ExtractionOptions
        {
            ExtractFullPath = true,
            Overwrite = true
        };

        foreach (var entry in entries)
        {
            if (entry.Key == null) continue;

            // built-in extraction handles path combining and directory creation safely
            entry.WriteToDirectory(outputDir, extractionOptions);

            current++;
            if (total > 0)
            {
                var percent = (int)((double)current / total * 100);
                progressAction?.Invoke(percent);
            }
        }
    }
}