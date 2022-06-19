using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasMe.Models.LogModels
{
    internal class ClientLogModel
    {
        public string? Ip { get; private set ; } = EasSystem.GetRemoteIPAddress();           
        public string? HWID { get; private set; } = EasSystem.GetMachineIdHashed();
        public string? ThreadId { get; private set; } = EasSystem.GetThreadId();
        public string? OSVersion { get; private set; } = EasSystem.GetOSVersion();
        public string? Timezone { get; private set; } = EasSystem.GetTimezone();


    }
}
