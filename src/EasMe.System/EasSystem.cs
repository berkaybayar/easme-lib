using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using EasMe.Extensions;
using EasMe.System.Models;
using Microsoft.Win32;

namespace EasMe.System;

public static class EasSystem
{
    /// <summary>
    ///     Returns this computers MAC Address.
    /// </summary>
    /// <returns></returns>
    public static string GetMACAddress() {
        var macAddr = (
                          from nic in NetworkInterface.GetAllNetworkInterfaces()
                          where nic.OperationalStatus == OperationalStatus.Up
                          select nic.GetPhysicalAddress().ToString()
                      ).FirstOrDefault();
        if (!string.IsNullOrEmpty(macAddr))
            return macAddr;
        var active = GetMACAddresses_Active();
        if (active.Count > 0) {
            var mac = active.Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault();
            if (mac is not null) return mac;
        }

        var all = GetMACAddresses_All();
        if (all.Count > 0) {
            var mac = all.Where(x => !string.IsNullOrEmpty(x)).FirstOrDefault();
            if (mac is not null) return mac;
        }

        return "NOT_FOUND";
    }

    public static List<string> GetMACAddresses_Active() {
        return NetworkInterface.GetAllNetworkInterfaces()
                               .Where(x => x.OperationalStatus == OperationalStatus.Up)
                               .Select(x => x.GetPhysicalAddress().ToString())
                               .Where(x => !string.IsNullOrEmpty(x))
                               .ToList();
    }

    public static List<string> GetMACAddresses_All() {
        return NetworkInterface.GetAllNetworkInterfaces()
                               .OrderBy(x => x.OperationalStatus == OperationalStatus.Up)
                               .Select(x => x.GetPhysicalAddress().ToString())
                               .Where(x => !string.IsNullOrEmpty(x))
                               .ToList();
    }

    /// <summary>
    ///     Returns this computers Ram information as a list of RamModel object.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static List<RamModel> GetRamList() {
        var list = new List<RamModel>();

        foreach (var obj in GetManagementObjList("Win32_PhysicalMemory")) {
            var ramModel = new RamModel();
            ramModel.Attributes = obj.Properties["Attributes"].Value.ToString();
            ramModel.Capacity = obj.Properties["Capacity"].Value.ToString();
            ramModel.Caption = obj.Properties["Caption"].Value.ToString();
            ramModel.DeviceLocator = obj.Properties["DeviceLocator"].Value.ToString();
            ramModel.FormFactor = obj.Properties["FormFactor"].Value.ToString();
            ramModel.Manufacturer = obj.Properties["Manufacturer"].Value.ToString();
            ramModel.Name = obj.Properties["Name"].Value.ToString();
            ramModel.PartNumber = obj.Properties["PartNumber"].Value.ToString();
            ramModel.SerialNumber = obj.Properties["SerialNumber"].Value.ToString();
            ramModel.Speed = obj.Properties["Speed"].Value.ToString();
            ramModel.Tag = obj.Properties["Tag"].Value.ToString();
            ramModel.TotalWidth = obj.Properties["TotalWidth"].Value.ToString();
            ramModel.TypeDetail = obj.Properties["TypeDetail"].Value.ToString();
            ramModel.BankLabel = obj.Properties["BankLabel"].Value.ToString();
            ramModel.ConfiguredClockSpeed = obj.Properties["ConfiguredClockSpeed"].Value.ToString();
            ramModel.ConfiguredVoltage = obj.Properties["ConfiguredVoltage"].Value.ToString();
            ramModel.CreationClassName = obj.Properties["CreationClassName"].Value.ToString();
            ramModel.DataWidth = obj.Properties["DataWidth"].Value.ToString();
            ramModel.Description = obj.Properties["Description"].Value.ToString();
            ramModel.MaxVoltage = obj.Properties["MaxVoltage"].Value.ToString();
            ramModel.MinVoltage = obj.Properties["MinVoltage"].Value.ToString();
            ramModel.SMBIOSMemoryType = obj.Properties["SMBIOSMemoryType"].Value.ToString();
            list.Add(ramModel);
        }

