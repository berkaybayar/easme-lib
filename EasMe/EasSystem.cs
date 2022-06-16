using EasMe.Models.SystemModels;
using Microsoft.Win32;
using System.Diagnostics;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;

namespace EasMe
{
    public class EasSystem
    {
        //private  ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        //private  ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");

        enum HardwareType
        {
            EthernetMAC = 0,
            Baseboard = 1,
            CPU = 2,
            Disk = 3,
        }
        #region Read System.Management
        private string GetIdentifier(string wmiClass, string wmiProperty)
        {
            var result = "";

            try
            {
                ManagementClass mc =
            new ManagementClass(wmiClass);
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    //Only get the first one
                    if (string.IsNullOrEmpty(result))
                    {
                        try
                        {
                            var prop = mo[wmiProperty];
                            if (prop != null)
                                result = prop.ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(result)) return "Unknown";
            return result;

        }
        private string GetIdentifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string? result = "";

            try
            {
                ManagementClass mc =
            new ManagementClass(wmiClass);
                ManagementObjectCollection moc = mc.GetInstances();
                foreach (ManagementObject mo in moc)
                {
                    if (mo[wmiMustBeTrue].ToString() == "True")
                    {
                        //Only get the first one
                        if (!string.IsNullOrEmpty(result))
                        {
                            try
                            {
                                var prop = mo[wmiProperty];
                                if (prop != null)
                                    result = prop.ToString();
                                break;
                            }
                            catch
                            {
                            }
                        }
                    }
                }
            }
            catch
            {
            }
            if (string.IsNullOrEmpty(result)) return "Unknown";
            return result;

        }
        //private ManagementObject? GetManagementObj(string className, string searchCol = "*")
        //{            
        //    foreach (var item )
        //    {
        //        if (obj == null)
        //            continue;
        //        return obj;
        //    }
        //    return null;
        //}
        private List<ManagementObject> GetManagementObjList(string className, string searchCol = "*")
        {
            var list = new List<ManagementObject>();
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("select " + searchCol + " from " + className);
            foreach (ManagementObject obj in searcher.Get())
            {
                if (obj == null)
                    continue;
                list.Add(obj);
            }
            return list;
        }
        #endregion

        public string? GetMACAddress()
        {
            try
            {
                var macAddr = (
                                from nic in NetworkInterface.GetAllNetworkInterfaces()
                                where nic.OperationalStatus == OperationalStatus.Up
                                select nic.GetPhysicalAddress().ToString()
                            ).FirstOrDefault();
                return macAddr;
            }
            catch
            {
                return "Unkown";
            }
        }
        public List<RamModel> GetRamList()
        {
            var list = new List<RamModel>();
            foreach (var obj in GetManagementObjList("Win32_PhysicalMemory"))
            {
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
        public MotherboardModel GetMotherboard()
        {
            var motherboardModel = new MotherboardModel();
            
            try
            {
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
            }
            catch
            {
            }
            return motherboardModel;


        }
        public CPUModel GetProcessor()
        {
            var CPUModel = new CPUModel();            
            try
            {
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
            }
            catch
            {
            }
            return CPUModel;


        }
        public List<DiskModel> GetDiskList()
        {
            var list = new List<DiskModel>();
            try
            {
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
            }
            catch
            {
            }
            return list;

        }
        public List<GPUModel> GetGPUList()
        {
            var list = new List<GPUModel>();
            try
            {
                var GPUList = GetManagementObjList("Win32_VideoController");
                foreach(var video in GPUList)
                {
                    var model = new GPUModel();
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
            }
            catch
            {
            }
            return list;
        }
        public BIOSModel GetBIOS()
        {
            var model = new BIOSModel();            
            try
            {
                var bios = GetManagementObjList("Win32_BIOS").FirstOrDefault();
                model.BIOSVersion = bios["BIOSVersion"].ToString();
                model.BiosCharacteristics = bios["BiosCharacteristics"].ToString();
                model.Caption = bios["Caption"].ToString();
                model.Description = bios["Description"].ToString();
                model.EmbeddedControllerMajorVersion = bios["EmbeddedControllerMajorVersion"].ToString();
                model.EmbeddedControllerMinorVersion = bios["EmbeddedControllerMinorVersion"].ToString();
                model.Manufacturer = bios["Manufacturer"].ToString();
                model.Name = bios["Name"].ToString();
                model.PrimaryBIOS = bios["PrimaryBIOS"].ToString();
                model.ReleaseDate = bios["ReleaseDate"].ToString();
                model.SerialNumber = bios["SerialNumber"].ToString();
                model.SMBIOSBIOSVersion = bios["SMBIOSBIOSVersion"].ToString();
                model.SMBIOSMajorVersion = bios["SMBIOSMajorVersion"].ToString();
                model.SMBIOSMinorVersion = bios["SMBIOSMinorVersion"].ToString();
                model.SMBIOSPresent = bios["SMBIOSPresent"].ToString();
                model.SoftwareElementID = bios["SoftwareElementID"].ToString();
                model.SoftwareElementState = bios["SoftwareElementState"].ToString();
                model.Status = bios["Status"].ToString();
                model.SystemBiosMajorVersion = bios["SystemBiosMajorVersion"].ToString();
                model.SystemBiosMinorVersion = bios["SystemBiosMinorVersion"].ToString();
                model.TargetOperatingSystem = bios["TargetOperatingSystem"].ToString();
                model.Version = bios["Version"].ToString();
            }
            catch
            {
            }
            return model;
        }
        public string GetMotherboardSerial()
        {
            try
            {
                var serial = GetIdentifier("Win32_BaseBoard", "SerialNumber");
                return serial;
            }
            catch
            {
            }
            return "Unkown";
        }
        public string GetProcessorId()
        {
            try
            {
                var id = GetIdentifier("Win32_Processor", "ProcessorId");
                return id;
            }
            catch
            {
            }
            return "Unkown";
        }
        public string GetDiskSerial()
        {
            try
            {
                var id = GetIdentifier("Win32_DiskDrive", "SerialNumber");
                return id;
            }
            catch
            {
            }
            return "Unkown";
        }
        public string GetVideoProcessorName()
        {
            try
            {
                var id = GetIdentifier("Win32_VideoController", "VideoProcessor");
                return id;
            }
            catch
            {
            }
            return "Unkown";
        }
        public string GetTimezone()
        {
            return TimeZoneInfo.Local.StandardName;

        }
        public string GetOSVersion()
        {
            try
            {
                return Environment.OSVersion.ToString();
            }
            catch
            {
                return "Unknown";
            }
        }
        public string GetMachineName()
        {
            try
            {
                return Environment.MachineName.ToString();
            }
            catch
            {
                return "Unknown";
            }

        }
        public string GetThreadId()
        {
            try
            {
                return Environment.CurrentManagedThreadId.ToString();
            }
            catch
            {
                return "Unknown";
            }
        }

        /// <summary>
        ///     Gets this device remote IP Address.
        /// </summary>
        /// <returns></returns>
        public string GetRemoteIPAddress()
        {
            try
            {
                var httpClient = new HttpClient();
                var response = httpClient.GetAsync("https://api.ipify.org/").Result;
                return response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                return "Unknown";
            }

        }

        private string GetMachineGuid()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
            using RegistryKey rk = localMachineX64View.OpenSubKey(location);
            if (rk == null)
                return "KeyNotFoundException";
            object machineGuid = rk.GetValue(name);
            if (machineGuid == null)
                return "IndexOutOfRangeException";

            return machineGuid.ToString().ToUpper();
        }

        /// <summary>
        /// Get Disk UUID from Win32_ComputerSystemProduct
        /// </summary>
        /// <returns></returns>
        private string GetDiskUUID()
        {
            string run = "get-wmiobject Win32_ComputerSystemProduct  | Select-Object -ExpandProperty UUID";
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
            return result.Trim();
        }



        //bios versions can be updated to a new version and make users licence invalid  
        //Also since Rams and Disks and GPUs can have multiple, changing ram or disk or gpu order may result in differenet id        
        private string GetMachineIdString(bool EnableBiosVersionIdentifier = true)
        {
            try
            {
                var processor = GetProcessor();
                var processorIdentifier = "";
                if (processor != null)
                    processorIdentifier = $"{processor.Name}:{processor.Manufacturer}:{processor.ProcessorId}";
                //Disabled ram id  
                var ramIdentifier = "";
                if (false)
                {
                    var ramList = GetRamList();
                    if (ramList != null)
                        ramIdentifier = string.Join(";", ramList.Select(x => $"{x.Name}:{x.Manufacturer}:{x.SerialNumber}"));
                }
                var bios = GetBIOS();
                var biosIdentifier = $"{bios.Manufacturer}:{bios.SMBIOSBIOSVersion}:{bios.SerialNumber}";
                if (EnableBiosVersionIdentifier)
                    biosIdentifier += $":{bios.ReleaseDate}:{bios.Version}";
                var mainboard = GetMotherboard();
                var mainboardIdentifier = $"{mainboard.Name}:{mainboard.Manufacturer}:{mainboard.SerialNumber}";
                var gpuList = GetGPUList();
                var gpuIdentifier = string.Join(";", gpuList.Select(x => $"{x.Name}"));
                var diskList = GetDiskList();
                var diskIdentifier = string.Join(";", diskList.Select(x => $"{x.Name}:{x.Manufacturer}:{x.SerialNumber}:{x.Size}"));
                var machineName = GetMachineName();
                var ethernetMac = GetMACAddress();
                string id = processorIdentifier + "|" + ramIdentifier + "|" + biosIdentifier + "|" + mainboardIdentifier + "|" + gpuIdentifier + "|" 
                    + diskIdentifier + "|" + machineName + "|" + ethernetMac + "|" + GetDiskUUID() + "|" + GetMachineGuid();
                //Removing Whitespace
                return id.Replace(" ","");
            }
            catch { return "Unkown"; }
        }
        public string GetMachineIdHashed()
        {
            try
            {
                return EasHash.Sha256Hash(GetMachineIdString());
            }
            catch { return "Unkown"; }
        }



        private string? GetTest()
        {
            try
            {
                var val = "";
                ManagementObjectSearcher MOS = new ManagementObjectSearcher("Select * From Win32_DiskDrive");
                foreach (ManagementObject item in MOS.Get())
                {
                    if (item == null) continue;
                    foreach (var a in item.Properties)
                    {
                        if (a == null || a.Name == null || a.Value == null) continue;
                        val += a.Name + ":" + a.Value.ToString() + "|";
                    }

                    return val.Substring(0, val.Length - 1);
                }
            }
            catch
            {
            }
            return null;

        }



        /// <summary>
        /// Generates a Guid based on the current computer hardware
        /// Example: C384B159-8E36-6C85-8ED8-6897486500FF
        /// </summary>        
        public static string Value()
        {
            var lCpuId = GetCpuId();
            var lBiodId = GetBiosId();
            var lMainboard = GetMainboardId();
            var lGpuId = GetGpuId();
            var lMac = GetMac();
            var lConcatStr = $"CPU:{lCpuId}|BIOS:{lBiodId}|Mainboard:{lMainboard}|GPU:{lGpuId}|MAC:{lMac}";
            return Hash(lConcatStr);
            string Hash(string s)
            {
                try
                {
                    var lProvider = new MD5CryptoServiceProvider();
                    var lUtf8 = lProvider.ComputeHash(ASCIIEncoding.UTF8.GetBytes(s));
                    return new Guid(lUtf8).ToString().ToUpper();
                }
                catch (Exception lEx)
                {
                    return lEx.Message;
                }
            }
        }


        #region Original Device ID Getting Code

        //Return a hardware identifier
        private static string GetIdentifier(string pWmiClass, List<string> pProperties)
        {
            string lResult = string.Empty;
            try
            {
                foreach (ManagementObject lItem in new ManagementClass(pWmiClass).GetInstances())
                {
                    foreach (var lProperty in pProperties)
                    {
                        try
                        {
                            switch (lProperty)
                            {
                                case "MACAddress":
                                    if (string.IsNullOrWhiteSpace(lResult) == false)
                                        return lResult; //Return just the first MAC

                                    if (lItem["IPEnabled"].ToString() != "True")
                                        continue;
                                    break;
                            }

                            var lItemProperty = lItem[lProperty];
                            if (lItemProperty == null)
                                continue;

                            var lValue = lItemProperty.ToString();
                            if (string.IsNullOrWhiteSpace(lValue) == false)
                                lResult += $"{lValue}; ";
                        }
                        catch { }
                    }

                }
            }
            catch { }
            return lResult.TrimEnd(' ', ';');
        }
        
        private static List<string> ListOfCpuProperties = new List<string> { "UniqueId", "ProcessorId", "Name", "Manufacturer" };

        public static string GetCpuId()
        {
            return GetIdentifier("Win32_Processor", ListOfCpuProperties);
        }

        private static List<string> ListOfBiosProperties = new List<string> { "Manufacturer", "SMBIOSBIOSVersion", "IdentificationCode", "SerialNumber", "ReleaseDate", "Version" };
        //BIOS Identifier
        private static string GetBiosId()
        {
            return GetIdentifier("Win32_BIOS", ListOfBiosProperties);
        }

        private static List<string> ListOfMainboardProperties = new List<string> { "Model", "Manufacturer", "Name", "SerialNumber" };
        //Motherboard ID
        private static string GetMainboardId()
        {
            return GetIdentifier("Win32_BaseBoard", ListOfMainboardProperties);
        }

        private static List<string> ListOfGpuProperties = new List<string> { "Name" };
        //Primary video controller ID
        private static string GetGpuId()
        {
            return GetIdentifier("Win32_VideoController", ListOfGpuProperties);
        }

        private static List<string> ListOfNetworkProperties = new List<string> { "MACAddress" };
        private static string GetMac()
        {
            return GetIdentifier("Win32_NetworkAdapterConfiguration", ListOfNetworkProperties);
        }

        #endregion


    }
}
