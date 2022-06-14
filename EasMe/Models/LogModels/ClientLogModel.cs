using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class ClientLogModel : BaseLogModel
    {
        public string? HWID { get; private set; } = EasSystem.GetHWID();
        public string? Ip { get; private set; } = EasSystem.GetClientRemoteIPAddress();
        public string? MachineName { get; private set; } = EasSystem.GetMachineName();
        public string? ThreadId { get; private set; } = EasSystem.GetThreadId();
        public string? OS { get; private set; } = EasSystem.GetOS();
        public string? OSVersion { get; private set; } = EasSystem.GetOSVersion();
        public string? Timezone { get; private set; } = EasSystem.GetTimezone();

    }
}
