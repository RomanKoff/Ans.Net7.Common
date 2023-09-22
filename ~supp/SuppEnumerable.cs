namespace Ans.Net7.Common
{

    public static class SuppEnumerable
    {

        /*
         * IEnumerable<T> Matching<T>(IEnumerable<T> current, IEnumerable<T> newest, Func<T, T, bool> compare);
         * IEnumerable<T> Removed<T>(IEnumerable<T> current, IEnumerable<T> newest, Func<T, T, bool> compare);
         * IEnumerable<T> Added<T>(IEnumerable<T> current, IEnumerable<T> newest, Func<T, T, bool> compare);
         */



        /// <summary>
        /// Matching or Updated
        /// </summary>
        public static IEnumerable<T> Matching<T>(
            IEnumerable<T> current,
            IEnumerable<T> newest,
            Func<T, T, bool> compare)
        {
            return
                from c1 in current
                from n1 in newest
                where compare(c1, n1)
                select n1;
        }


        public static IEnumerable<T> Removed<T>(
            IEnumerable<T> current,
            IEnumerable<T> newest,
            Func<T, T, bool> compare)
        {
            return
                from c1 in current
                where !newest.Any(n1 => compare(c1, n1))
                select c1;
        }


        public static IEnumerable<T> Added<T>(
            IEnumerable<T> current,
            IEnumerable<T> newest,
            Func<T, T, bool> compare)
        {
            return
                from n1 in newest
                where !current.Any(c1 => compare(c1, n1))
                select n1;
        }

    }

}
