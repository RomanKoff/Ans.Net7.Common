using System.Text;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
		 * void TranslitRuToEn(this StringBuilder sb);
		 * void FixUmlautRu(this StringBuilder sb);
		 * void FixNumberRu(this StringBuilder sb);
		 * 
		 * string GetTranslitRuToEn(this string source);
		 * string GetFixUmlautRu(this string source);
		 * string GetFixNumberRu(this string source);
		 */


        /* methods */


        public static void TranslitRuToEn(
            this StringBuilder sb)
        {
            // https://www.ifap.ru/library/gost/70342014.pdf
            if (sb.Length > 0)
                _ = sb
                    .Replace('а', 'a')
                    .Replace('б', 'b')
                    .Replace('в', 'v')
                    .Replace('г', 'g')
                    .Replace('д', 'd')
                    .Replace('е', 'e')
                    .Replace("ё", "yo")
                    .Replace("ж", "zh")
                    .Replace('з', 'z')
                    .Replace('и', 'i')
                    .Replace('й', 'y')
                    .Replace('к', 'k')
                    .Replace('л', 'l')
                    .Replace('м', 'm')
                    .Replace('н', 'n')
                    .Replace('о', 'o')
                    .Replace('п', 'p')
                    .Replace('р', 'r')
                    .Replace('с', 's')
                    .Replace('т', 't')
                    .Replace('у', 'u')
                    .Replace('ф', 'f')
                    .Replace("х", "kh")
                    .Replace("ц", "ts")
                    .Replace("ч", "ch")
                    .Replace("ш", "sh")
                    .Replace("щ", "shh")
                    .Replace("ъ", "")
                    .Replace('ы', 'y')
                    .Replace("ь", "")
                    .Replace('э', 'e')
                    .Replace("ю", "yu")
                    .Replace("я", "ya");
        }


        /// <summary>
        /// ё --> е
        /// </summary>
        public static void FixUmlautRu(
            this StringBuilder sb)
        {
            if (sb.Length > 0)
                _ = sb
                    .Replace('ё', 'е')
                    .Replace('Ё', 'Е');
        }


        /// <summary>
        /// № --> Nº
        /// </summary>
        public static void FixNumberRu(
            this StringBuilder sb)
        {
            if (sb.Length > 0)
                _ = sb
                    .Replace("№", "Nº");
        }


        /* functions */


        public static string GetTranslitRuToEn(
            this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            var sb1 = new StringBuilder(source);
            sb1.TranslitRuToEn();
            return sb1.ToString();
        }


        /// <summary>
        /// ё --> е
        /// </summary>
        public static string GetFixUmlautRu(
            this string source)
        {
            return source
                .Replace('ё', 'е')
                .Replace('Ё', 'Е'); ;
        }


        /// <summary>
        /// № --> Nº
        /// </summary>
        public static string GetFixNumberRu(
            this string source)
        {
            return source
                .Replace("№", "Nº");
        }

    }

}
