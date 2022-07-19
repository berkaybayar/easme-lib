
namespace EasMe
{
    internal static class SelfLog
    {
        internal static EasLog Logger { get; set; } = IEasLog.CreateLogger("EasMe.SelfLogging");

    }
}
