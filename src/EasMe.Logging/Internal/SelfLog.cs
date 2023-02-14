
namespace EasMe.Logging.Internal
{
    internal static class SelfLog
    {
        internal static IEasLog Logger { get; set; } = EasLogFactory.CreateLogger();

    }
}
