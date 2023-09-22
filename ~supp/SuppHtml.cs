using System.Text;
using System.Xml.Linq;

namespace Ans.Net7.Common
{

    public static class SuppHtml
    {

        /*
         * string EscapeHtml(string source);
         * string MakeIndents(string source);
         * string ReplaceMacros(string source);
         */


        public static string EscapeHtml(
            string source)
        {
            return source
                .Replace("&", "&amp;")
                .Replace("<", "&lt;")
                .Replace(">", "&gt;");
        }


        public static string MakeIndents(
            string source)
        {
            var x1 = XDocument.Parse(source);
            return x1.ToString(SaveOptions.OmitDuplicateNamespaces);
        }


        public static string ReplaceMacros(
            string source)
        {
            var sb1 = new StringBuilder(source);
            _ = sb1
                .Replace("<<<^", "”")
                .Replace("<<<", "„")
                .Replace(">>>", "“")
                .Replace("<<", "«")
                .Replace(">>", "»")
                .Replace("<--", "←")
                .Replace("-->", "→")
                .Replace("<==", "⇐")
                .Replace("==>", "⇒")
                .Replace("--", "—")
                .Replace("-^", "–")
                .Replace("...", "…");
            return sb1.ToString()
                .ReplaceKeysFromDict(_Consts.HTML_MACROS, '&');
        }

    }

}
