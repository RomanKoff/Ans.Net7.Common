namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * string GetPluralizeEn(this string word);
         */


        public static string GetPluralizeEn(
            this string word)
        {
            // http://engblog.ru/plural-noun-table
            // ay = ays
            // ey = eys
            // oy = oys
            // y = ies
            // s = ses
            // sh = shes
            // ch = ches
            // x = xes
            // +s
            int l1 = word.Length;
            int l2 = l1 - 2;
            if (l1 > 2)
            {
                string s1 = word[l2..];
                switch (s1)
                {
                    case "ay": return string.Format("{0}ays", word[..l2]);
                    case "ey": return string.Format("{0}eys", word[..l2]);
                    case "oy": return string.Format("{0}oys", word[..l2]);
                }
                switch (s1[1])
                {
                    case 'y': return string.Format("{0}ies", word[..(l1 - 1)]);
                    case 's': return string.Format("{0}es", word);
                }
                switch (s1)
                {
                    case "sh":
                    case "ch": return string.Format("{0}es", word);
                }
                if (s1[1] == 'x')
                    return string.Format("{0}es", word);
            }
            return string.Format("{0}s", word);
        }



        /*
		public static string GetPluralizeEn(
			this string word)
		{
			var ps1 = PluralizationService.CreateService(new CultureInfo("en-gb"));
			return ps1.Pluralize(word);
		}
		*/

    }

}
