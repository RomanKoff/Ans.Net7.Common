namespace Ans.Net7.Common
{

    public static class SuppDateTime
    {

        /*
         * IEnumerable<DateTime> GetDays(DateTime start, DateTime end);
         * DateTime Max(DateTime value1, DateTime? value2);
         * DateTime Min(DateTime value1, DateTime? value2);
         */


        /// <summary>
        /// Возвращает перечень дней между от start до end
        /// </summary>
        public static IEnumerable<DateTime> GetDays(
            DateTime start,
            DateTime end)
        {
            return Enumerable.Range(0, end.Date.Subtract(start.Date).Days + 1)
                .Select(x => start.Date.AddDays(x))
                .ToArray();
        }


        public static DateTime Max(
            DateTime value1,
            DateTime? value2)
        {
            if (value2 == null)
                return value1;
            return (value1 > value2) ? value1 : value2.Value;
        }


        public static DateTime Min(
            DateTime value1,
            DateTime? value2)
        {
            if (value2 == null)
                return value1;
            return (value1 < value2) ? value1 : value2.Value;
        }

    }

}
