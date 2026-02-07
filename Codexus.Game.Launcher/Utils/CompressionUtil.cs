using System;
using System.IO;
using System.Threading.Tasks;
using Serilog;
using SharpCompress.Archives;
using SharpCompress.Common;
using SharpCompress.Readers;

namespace Codexus.Game.Launcher.Utils;

public static class CompressionUtil
{
    public static bool ExtractArchive(string filePath, string outputDir, Action<int>? progress = null)
    {
        Log.Information("[CompressionUtil] Extracting {FilePath} to {OutputDir}...", filePath, outputDir);
        
        try
        {
            using var archive = ArchiveFactory.OpenArchive(filePath, new ReaderOptions
            {
                Progress = new Progress<ProgressReport>(report =>
                {
                    progress?.Invoke((int)(report.PercentComplete ?? 0));
                })
            });
            ExtractInternal(archive, outputDir);
            return true;
        }
        catch (Exception ex)
        {
            Log.Error(ex, "[CompressionUtil] Error extracting {FilePath}.", filePath);
            return false;
        }
    }
    
    public static async Task ExtractArchiveAsync(string filePath, string outputDir, Action<int>? progress = null)
    {
        Log.Information("[CompressionUtil] Extracting {FilePath} to {OutputDir}...", filePath, outputDir);
        
        // Offload to a background thread to keep UI responsive
        await Task.Run(() =>
        {
            using var archive = ArchiveFactory.OpenArchive(filePath, new ReaderOptions
            {
                Progress = new Progress<ProgressReport>(report =>
                {
                    progress?.Invoke((int)(report.PercentComplete ?? 0));
                })
            });
            ExtractInternal(archive, outputDir);
        });
    }
    
    public static bool Extract7Z(string filePath, string outPath, Action<int> progress) => ExtractArchive(filePath, outPath, progress);
    
    public static async Task Extract7ZAsync(string filePath, string outputDir, Action<int>? progress = null) =>  await ExtractArchiveAsync(filePath, outputDir, progress);

    private static void ExtractInternal(IArchive archive, string outputDir)
    {
        Directory.CreateDirectory(outputDir);
        
        var extractionOptions = new ExtractionOptions
        {
            ExtractFullPath = true,
            Overwrite = true
        };

        archive.WriteToDirectory(outputDir, extractionOptions);
    }
}