using System.Text;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * void Html2Text(this StringBuilder sb);
         * void Text2Html(this StringBuilder sb);
         * void FixTextLine(this StringBuilder sb);
         * void FixTextBox(this StringBuilder sb);
         * void FixHtmlLine(this StringBuilder sb);
         * void FixHtmlBox(this StringBuilder sb);
         * 
         * string GetText2Html(this string source);
         * string GetHtml2Text(this string source);
         * string GetFixTextLine(this string source);
         * string GetFixTextBox(this string source);
         * string GetFixHtmlLine(this string source);
         * string GetFixHtmlBox(this string source);
         * string GetFixString(this string source);
         * string GetFixStringLo(this string source);
         * string GetFixStringName(this string source);
         * string TypografMin(this string source);
         */


        /* methods */


        public static void Html2Text(
            this StringBuilder sb)
        {
            _ = sb
                .Replace("&", "&amp;")
                //.Replace("&nbsp;", " ")
                .Replace(">", "&gt;")
                .Replace("<", "&lt;");
        }


        public static void Text2Html(
            this StringBuilder sb)
        {
            _ = sb.Replace("&amp;", "&")
                //.Replace("&nbsp;", " ")
                .Replace("&gt;", ">")
                .Replace("&lt;", "<");
        }


        public static void FixTextLine(
            this StringBuilder sb)
        {
            sb.ReplaceSpecChars();
            sb.ReplaceRecursively("  ", " ");
            sb.Trim(new char[] { ' ' });
        }


        public static void FixTextBox(
            this StringBuilder sb)
        {
            sb.Replace("\r", "");
            sb.ReplaceRecursively("  ", " ");
            sb.Replace(" \t", "\t");
            sb.Replace("\t ", "\t");
            sb.Replace(" \n", "\n");
            sb.Replace("\n ", "\n");
            sb.ReplaceRecursively("\n\n\n", "\n\n");
            sb.Trim(new char[] { '\n' });
        }


        public static void FixHtmlLine(
            this StringBuilder sb)
        {
            sb.FixTextLine();
            sb.Text2Html();
        }


        public static void FixHtmlBox(
            this StringBuilder sb)
        {
            sb.FixTextBox();
            sb.Text2Html();
        }


        /* functions */


        public static string GetText2Html(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.Text2Html();
            return sb1.ToString();
        }


        public static string GetHtml2Text(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.Html2Text();
            return sb1.ToString();
        }


        public static string GetFixTextLine(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.FixTextLine();
            return sb1.ToString();
        }


        public static string GetFixTextBox(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.FixTextBox();
            return sb1.ToString();
        }


        public static string GetFixHtmlLine(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.FixHtmlLine();
            return sb1.ToString();
        }


        public static string GetFixHtmlBox(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.FixHtmlBox();
            return sb1.ToString();
        }


        public static string GetFixString(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            return source.Trim();
        }


        public static string GetFixStringLo(
            this string source)
        {
            return source.GetFixString().ToLower();
        }


        public static string GetFixStringName(
            this string source)
        {
            var sb1 = new StringBuilder(source);
            sb1.ReplaceSpecChars();
            sb1.ReplaceRecursively("  ", " ");
            return sb1
                .GetTransformToStartWithACapital()
                .ToString()
                .GetFixString();
        }


        public static string TypografMin(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var a1 = source.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var sb1 = new StringBuilder();
            foreach (var w1 in a1.SkipLast(1))
                if (w1.Length < 4)
                    sb1.Append($"{w1}&nbsp;");
                else
                    sb1.Append($"{w1} ");
            sb1.Append(a1.Last());
            return sb1.ToString();
        }




        //public static void Typography(
        //	this StringBuilder sb)
        //{
        //	sb.ReplaceRecursively("  ", " ");
        //	SuppHtml.ReplaceHtmlMacros(sb);
        //	_ = sb
        //		.Replace(" .", ".").Replace(" ,", ",")
        //		.Replace(" :", ":").Replace(" ;", ";")
        //		.Replace(" - ", " — ").Replace(" -", "-").Replace("- ", "-")
        //		.Replace("« ", "«").Replace(" »", "»")
        //		.Replace("” ", "”").Replace(" “", "“")
        //		.Replace("„ ", "„")
        //		.Replace("( ", "(").Replace(" )", ")")
        //		.Replace("[ ", "[").Replace(" ]", "]")
        //		.Replace(" …", "…");
        //}

        //public static string TypographLine(
        //	this string source)
        //{
        //	if (string.IsNullOrEmpty(source))
        //		return string.Empty;
        //	var sb = new StringBuilder(source);
        //	return FixString(sb.ToString());
        //}

        //public static string TypographBox(
        //	string source)
        //{
        //	if (string.IsNullOrEmpty(source))
        //		return string.Empty;
        //	var sb = new StringBuilder(source);
        //	return FixString(sb.ToString());
        //}

    }

}
