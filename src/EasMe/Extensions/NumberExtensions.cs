namespace EasMe.Extensions;

public static class NumberExtensions {
    public static bool IsBetween(this int value, int belowCheck, int aboveCheck) {
        return value > belowCheck && value < aboveCheck;
    }

    public static bool IsBetween(this long value, long belowCheck, long aboveCheck) {
        return value > belowCheck && value < aboveCheck;
    }

    public static bool IsBetween(this decimal value, decimal belowCheck, decimal aboveCheck) {
        return value > belowCheck && value < aboveCheck;
    }

    public static bool IsBetween(this float value, float belowCheck, float aboveCheck) {
        return value > belowCheck && value < aboveCheck;
    }

    public static bool IsBetweenOrEqual(this int value, int belowCheck, int aboveCheck) {
        return value >= belowCheck && value <= aboveCheck;
    }

    public static bool IsBetweenOrEqual(this long value, long belowCheck, long aboveCheck) {
        return value >= belowCheck && value <= aboveCheck;
    }

    public static bool IsBetweenOrEqual(this decimal value, decimal belowCheck, decimal aboveCheck) {
        return value >= belowCheck && value <= aboveCheck;
    }

    public static bool IsBetweenOrEqual(this float value, float belowCheck, float aboveCheck) {
        return value >= belowCheck && value <= aboveCheck;
    }

    public static bool IsInRange(this int value, int checkValue, int belowAndAboveCheck) {
        return value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
    }

    public static bool IsInRange(this long value, long checkValue, long belowAndAboveCheck) {
        return value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
    }

    public static bool IsInRange(this decimal value, decimal checkValue, decimal belowAndAboveCheck) {
        return value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
    }

    public static bool IsInRange(this float value, float checkValue, float belowAndAboveCheck) {
        return value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
    }

    public static int ToNotNullInt(this int? value) {
        if (value is null) return default;
        return value.Value;
    }

    public static long ToNotNullLong(this long? value) {
        if (value is null) return default;
        return value.Value;
    }
}