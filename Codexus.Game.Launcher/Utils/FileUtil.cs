using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Codexus.Game.Launcher.Utils;

public static class FileUtil
{
    public static string[] EnumerateFiles(string path, string? fileType = null)
    {
        var flag = string.IsNullOrWhiteSpace(path) || !Directory.Exists(path);
        string[] array;
        if (flag)
        {
            array = [];
        }
        else
        {
            var text = string.IsNullOrWhiteSpace(fileType) ? "*" : "*." + fileType.TrimStart('.').ToLowerInvariant();
            array = Directory.EnumerateFiles(path, text, SearchOption.AllDirectories).ToArray();
        }

        return array;
    }

    public static string ComputeMd5FromFile(string path)
    {
        var flag = string.IsNullOrWhiteSpace(path) || !File.Exists(path);
        string text;
        if (flag)
            text = string.Empty;
        else
            try
            {
                using var fileStream = File.OpenRead(path);
                using var md = MD5.Create();
                text = Convert.ToHexString(md.ComputeHash(fileStream)).ToLowerInvariant();
            }
            catch (IOException)
            {
                text = string.Empty;
            }
            catch (UnauthorizedAccessException)
            {
                text = string.Empty;
            }

        return text;
    }

    public static bool CreateSymbolicLinkSafe(string linkPath, string targetPath)
    {
        var flag = string.IsNullOrWhiteSpace(linkPath) || string.IsNullOrWhiteSpace(targetPath);
        bool flag2;
        if (flag)
            flag2 = false;
        else
            try
            {
                var flag3 = Directory.Exists(targetPath);
                var flag4 = File.Exists(linkPath) || Directory.Exists(linkPath);
                if (flag4)
                {
                    FileSystemInfo fileSystemInfo = flag3 ? new DirectoryInfo(linkPath) : new FileInfo(linkPath);
                    var flag5 = fileSystemInfo.LinkTarget != null &&
                                Path.GetFullPath(fileSystemInfo.LinkTarget) == Path.GetFullPath(targetPath);
                    if (flag5) return true;
                    fileSystemInfo.Delete();
                }

                if (flag3)
                    Directory.CreateSymbolicLink(linkPath, targetPath);
                else
                    File.CreateSymbolicLink(linkPath, targetPath);
                flag2 = true;
            }
            catch (IOException)
            {
                flag2 = false;
            }
            catch (UnauthorizedAccessException)
            {
                flag2 = false;
            }
            catch (PlatformNotSupportedException)
            {
                flag2 = false;
            }

        return flag2;
    }

    public static void CleanDirectorySafe(string path)
    {
        var flag = !string.IsNullOrWhiteSpace(path);
        if (flag)
        {
            DeleteDirectorySafe(path);
            CreateDirectorySafe(path);
        }
    }

    public static bool CreateDirectorySafe(string path)
    {
        var flag = string.IsNullOrWhiteSpace(path);
        bool flag2;
        if (flag)
            flag2 = false;
        else
            try
            {
                var flag3 = !Directory.Exists(path);
                if (flag3) Directory.CreateDirectory(path);
                flag2 = true;
            }
            catch (IOException)
            {
                flag2 = false;
            }
            catch (UnauthorizedAccessException)
            {
                flag2 = false;
            }

        return flag2;
    }

    public static bool DeleteDirectorySafe(string path)
    {
        var flag = string.IsNullOrWhiteSpace(path) || !Directory.Exists(path);
        bool flag2;
        if (flag)
            flag2 = true;
        else
            try
            {
                Directory.Delete(path, true);
                flag2 = true;
            }
            catch (IOException)
            {
                flag2 = false;
            }
            catch (UnauthorizedAccessException)
            {
                flag2 = false;
            }

        return flag2;
    }

    public static bool CopyFileSafe(string sourcePath, string destPath)
    {
        var flag = string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(destPath);
        bool flag2;
        if (flag)
            flag2 = false;
        else
            try
            {
                var directoryName = Path.GetDirectoryName(destPath);
                var flag3 = !string.IsNullOrEmpty(directoryName) && !Directory.Exists(directoryName);
                if (flag3) Directory.CreateDirectory(directoryName);
                File.Copy(sourcePath, destPath, true);
                flag2 = true;
            }
            catch (IOException)
            {
                flag2 = false;
            }
            catch (UnauthorizedAccessException)
            {
                flag2 = false;
            }

        return flag2;
    }

    public static void CopyDirectory(string sourceDir, string targetDir, bool includeRoot = true,
        bool deleteSource = false)
    {
        var flag = string.IsNullOrWhiteSpace(sourceDir) || !Directory.Exists(sourceDir);
        if (!flag)
        {
            var name = new DirectoryInfo(sourceDir).Name;
            var text = includeRoot ? Path.Combine(targetDir, name) : targetDir;
            Directory.CreateDirectory(text);
            foreach (var text2 in Directory.EnumerateFileSystemEntries(sourceDir))
            {
                var fileName = Path.GetFileName(text2);
                var text3 = Path.Combine(text, fileName);
                var flag2 = Directory.Exists(text2);
                if (flag2)
                {
                    CopyDirectory(text2, text, true, deleteSource);
                    if (deleteSource) Directory.Delete(text2, true);
                }
                else
                {
                    File.Copy(text2, text3, true);
                    if (deleteSource) File.Delete(text2);
                }
            }
        }
    }

    public static bool DeleteFileSafe(string path)
    {
        bool flag2;
        try
        {
            var flag = !File.Exists(path);
            if (flag)
            {
                flag2 = true;
            }
            else
            {
                File.Delete(path);
                flag2 = true;
            }
        }
        catch (Exception)
        {
            flag2 = false;
        }

        return flag2;
    }

    public static bool IsFileReadable(string filePath)
    {
        bool flag;
        try
        {
            using var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            flag = fileStream.Length > 0L;
        }
        catch
        {
            flag = false;
        }

        return flag;
    }

    public static async Task WriteFileSafelyAsync(string filePath, byte[] buffer)
    {
        var tempFile = filePath + ".tmp";
        try
        {
            await File.WriteAllBytesAsync(tempFile, buffer);
            if (File.Exists(tempFile) && new FileInfo(tempFile).Length > 0L)
            {
                if (File.Exists(filePath)) File.Delete(filePath);
                File.Move(tempFile, filePath);
            }
        }
        catch
        {
            if (File.Exists(tempFile)) File.Delete(tempFile);
            throw;
        }
    }
}