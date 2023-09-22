namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * string GetString(this Dictionary<string, string> dictionary, string key, string defaultValue);
         * int GetInt(this Dictionary<string, string> dictionary, string key, int defaultValue);
         * string GetString(this Dictionary<int, string> dictionary, int key, string defaultValue);
         * int GetInt(this Dictionary<int, string> dictionary, int key, int defaultValue);
         */


        public static string GetString(
            this Dictionary<string, string> dictionary,
            string key,
            string defaultValue)
        {
            if (dictionary.TryGetValue(key, out string value))
                return value;
            return defaultValue;
        }


        public static int GetInt(
            this Dictionary<string, string> dictionary,
            string key,
            int defaultValue)
        {
            if (dictionary.TryGetValue(key, out string value))
                return value.ToInt(defaultValue);
            return defaultValue;
        }


        public static string GetString(
            this Dictionary<int, string> dictionary,
            int key,
            string defaultValue)
        {
            if (dictionary.TryGetValue(key, out string value))
                return value;
            return defaultValue;
        }


        public static int GetInt(
            this Dictionary<int, string> dictionary,
            int key,
            int defaultValue)
        {
            if (dictionary.TryGetValue(key, out string value))
                return value.ToInt(defaultValue);
            return defaultValue;
        }

    }

}
