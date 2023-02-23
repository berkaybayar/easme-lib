using EasMe.PostSharp.ExceptionAspects;

namespace EasMe.Test;

public static class Class1
{
    [ExceptionLogAspect]
    public static string Test()
    {
        throw new Exception("test;");
        return EasGenerate.GenerateGuidString();

    }
}