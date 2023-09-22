namespace Ans.Net7.Common
{

    public class FilterKeys
    {
        public IEnumerable<string> Keys { get; private set; }
        public bool UseOr { get; private set; }

        /// <param name="filter">
        /// '*' в начале строки — операция OR, иначе AND
        /// </param>
        public FilterKeys(
            string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                string s1;
                if (filter[0] == '*')
                {
                    s1 = filter[1..];
                    UseOr = true;
                }
                else
                    s1 = filter;
                Keys = s1.Split(new char[] { ',', ';' });
            }
        }
    }



    public class FilterIntKeys
    {
        public IEnumerable<int> Keys { get; private set; }
        public bool UseOr { get; private set; }

        /// <param name="filter">
        /// '*' в начале строки — операция OR, иначе AND
        /// </param>
        public FilterIntKeys(
            string filter)
        {
            var filter1 = new FilterKeys(filter);
            UseOr = filter1.UseOr;
            Keys = filter1.Keys
                .Select(x => x.ToInt(0))
                .ToArray();
        }
    }

}
