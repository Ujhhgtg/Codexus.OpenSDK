using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Codexus.Game.Launcher.Utils;
public static class DownloadUtil
{
	public static async Task<bool> DownloadAsync(
	    string url, 
	    string destinationPath, 
	    Action<uint>? downloadProgress = null, 
	    int maxConcurrentSegments = 8, 
	    CancellationToken ct = default)
	{
	    try
	    {
	        // 1. Prepare Directory
	        var directory = Path.GetDirectoryName(destinationPath);
	        if (!string.IsNullOrEmpty(directory)) Directory.CreateDirectory(directory);

	        // 2. Check Server Capabilities (HEAD request)
	        using var headReq = new HttpRequestMessage(HttpMethod.Head, url);
	        using var headResp = await HttpClient.SendAsync(headReq, HttpCompletionOption.ResponseHeadersRead, ct);
	        headResp.EnsureSuccessStatusCode();

	        var totalSize = headResp.Content.Headers.ContentLength;
	        var supportsRange = headResp.Headers.AcceptRanges.Contains("bytes");

	        // 3. Decide: Multi-segmented or Single download
	        // Requirements for multi-segment: Support ranges, size > 1MB, and concurrency > 1
	        if (supportsRange && totalSize >= 1_048_576L && maxConcurrentSegments >= 2)
	        {
	            return await MultiSegmentDownloadAsync(url, destinationPath, totalSize.Value, downloadProgress, maxConcurrentSegments, ct);
	        }

	        return await SingleDownloadAsync(url, destinationPath, downloadProgress, ct);
	    }
	    catch (OperationCanceledException)
	    {
	        Log.Information("Download canceled: {Url}", url);
	        throw;
	    }
	    catch (Exception ex)
	    {
	        Log.Error(ex, "Download failed for {Url}", url);
	        return false;
	    }
	}

	private static async Task<bool> MultiSegmentDownloadAsync(
	    string url, string path, long totalSize, Action<uint>? progress, int maxSegments, CancellationToken ct)
	{
	    // Initialize shared state for reporting
	    long totalRead = 0;
	    var lastReportedPercent = -1;

	    void ReportProgress(int bytesRead)
	    {
	        var read = Interlocked.Add(ref totalRead, bytesRead);
	        var percent = (int)((double)read / totalSize * 100);
	        if (percent > lastReportedPercent)
	        {
	            lastReportedPercent = percent;
	            progress?.Invoke((uint)percent);
	        }
	    }

	    using var mmFile = MemoryMappedFile.CreateFromFile(path, FileMode.Create, null, totalSize, MemoryMappedFileAccess.ReadWrite);
	    using var semaphore = new SemaphoreSlim(maxSegments);
	    
	    // Divide work into ranges
	    var ranges = CalculateRanges(maxSegments * 3, totalSize);
	    var tasks = ranges.Select(async range =>
	    {
	        await semaphore.WaitAsync(ct);
	        try
	        {
	            await DownloadRangeWithRetryAsync(url, mmFile, range, ReportProgress, ct);
	        }
	        finally
	        {
	            semaphore.Release();
	        }
	    });

	    await Task.WhenAll(tasks);
	    progress?.Invoke(100U);
	    return true;
	}

	private static async Task DownloadRangeWithRetryAsync(
	    string url, MemoryMappedFile mmFile, (long Start, long End) range, Action<int> onRead, CancellationToken ct)
	{
	    for (var attempt = 1; attempt <= 3; attempt++)
	    {
	        try
	        {
	            using var req = new HttpRequestMessage(HttpMethod.Get, url);
	            req.Headers.Range = new RangeHeaderValue(range.Start, range.End);

	            using var resp = await HttpClient.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, ct);
	            resp.EnsureSuccessStatusCode();

	            using var netStream = await resp.Content.ReadAsStreamAsync(ct);
	            using var viewStream = mmFile.CreateViewStream(range.Start, range.End - range.Start + 1, MemoryMappedFileAccess.Write);

	            var buffer = new byte[8192];
	            int bytesRead;
	            while ((bytesRead = await netStream.ReadAsync(buffer, ct)) > 0)
	            {
	                await viewStream.WriteAsync(buffer.AsMemory(0, bytesRead), ct);
	                onRead(bytesRead);
	            }
	            return; // Success
	        }
	        catch (Exception) when (attempt < 3 && !ct.IsCancellationRequested)
	        {
	            await Task.Delay(500 * attempt, ct);
	        }
	    }
	}

	private static async Task<bool> SingleDownloadAsync(
		string url, 
		string destinationPath, 
		Action<uint>? downloadProgress, 
		CancellationToken ct)
	{
		// Use 'using' for automatic disposal of the response
		using var resp = await HttpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct);
		resp.EnsureSuccessStatusCode();

		var totalBytes = resp.Content.Headers.ContentLength.GetValueOrDefault();
		long totalRead = 0;
    
		// Open both streams with 'using' for safe cleanup
		using var input = await resp.Content.ReadAsStreamAsync(ct);
		using var output = new FileStream(destinationPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, useAsync: true);

		var buffer = new byte[8192];
		var stopwatch = Stopwatch.StartNew();
		var lastReportedProgress = -1;

		int bytesRead;
		while ((bytesRead = await input.ReadAsync(buffer, ct)) > 0)
		{
			await output.WriteAsync(buffer.AsMemory(0, bytesRead), ct);

			// Progress reporting logic
			if (totalBytes > 0)
			{
				totalRead += bytesRead;

				// Throttle progress updates to every 150ms to save UI thread resources
				if (stopwatch.ElapsedMilliseconds > 150)
				{
					stopwatch.Restart();
					var percent = (int)(totalRead * 100.0 / totalBytes);
                
					if (percent > lastReportedProgress)
					{
						lastReportedProgress = percent;
						downloadProgress?.Invoke((uint)percent);
					}
				}
			}
		}

		// Ensure we hit 100% at the end
		downloadProgress?.Invoke(100U);
		return true;
	}
	
	private static IEnumerable<(long Start, long End)> CalculateRanges(int segments, long totalSize)
	{
		// Calculate how many bytes each segment should handle
		var segmentSize = totalSize / segments;

		for (var i = 0; i < segments; i++)
		{
			var start = i * segmentSize;
        
			// The last segment must take all remaining bytes to ensure no data is lost
			var end = i == segments - 1 ? totalSize - 1 : start + segmentSize - 1;

			yield return (start, end);
		}
	}
	
	private static readonly HttpClient HttpClient = new(new HttpClientHandler
	{
		MaxConnectionsPerServer = 16
	})
	{
		Timeout = TimeSpan.FromMinutes(10L)
	};
}