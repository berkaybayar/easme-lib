using EasMe.Logging;

namespace EasMe.Test;

public class Class1
{
    public static readonly IEasLog logger = EasLogFactory.CreateLogger();

    public static void Test()
    {
        logger.Info(1213);
    }
}