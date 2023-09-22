using System.Globalization;

namespace Ans.Net7.Common
{

    public static class SuppCulture
    {

        /*
         * void SetCulture(CultureInfo culture);
         * void SetCulture(string culture);
         */


        public static void SetCulture(
            CultureInfo culture)
        {
            Thread.CurrentThread.CurrentUICulture = culture;
            Thread.CurrentThread.CurrentCulture = CultureInfo
                .CreateSpecificCulture(culture.Name);
        }


        public static void SetCulture(
            string culture)
        {
            SetCulture(new CultureInfo(culture));
        }

    }

}
