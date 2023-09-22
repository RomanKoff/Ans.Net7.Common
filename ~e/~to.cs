namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * IEnumerable<IEnumerable<T>> ToGrid<T>(this IEnumerable<T> source, int count);
         * int[] ToIntArray(this string[] source);
         * int[] ToIntArray(this string source);
         * int ToIntRound(this double value);
         * int ToIntRound(this float value);
         * int ToIntRound(this decimal value);
         * int? ToInt(this string value);
         * int ToInt(this string value, int defaultValue);
         * uint? ToUInt(this string value);
         * uint ToUInt(this string value, uint defaultValue);
         * long? ToLong(this string value);
         * long ToLong(this string value, long defaultValue);
         * double? ToDouble(this string value);
         * double ToDouble(this string value, double defaultValue);
         * float? ToFloat(this string value);
         * float ToFloat(this string value, float defaultValue);
         * decimal? ToDecimal(this string value);
         * decimal ToDecimal(this string value, decimal defaultValue);
         * bool ToBool(this string value);
         * DateTime? ToDateTime(this string value);
         * DateTime ToDateTime(this string value, DateTime defaultValue);
         * DateTime ToDateTimeFromUniDate(this string value);
         * DateTime ToDateTimeFromSpecDate(this string value)
         */


        public static IEnumerable<IEnumerable<T>> ToGrid<T>(
            this IEnumerable<T> source,
            int count)
        {
            return source
                .Select((x, y) => new { Index = y, Value = x })
                .GroupBy(x => x.Index / count)
                .Select(x => x.Select(y => y.Value));
        }


        public static int[] ToIntArray(
            this string[] source)
        {
            return source.Select(x => x.ToInt(0)).ToArray();
        }


        public static int[] ToIntArray(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return null;
            return source.Split(_Consts.SPLIT_ITEMS).ToIntArray();
        }


        public static int ToIntRound(
            this double value)
        {
            return (int)(value + .5d);
        }


        public static int ToIntRound(
            this float value)
        {
            return (int)(value + .5f);
        }


        public static int ToIntRound(
            this decimal value)
        {
            return (int)(value + .5m);
        }


        public static int? ToInt(
            this string value)
        {
            if (int.TryParse(value, out int v1))
                return v1;
            return null;
        }


        public static int ToInt(
            this string value,
            int defaultValue)
        {
            if (int.TryParse(value, out int v1))
                return v1;
            return defaultValue;
        }


        public static uint? ToUInt(
            this string value)
        {
            if (uint.TryParse(value, out uint v1))
                return v1;
            return null;
        }


        public static uint ToUInt(
            this string value,
            uint defaultValue)
        {
            if (uint.TryParse(value, out uint v1))
                return v1;
            return defaultValue;
        }


        public static long? ToLong(
            this string value)
        {
            if (long.TryParse(value, out long v1))
                return v1;
            return null;
        }


        public static long ToLong(
            this string value,
            long defaultValue)
        {
            if (long.TryParse(value, out long v1))
                return v1;
            return defaultValue;
        }


        public static double? ToDouble(
            this string value)
        {
            if (double.TryParse(value, out double v1))
                return v1;
            return null;
        }


        public static double ToDouble(
            this string value,
            double defaultValue)
        {
            if (double.TryParse(value, out double v1))
                return v1;
            return defaultValue;
        }


        public static float? ToFloat(
            this string value)
        {
            if (float.TryParse(value, out float v1))
                return v1;
            return null;
        }


        public static float ToFloat(
            this string value,
            float defaultValue)
        {
            if (float.TryParse(value, out float v1))
                return v1;
            return defaultValue;
        }


        public static decimal? ToDecimal(
            this string value)
        {
            if (decimal.TryParse(value, out decimal v1))
                return v1;
            return null;
        }


        public static decimal ToDecimal(
            this string value,
            decimal defaultValue)
        {
            if (decimal.TryParse(value, out decimal v1))
                return v1;
            return defaultValue;
        }


        public static bool ToBool(
            this string value)
        {
            if (value == null || value.Length > 4)
                return false;
            if (value.Length < 4)
                return value == "1";
            return value == "true"
                || value == "True"
                || value == "TRUE";
        }


        public static DateTime? ToDateTime(
            this string value)
        {
            if (DateTime.TryParse(value, out DateTime v1))
                return v1;
            return null;
        }


        public static DateTime ToDateTime(
            this string value,
            DateTime defaultValue)
        {
            if (DateTime.TryParse(value, out DateTime v1))
                return v1;
            return defaultValue;
        }


        /// <summary>
        /// Возвращает DateTime из короткой строки даты в формате
        /// YYYY-MM-DD
        /// </summary>
        public static DateTime ToDateTimeFromUniDate(
            this string value)
        {
            return new DateTime(
                value[..4].ToInt(1),
                value[5..7].ToInt(1),
                value[8..10].ToInt(1));
        }


        /// <summary>
        /// Возвращает DateTime из короткой строки даты в формате
        /// YYYY-0MM-DD
        /// </summary>
        public static DateTime ToDateTimeFromSpecDate(
            this string value)
        {
            return new DateTime(
                value[..4].ToInt(1),
                value[5..8].ToInt(1),
                value[9..11].ToInt(1));
        }

    }

}
