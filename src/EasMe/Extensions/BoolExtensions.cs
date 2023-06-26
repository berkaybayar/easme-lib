namespace EasMe.Extensions;

public static class BoolExtensions
{
    public static bool ToBoolean(bool? value) {
        if (value == null) return false;
        return (bool)value;
    }
}