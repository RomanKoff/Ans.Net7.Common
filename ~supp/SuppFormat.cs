namespace Ans.Net7.Common
{

    public static class SuppFormat
    {

        /*
         * string ToCurrencyLoc(float amount);
         * string ToCurrencyLoc(double amount);
         * string ToCurrencyBuh(float amount);
         * string ToCurrencyBuh(double amount);
         * string ToPhone(string number);
         */


        public static string ToCurrencyLoc(
            float amount)
        {
            return string.Format("{0:N2}", amount);
        }


        public static string ToCurrencyLoc(
            double amount)
        {
            return string.Format("{0:N2}", amount);
        }


        public static string ToCurrencyBuh(
            float amount)
        {
            long h1 = (long)Math.Floor(amount);
            long h2 = ((long)Math.Round(amount * 100)) % 100;
            return string.Format("{0}={1:00}", h1, h2);
        }


        public static string ToCurrencyBuh(
            double amount)
        {
            long h1 = (long)Math.Floor(amount);
            long h2 = ((long)Math.Round(amount * 100)) % 100;
            return string.Format("{0}={1:00}", h1, h2);
        }


        public static string ToPhone(
            string number)
        {
            int l1 = number.Length;
            if (l1 > 11 || l1 < 5)
                return number;
            var stops1 = new int[] { l1 - 2, l1 - 4, l1 - 7, l1 - 10 };
            var a1 = new char[16];
            int p1 = 1;
            a1[0] = number[0];
            for (int i1 = 1; i1 < l1; i1++)
            {
                if (stops1.Contains(i1))
                {
                    a1[p1] = '-';
                    p1++;
                }
                a1[p1] = number[i1];
                p1++;
            }
            return new string(a1, 0, p1);
        }

    }

}
