namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * string[] SplitFix(this string source, char separator, int count);
         * IEnumerable<string> Trim(this IEnumerable<string> source);
         * IEnumerable<string> RemoveEmpty(this IEnumerable<string> source);
         */


        public static string[] SplitFix(
            this string source,
            char separator,
            int count)
        {
            var a1 = source.Split(separator);
            if (a1.Length >= count)
                return a1.Take(count).ToArray();
            var a2 = new string[count];
            foreach (var (item1, index1) in a1.WithIndex())
                a2[index1] = item1;
            return a2;
        }


        public static IEnumerable<string> Trim(
            this IEnumerable<string> source)
        {
            return source.Select(x => x.Trim());
        }


        public static IEnumerable<string> RemoveEmpty(
            this IEnumerable<string> source)
        {
            return source.Where(x => !string.IsNullOrEmpty(x));
        }

    }

}