        return list;
    }

    /// <summary>
    ///     Returns this computers Motherboard information as a MotherboardModel object.
    /// </summary>
    /// <returns></returns>
    public static MotherboardModel GetMotherboard() {
        var motherboardModel = new MotherboardModel();

        var item = GetManagementObjList("Win32_BaseBoard").FirstOrDefault();
        motherboardModel.Caption = item["Caption"].ToString();
        motherboardModel.ConfigOptions = item["ConfigOptions"].ToString();
        motherboardModel.CreationClassName = item["CreationClassName"].ToString();
        motherboardModel.Description = item["Description"].ToString();
        motherboardModel.HostingBoard = item["HostingBoard"].ToString();
        motherboardModel.HotSwappable = item["HotSwappable"].ToString();
        motherboardModel.Manufacturer = item["Manufacturer"].ToString();
        motherboardModel.Name = item["Name"].ToString();
        motherboardModel.PoweredOn = item["PoweredOn"].ToString();
        motherboardModel.Product = item["Product"].ToString();
        motherboardModel.Removable = item["Removable"].ToString();
        motherboardModel.Replaceable = item["Replaceable"].ToString();
        motherboardModel.RequiresDaughterBoard = item["RequiresDaughterBoard"].ToString();
        motherboardModel.SerialNumber = item["SerialNumber"].ToString();
        motherboardModel.Status = item["Status"].ToString();
        motherboardModel.Tag = item["Tag"].ToString();
        motherboardModel.Version = item["Version"].ToString();
        return motherboardModel;
    }

    //public static string? GetProcessorName()
    //{
    //	ManagementObjectSearcher mos =  new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
    //	foreach (ManagementObject mo in mos.Get())
    //	{
    //		return mo["Name"].ToString();
    //	}
    //	return "";
    //}
    public static string GetGpuName() {
        var item = GetManagementObjList("Win32_VideoController").FirstOrDefault();
        return item["Name"]?.ToString() ?? "";
    }

    public static string GetProcessorName() {
        var CPUName = Convert.ToString(Registry.GetValue(
                                                         "HKEY_LOCAL_MACHINE\\HARDWARE\\DESCRIPTION\\SYSTEM\\CentralProcessor\\0", "ProcessorNameString", null));
        return CPUName?.Trim() ?? "";
    }

    /// <summary>
    ///     Returns this computers Processor information as a CPUModel object.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static CpuModel GetProcessor() {
        var CPUModel = new CpuModel();
        var item = GetManagementObjList("Win32_Processor").FirstOrDefault();
        CPUModel.AddressWidth = item["AddressWidth"].ToString();
        CPUModel.Architecture = item["Architecture"].ToString();
        CPUModel.AssetTag = item["AssetTag"].ToString();
        CPUModel.Availability = item["Availability"].ToString();
        CPUModel.Caption = item["Caption"].ToString();
        CPUModel.Characteristics = item["Characteristics"].ToString();
        CPUModel.CpuStatus = item["CpuStatus"].ToString();
        CPUModel.CreationClassName = item["CreationClassName"].ToString();
        CPUModel.CurrentClockSpeed = item["CurrentClockSpeed"].ToString();
        CPUModel.CurrentVoltage = item["CurrentVoltage"].ToString();
        CPUModel.DataWidth = item["DataWidth"].ToString();
        CPUModel.Description = item["Description"].ToString();
        CPUModel.DeviceID = item["DeviceID"].ToString();
        CPUModel.ExtClock = item["ExtClock"].ToString();
        CPUModel.Family = item["Family"].ToString();
        CPUModel.L2CacheSize = item["L2CacheSize"].ToString();
        CPUModel.L3CacheSize = item["L3CacheSize"].ToString();
        CPUModel.L3CacheSpeed = item["L3CacheSpeed"].ToString();
        CPUModel.Level = item["Level"].ToString();
        CPUModel.LoadPercentage = item["LoadPercentage"].ToString();
        CPUModel.Manufacturer = item["Manufacturer"].ToString();
        CPUModel.MaxClockSpeed = item["MaxClockSpeed"].ToString();
        CPUModel.Name = item["Name"].ToString();
        CPUModel.NumberOfCores = item["NumberOfCores"].ToString();
        CPUModel.NumberOfLogicalProcessors = item["NumberOfLogicalProcessors"].ToString();
        CPUModel.PartNumber = item["PartNumber"].ToString();
        CPUModel.PowerManagementSupported = item["PowerManagementSupported"].ToString();
        CPUModel.ProcessorId = item["ProcessorId"].ToString();
        CPUModel.ProcessorType = item["ProcessorType"].ToString();
        CPUModel.Revision = item["Revision"].ToString();
        CPUModel.Role = item["Role"].ToString();
        CPUModel.SecondLevelAddressTranslationExtensions = item["SecondLevelAddressTranslationExtensions"].ToString();
        CPUModel.SerialNumber = item["SerialNumber"].ToString();
        CPUModel.SocketDesignation = item["SocketDesignation"].ToString();
        CPUModel.Status = item["Status"].ToString();
        CPUModel.StatusInfo = item["StatusInfo"].ToString();
        CPUModel.Stepping = item["Stepping"].ToString();
        CPUModel.SystemCreationClassName = item["SystemCreationClassName"].ToString();
        CPUModel.SystemName = item["SystemName"].ToString();
        CPUModel.ThreadCount = item["ThreadCount"].ToString();
        CPUModel.UpgradeMethod = item["UpgradeMethod"].ToString();
        CPUModel.Version = item["Version"].ToString();
        CPUModel.VirtualizationFirmwareEnabled = item["VirtualizationFirmwareEnabled"].ToString();
        CPUModel.VMMonitorModeExtensions = item["VMMonitorModeExtensions"].ToString();
        return CPUModel;
    }

    /// <summary>
    ///     Returns this computers Disk information as a list of DiskModel objects.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static List<DiskModel> GetDiskList() {
        var list = new List<DiskModel>();
        var disk = GetManagementObjList("Win32_DiskDrive").FirstOrDefault();
        var model = new DiskModel();
        model.BytesPerSector = disk["BytesPerSector"].ToString();
        model.Capabilities = disk["Capabilities"].ToString();
        model.CapabilityDescriptions = disk["CapabilityDescriptions"].ToString();
        model.Caption = disk["Caption"].ToString();
        model.ConfigManagerErrorCode = disk["ConfigManagerErrorCode"].ToString();
        model.ConfigManagerUserConfig = disk["ConfigManagerUserConfig"].ToString();
        model.CreationClassName = disk["CreationClassName"].ToString();
        model.FirmwareRevision = disk["FirmwareRevision"].ToString();
        model.Index = disk["Index"].ToString();
        model.InterfaceType = disk["InterfaceType"].ToString();
        model.Manufacturer = disk["Manufacturer"].ToString();
        model.MediaLoaded = disk["MediaLoaded"].ToString();
        model.MediaType = disk["MediaType"].ToString();
        model.Model = disk["Model"].ToString();
        model.Name = disk["Name"].ToString();
        model.Partitions = disk["Partitions"].ToString();
        model.PNPDeviceID = disk["PNPDeviceID"].ToString();
        model.SCSIBus = disk["SCSIBus"].ToString();
        model.SCSILogicalUnit = disk["SCSILogicalUnit"].ToString();
        model.SCSIPort = disk["SCSIPort"].ToString();
        model.SCSITargetId = disk["SCSITargetId"].ToString();
        model.SectorsPerTrack = disk["SectorsPerTrack"].ToString();
        model.SerialNumber = disk["SerialNumber"].ToString();
        model.Size = disk["Size"].ToString();
        model.Status = disk["Status"].ToString();
        model.SystemCreationClassName = disk["SystemCreationClassName"].ToString();
        model.SystemName = disk["SystemName"].ToString();
        model.TotalCylinders = disk["TotalCylinders"].ToString();
        model.TotalHeads = disk["TotalHeads"].ToString();
        model.TotalSectors = disk["TotalSectors"].ToString();
        model.TotalTracks = disk["TotalTracks"].ToString();
        model.TracksPerCylinder = disk["TracksPerCylinder"].ToString();
        list.Add(model);
        return list;
    }


    /// <summary>
    ///     Returns this computers GPU information as a list of GPUModel objects.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static List<GpuModel> GetGPUList() {
        var list = new List<GpuModel>();
        var GPUList = GetManagementObjList("Win32_VideoController");
        foreach (var video in GPUList) {
            var model = new GpuModel();
            model.AdapterRAM = video["AdapterRAM"].ToString();
            model.Availability = video["Availability"].ToString();
            model.Caption = video["Caption"].ToString();
            model.ConfigManagerErrorCode = video["ConfigManagerErrorCode"].ToString();
            model.ConfigManagerUserConfig = video["ConfigManagerUserConfig"].ToString();
            model.CreationClassName = video["CreationClassName"].ToString();
            model.CurrentBitsPerPixel = video["CurrentBitsPerPixel"].ToString();
            model.CurrentHorizontalResolution = video["CurrentHorizontalResolution"].ToString();
            model.CurrentNumberOfColors = video["CurrentNumberOfColors"].ToString();
            model.CurrentNumberOfColumns = video["CurrentNumberOfColumns"].ToString();
            model.CurrentNumberOfRows = video["CurrentNumberOfRows"].ToString();
            model.CurrentScanMode = video["CurrentScanMode"].ToString();
            model.CurrentVerticalResolution = video["CurrentVerticalResolution"].ToString();
            model.Description = video["Description"].ToString();
            model.DeviceID = video["DeviceID"].ToString();
            model.DriverVersion = video["DriverVersion"].ToString();
            model.InstalledDisplayDrivers = video["InstalledDisplayDrivers"].ToString();
            model.MaxRefreshRate = video["MaxRefreshRate"].ToString();
            model.MinRefreshRate = video["MinRefreshRate"].ToString();
            model.Name = video["Name"].ToString();
            model.PNPDeviceID = video["PNPDeviceID"].ToString();
            model.Status = video["Status"].ToString();
            model.SystemCreationClassName = video["SystemCreationClassName"].ToString();
            model.SystemName = video["SystemName"].ToString();
            model.VideoArchitecture = video["VideoArchitecture"].ToString();
            model.VideoMemoryType = video["VideoMemoryType"].ToString();
            model.VideoProcessor = video["VideoProcessor"].ToString();
            model.AdapterCompatibility = video["AdapterCompatibility"].ToString();
            model.AdapterDACType = video["AdapterDACType"].ToString();
            model.CurrentRefreshRate = video["CurrentRefreshRate"].ToString();
            model.DitherType = video["DitherType"].ToString();
            model.DriverDate = video["DriverDate"].ToString();
            model.InfFilename = video["InfFilename"].ToString();
            model.InfSection = video["InfSection"].ToString();
            model.Monochrome = video["Monochrome"].ToString();
            model.VideoModeDescription = video["VideoModeDescription"].ToString();
            list.Add(model);
        }

        return list;
    }

    /// <summary>
    ///     Returns this computers BIOS information as a BIOSModel object.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="EasException"></exception>
    public static BiosModel GetBIOS() {
        var model = new BiosModel();
        var bios = GetManagementObjList("Win32_BIOS").FirstOrDefault();
        model.BiosVersion = bios["BIOSVersion"].ToString();
        model.BiosCharacteristics = bios["BiosCharacteristics"].ToString();
        model.Caption = bios["Caption"].ToString();
        model.Description = bios["Description"].ToString();
        model.EmbeddedControllerMajorVersion = bios["EmbeddedControllerMajorVersion"].ToString();
        model.EmbeddedControllerMinorVersion = bios["EmbeddedControllerMinorVersion"].ToString();
        model.Manufacturer = bios["Manufacturer"].ToString();
        model.Name = bios["Name"].ToString();
        model.PrimaryBios = bios["PrimaryBIOS"].ToString();
        model.ReleaseDate = bios["ReleaseDate"].ToString();
        model.SerialNumber = bios["SerialNumber"].ToString();
        // model.SMBIOSBIOSVersion = bios["SMBIOSBIOSVersion"].ToString();
        // model.SMBIOSMajorVersion = bios["SMBIOSMajorVersion"].ToString();
        // model.SMBIOSMinorVersion = bios["SMBIOSMinorVersion"].ToString();
        // model.SMBIOSPresent = bios["SMBIOSPresent"].ToString();
        model.SoftwareElementId = bios["SoftwareElementID"].ToString();
        model.SoftwareElementState = bios["SoftwareElementState"].ToString();
        model.Status = bios["Status"].ToString();
        model.SystemBiosMajorVersion = bios["SystemBiosMajorVersion"].ToString();
        model.SystemBiosMinorVersion = bios["SystemBiosMinorVersion"].ToString();
        model.TargetOperatingSystem = bios["TargetOperatingSystem"].ToString();
        model.Version = bios["Version"].ToString();
        return model;
    }

    public static string GetTimezone() {
        return TimeZoneInfo.Local.StandardName;
    }

    public static string GetOSVersion() {
        return Environment.OSVersion.ToString();
    }

    public static string GetMachineName() {
        return Environment.MachineName;
    }

    public static string GetThreadId() {
        return Environment.CurrentManagedThreadId.ToString();
    }


    public static string GetRemoteIPAddress() {
        try {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://api.ipify.org/").Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        catch (Exception ex) {
            return "N/A";
        }
    }

    /// <summary>
    ///     Gets MachineGUID assigned by Windows.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="KeyNotFoundException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <exception cref="EasException"></exception>
    public static string? GetMachineGuid() {
        var location = @"SOFTWARE\Microsoft\Cryptography";
        var name = "MachineGuid";

        using var localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
        using var rk = localMachineX64View.OpenSubKey(location);
        if (rk == null)
            throw new KeyNotFoundException("Cannot find the key: " + location);
        var machineGuid = rk.GetValue(name);
        if (machineGuid == null)
            throw new IndexOutOfRangeException("Cannot find the value: " + name);
        return machineGuid.ToString()?.ToUpper();
    }

    /// <summary>
    ///     Returns Disk UUID from Win32_ComputerSystemProduct
    /// </summary>
    /// <returns></returns>
    public static string GetDiskUUID() {
        var run = "get-wmiobject Win32_ComputerSystemProduct  | Select-Object -ExpandProperty UUID";
        var oProcess = new Process();
        var oStartInfo = new ProcessStartInfo("powershell.exe", run);
        oStartInfo.UseShellExecute = false;
        oStartInfo.RedirectStandardInput = true;
        oStartInfo.RedirectStandardOutput = true;
        oStartInfo.CreateNoWindow = true;
        oProcess.StartInfo = oStartInfo;
        oProcess.Start();
        oProcess.WaitForExit();
        var result = oProcess.StandardOutput.ReadToEnd();
        return result.RemoveWhiteSpace().RemoveLineEndings();
    }

    public static NetworkInfoModel GetNetworkInfo_Client() {
        var resp = EasAPI.SendGetRequest("https://cloudflare.com/cdn-cgi/trace", null, 3);
        if (!resp.IsSuccessStatusCode) throw new Exception("Failed to get network info");
        var bodyText = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
        var bodyLines = bodyText.Split("\n");
        var ip = bodyLines[2].Split("=")[1];
        var loc = bodyLines[9].Split("=")[1];
        var warp = bodyLines[12].Split("=")[1].StringConversion<bool>();
        var gateway = bodyLines[13].Split("=")[1].StringConversion<bool>();
        return new NetworkInfoModel {
                                        IpAddress = ip,
                                        IsGatewayOn = gateway,
                                        IsWarpOn = warp,
                                        Location = loc
                                    };
    }

    #region Read System.Management

    private static string GetIdentifier(string wmiClass, string wmiProperty) {
        var result = "";
        var mc = new ManagementClass(wmiClass);
        var moc = mc.GetInstances();
        foreach (var mo in moc.Cast<ManagementObject>())
            //Only get the first one
            if (string.IsNullOrEmpty(result))
                try {
                    var prop = mo[wmiProperty];
                    if (prop != null)
                        result = prop.ToString();
                    break;
                }
                catch {
                }

        if (string.IsNullOrEmpty(result)) return "Unknown";
        return result;
    }


    private static List<ManagementObject> GetManagementObjList(string className, string searchCol = "*") {
        var list = new List<ManagementObject>();
        var searcher = new ManagementObjectSearcher("select " + searchCol + " from " + className);
        foreach (var obj in searcher.Get().Cast<ManagementObject>()) {
            if (obj == null)
                continue;
            list.Add(obj);
        }

        return list;
    }

    #endregion
}