namespace EasMe.Models.LogModels
{
    internal class ClientLogModel : BaseLogModel
    {
        static EasSystem _system = new EasSystem();
        public string Ip { get; private set; } = _system.GetRemoteIPAddress();
        public string HWID { get; private set; } = _system.GetMachineIdHashed();
        public string ThreadId { get; private set; } = _system.GetThreadId();
        public string OSVersion { get; private set; } = _system.GetOSVersion();
        public string Timezone { get; private set; } = _system.GetTimezone();


    }
}
