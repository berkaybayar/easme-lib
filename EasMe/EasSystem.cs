using System.Net.NetworkInformation;
using System.Management;
namespace EasMe
{
    public static class EasSystem 
    {
        private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        private static ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");


        public static List<string> GetMACAdresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                                        .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                        .Select(nic => nic.GetPhysicalAddress().ToString()).ToList();
        }
        public static string GetMotherboardSerial()
        {
            var obj = motherboardSearcher.Get().OfType<ManagementObject>().FirstOrDefault()?.GetPropertyValue("SerialNumber");
            if (obj == null)
                return "";            
            return obj.ToString();
        }
        public static string GetMotherboardManufacturer()
        {
            var obj = motherboardSearcher.Get().OfType<ManagementObject>().FirstOrDefault()?.GetPropertyValue("Manufacturer");
            if (obj == null)
                return "";
            return obj.ToString();
        }
        public static string GetHWID()
        {
            var obj = baseboardSearcher.Get().OfType<ManagementObject>().FirstOrDefault()?.GetPropertyValue("SerialNumber");
            if (obj == null)
                return "";
            return obj.ToString();
        }
        public static string GetTimezone()
        {
            return TimeZoneInfo.Local.StandardName;

        }
        public static string GetOS()
        {
            try
            {
                var name = (from x in new ManagementObjectSearcher("SELECT Caption FROM Win32_OperatingSystem").Get()
                        .Cast<ManagementObject>()
                            select x.GetPropertyValue("Caption")).FirstOrDefault();
                return name != null ? name.ToString() : "Unknown";
            }
            catch
            {
                return "Unknown";
            }
        }
        public static string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }
        public static string GetMachineName()
        {
            return Environment.MachineName.ToString();
            
        }
        public static string GetThreadId()
        {
            return Environment.CurrentManagedThreadId.ToString();
        }
        public static string GetProcessorCount()
        {
            return Environment.ProcessorCount.ToString();
        }
        public static string? GetProcessorType()
        {
            return Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER");
        }
        public static string? GetProcessorSpeed()
        {
            return Environment.GetEnvironmentVariable("PROCESSOR_SPEED");
        }
        public static string? GetRAMSize()
        {
            ConnectionOptions connection = new ConnectionOptions();
            connection.Impersonation = ImpersonationLevel.Impersonate;
            ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
            scope.Connect();
            ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory"); ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query); foreach (ManagementObject queryObj in searcher.Get())
            {
                System.Diagnostics.Debug.WriteLine("-----------------------------------"); System.Diagnostics.Debug.WriteLine("Capacity: {0}", queryObj["Capacity"]); System.Diagnostics.Debug.WriteLine("MemoryType: {0}", queryObj["MemoryType"]);
            }
            return Environment.GetEnvironmentVariable("PROCESSOR_LEVEL");
        }
        public static string? GetRAMType()
        {
            return Environment.GetEnvironmentVariable("PROCESSOR_REVISION");
        }
        /// <summary>
        ///     Gets this device remote IP Address.
        /// </summary>
        /// <returns></returns>
        public static string GetClientRemoteIPAddress()
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync("https://api.ipify.org/").Result;
            return response.Content.ReadAsStringAsync().Result;
        }




    }
}
