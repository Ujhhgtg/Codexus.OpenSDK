using System;
using System.Diagnostics;
using System.IO;
using Codexus.Game.Launcher.Utils;

namespace Codexus.Game.Launcher.Services.Java;

public static class RepairService
{
    public static void RegisterKillGameAction(Action action)
    {
        _killGameAction = action;
    }

    public static void ClearClientResources()
    {
        if (_killGameAction == null)
        {
            var processesByName = Process.GetProcessesByName("javaw");
            foreach (var process in processesByName)
                try
                {
                    process.Kill(true);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to kill javaw process", ex);
                }
        }
        else
        {
            _killGameAction();
        }

        var cachePath = PathUtil.CachePath;
        var flag2 = Directory.Exists(cachePath);
        if (flag2) FileUtil.DeleteDirectorySafe(cachePath);
    }

    private static Action? _killGameAction;
}