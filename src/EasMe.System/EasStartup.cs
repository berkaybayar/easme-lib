using Microsoft.Win32;

namespace EasMe.System;

public static class EasStartup {
    public static void AddToLaunchOnOSStartup(string applicationName, string applicationExecutablePath) {
        var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        rk?.SetValue(applicationName, applicationExecutablePath);
    }

    public static void RemoveFromLaunchOnOSStartup(string applicationName) {
        var rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        rk?.DeleteValue(applicationName, false);
    }
}