using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;
namespace EasMe
{
    public class EasSystem
    {
        private static ManagementObjectSearcher baseboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_BaseBoard");
        private static ManagementObjectSearcher motherboardSearcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_MotherboardDevice");


        public List<string> GetMACAdresses()
        {
            return NetworkInterface.GetAllNetworkInterfaces()
                                        .Where(nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                                        .Select(nic => nic.GetPhysicalAddress().ToString()).ToList();
        }
        public string GetMotherboardSerial()
        {
            return motherboardSearcher.Get().OfType<ManagementObject>().FirstOrDefault()?.GetPropertyValue("SerialNumber").ToString();
        }
        
    }
}
