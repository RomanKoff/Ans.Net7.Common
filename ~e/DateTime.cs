namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
		 * DateTime StartOfWeek(this DateTime value, DayOfWeek startOfWeek);
		 * DateTime StartOfMonth(this DateTime value);
		 * int TotalMinutesInt(this TimeSpan value);
		 * bool IsEqual(this DateTime value1, DateTime value2, int minutesDiff);
		 * bool HasTime(this DateTime value);
		 * string GetSafeDate(this DateTime value);
		 * string GetSafeDateTime(this DateTime value);
		 * string GetSafeDateTimeForFile(this DateTime value);
		 * string GetSafeDateTimeForMin(this DateTime value)
		 */


        /// <summary>
        /// Возвращает дату ближайшего дня начала недели
        /// </summary>
        public static DateTime StartOfWeek(
            this DateTime value,
            DayOfWeek startOfWeek)
        {
            int diff1 = (7 + (value.DayOfWeek - startOfWeek)) % 7;
            return value.AddDays(-1 * diff1).Date;
        }


        /// <summary>
        /// Возвращает дату ближайшего дня начала месяца
        /// </summary>
        public static DateTime StartOfMonth(
            this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }


        /// <summary>
        /// Сравнивает значения даты и времени с точностью minutesDiff
        /// </summary>
        public static int TotalMinutesInt(
            this TimeSpan value)
        {
            return SuppMath.RoundToInt(Math.Abs(value.TotalMinutes));
        }


        /// <summary>
        /// Сравнивает значения даты и времени с точностью minutesDiff
        /// </summary>
        public static bool IsEqual(
            this DateTime value1,
            DateTime value2,
            int minutesDiff)
        {
            return Math.Abs((value1 - value2).TotalMinutes) < minutesDiff;
        }


        /// <summary>
        /// Определяет наличие данных о времени
        /// </summary>
        public static bool HasTime(
            this DateTime value)
        {
            return value.TimeOfDay != _Consts.DAY_START_SPAN;
        }


        /// <summary>
        /// Возвращает значение даты в виде строки в безопасном формате
        /// (yyyy-0MM-dd)
        /// </summary>
        public static string GetSafeDate(
            this DateTime value)
        {
            return value.ToString(
                _Consts.FORMAT_SAFE_DATE);
        }


        /// <summary>
        /// Возвращает значение даты и времени в виде строки в безопасном формате
        /// (yyyy-0MM-dd HH:mmmm:ss)
        /// </summary>
        public static string GetSafeDateTime(
            this DateTime value)
        {
            return value.ToString(
                _Consts.FORMAT_SAFE_DATETIME);
        }


        /// <summary>
        /// Возвращает значение даты и времени в виде строки в безопасном формате
        /// (yyyy-0MM-dd_HH-mmmm-ss) для именования файлов
        /// </summary>
        public static string GetSafeDateTimeForFile(
            this DateTime value)
        {
            return value.ToString(
                _Consts.FORMAT_SAFE_DATETIME_FILE);
        }


        /// <summary>
        /// Возвращает значение даты и времени в виде строки в безопасном формате
        /// (yyyy0MMddHHmmmmss) минимальной длины
        /// </summary>
        public static string GetSafeDateTimeForMin(
            this DateTime value)
        {
            return value.ToString(
                _Consts.FORMAT_SAFE_DATETIME_MIN);
        }

    }

}
