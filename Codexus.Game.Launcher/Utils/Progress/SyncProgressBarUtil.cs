using System;

namespace Codexus.Game.Launcher.Utils.Progress;

public static class SyncProgressBarUtil
{
    private static string GetAnsiColorCode(ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "\e[30m",
            ConsoleColor.DarkBlue => "\e[34m",
            ConsoleColor.DarkGreen => "\e[32m",
            ConsoleColor.DarkCyan => "\e[36m",
            ConsoleColor.DarkRed => "\e[31m",
            ConsoleColor.DarkMagenta => "\e[35m",
            ConsoleColor.DarkYellow => "\e[33m",
            ConsoleColor.Gray => "\e[37m",
            ConsoleColor.DarkGray => "\e[90m",
            ConsoleColor.Blue => "\e[94m",
            ConsoleColor.Green => "\e[92m",
            ConsoleColor.Cyan => "\e[96m",
            ConsoleColor.Red => "\e[91m",
            ConsoleColor.Magenta => "\e[95m",
            ConsoleColor.Yellow => "\e[93m",
            ConsoleColor.White => "\e[97m",
            _ => "\e[37m"
        };
    }

    private static readonly Lock Lock = new();

    public class ProgressBarOptions
    {
        public int Width { get; set; } = 45;
        public char FillChar { get; set; } = '■';
        public char EmptyChar { get; set; } = '·';
        public string ProgressFormat { get; set; } = "{0:P1}";
        public bool ShowPercentage { get; set; } = true;
        public bool ShowElapsedTime { get; set; } = true;
        public bool ShowEta { get; set; } = true;
        public bool ShowSpinner { get; set; } = true;
        public ConsoleColor FillColor { get; set; } = ConsoleColor.Cyan;
        public ConsoleColor EmptyColor { get; set; } = ConsoleColor.DarkGray;
        public ConsoleColor SpinnerColor { get; set; } = ConsoleColor.Cyan;
        public string Prefix { get; set; } = "";
        public string Suffix { get; set; } = "";
        public bool LastLineNewline { get; set; } = true;
    }

    public class ProgressBar : IDisposable
    {
        public ProgressBar(int total, ProgressBarOptions? options = null)
        {
            _options = options ?? new ProgressBarOptions();
            _startTime = DateTime.Now;
            _spinnerChars = ['|', '/', '─', '\\'];
        }

        public void Update(int current, string action)
        {
            var flag = !_disposed;
            if (flag)
            {
                _current = current;
                _spinnerIndex = (_spinnerIndex + 1) % _spinnerChars.Length;
                Display(action);
            }
        }

        private static void Display(string action)
        {
        }

        public static void ClearCurrent()
        {
        }

        public void Dispose()
        {
            var flag = !_disposed;
            if (flag) _disposed = true;
            GC.SuppressFinalize(this);
        }

        private readonly ProgressBarOptions _options;
        private readonly DateTime _startTime;
        private readonly char[] _spinnerChars;
        private int _current;
        private int _spinnerIndex;
        private bool _disposed;
    }

    public class ProgressReport
    {
        public int Percent { get; set; }
        public string Message { get; set; } = "";
    }
}