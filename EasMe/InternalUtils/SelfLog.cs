
namespace EasMe
{
    internal static class SelfLog
    {
        internal static EasLog Logger { get; set; } = EasLogFactory.CreateLogger("EasMe.SelfLog");

    }
}
