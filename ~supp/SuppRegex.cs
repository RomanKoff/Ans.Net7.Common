namespace Ans.Net7.Common
{

    public static partial class SuppRegex
    {

        /*
		 * string ParamEncode(string source)
		 */


        /// <summary>
        /// Кодировка спецсимволов для regex-выражений
        /// </summary>
        public static string ParamEncode(
            string source)
        {
            return _Consts.G_REGEX_ENCODE().Replace(source, @"\$&");
        }





        /*
		 public static string ParamEncode(
			string source)
		{
			return Regex.Replace(source, @"[]^\\-]", @"\$&");
		}
		 */

    }

}
