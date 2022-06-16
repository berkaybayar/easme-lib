using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class ClientLogModel : BaseLogModel
    {
        static EasSystem _system = new EasSystem();
        static GeoLocationModel loc = EasProxy.GetGeolocation();
        public string? Ip { get; private set ; } = _system.GetRemoteIPAddress();           
        public string? UniqueSystemId { get; private set; } = _system.GetUniqueSystemId();
        public string? MachineName { get; private set; } = _system.GetMachineName();
        public string? VideoCardName { get; private set; } = _system.GetVideoProcessorName();
        public string? ThreadId { get; private set; } = _system.GetThreadId();
        public string? OSVersion { get; private set; } = _system.GetOSVersion();
        public string? Timezone { get; private set; } = _system.GetTimezone();
        public string? Location { get; private set; } = loc.Ip + " " + loc.Longitude + " " + loc.Latitude + " " + loc.Accuracy;


    }
}
