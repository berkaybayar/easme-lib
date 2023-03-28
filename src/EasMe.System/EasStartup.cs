using Microsoft.Win32;
namespace EasMe.System;

public static class EasStartup
{
    public static void AddToLaunchOnStartup(string applicationName, string applicationExecutablePath)
    {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        rk?.SetValue(applicationName, applicationExecutablePath);
    }
    public static void RemoveFromLaunchOnStartup(string applicationName)
    {
        RegistryKey rk = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        rk?.DeleteValue(applicationName, false);

    }
}