namespace EasMe.Extensions
{
    public static class NumberExtensions
    {
        public static bool IsBetween(this int value, int belowCheck, int aboveCheck) => value > belowCheck && value < aboveCheck;
        public static bool IsBetween(this long value, long belowCheck, long aboveCheck) => value > belowCheck && value < aboveCheck;
        public static bool IsBetween(this decimal value, decimal belowCheck, decimal aboveCheck) => value > belowCheck && value < aboveCheck;
        public static bool IsBetween(this float value, float belowCheck, float aboveCheck) => value > belowCheck && value < aboveCheck;
        public static bool IsBetweenOrEqual(this int value, int belowCheck, int aboveCheck) => value >= belowCheck && value <= aboveCheck;
        public static bool IsBetweenOrEqual(this long value, long belowCheck, long aboveCheck) => value >= belowCheck && value <= aboveCheck;
        public static bool IsBetweenOrEqual(this decimal value, decimal belowCheck, decimal aboveCheck) => value >= belowCheck && value <= aboveCheck;
        public static bool IsBetweenOrEqual(this float value, float belowCheck, float aboveCheck) => value >= belowCheck && value <= aboveCheck;
        public static bool IsInRange(this int value, int checkValue, int belowAndAboveCheck) => value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
        public static bool IsInRange(this long value, long checkValue, long belowAndAboveCheck) => value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
        public static bool IsInRange(this decimal value, decimal checkValue, decimal belowAndAboveCheck) => value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;
        public static bool IsInRange(this float value, float checkValue, float belowAndAboveCheck) => value > checkValue - belowAndAboveCheck && value < checkValue + belowAndAboveCheck;

        public static int ToNotNullInt(this int? value)
        {
            if(value is null) return default;
            return value.Value;
        }
		public static long ToNotNullLong(this long? value)
		{
			if (value is null) return default;
			return value.Value;
		}
	}
}
