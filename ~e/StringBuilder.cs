using System.Text;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*		 
		 * void InsertFormat(this StringBuilder sb, int index, string template, params string[] args);
		 * void InsertIf(this StringBuilder sb, bool expression, int index, string template, params string[] args);
		 * void InsertIfPresent(this StringBuilder sb, int index, string value);
		 * void InsertIfFormat(this StringBuilder sb, bool expression, int index, string template, params string[] args);
		 * void AppendIf(this StringBuilder sb, bool expression, string value);
		 * void AppendIfPresent(this StringBuilder sb, string value);
		 * void AppendIfFormat(this StringBuilder sb, bool expression, string template, params string[] args);
		 * void Trim(this StringBuilder sb);
		 * void Trim(this StringBuilder sb, char[] chars);
		 * void ReplaceSpecChars(this StringBuilder sb);
		 * 
		 * int IndexOf(this StringBuilder sb, string value, int startIndex, bool ignoreCase);
		 * StringBuilder ReplaceRecursively(this StringBuilder sb, string oldText, string newText);
		 * StringBuilder GetTransformToLower(this StringBuilder sb);
		 * StringBuilder GetTransformToUpper(this StringBuilder sb);
		 * StringBuilder GetTransformToStartWithACapital(this StringBuilder sb);
		 * StringBuilder GetTransformToSafeText(this StringBuilder sb)
		 */


        /* methods */


        public static void InsertFormat(
            this StringBuilder sb,
            int index,
            string template,
            params string[] args)
        {
            sb.Insert(index, string.Format(template, args));
        }


        public static void InsertIf(
            this StringBuilder sb,
            bool expression,
            int index,
            string template,
            params string[] args)
        {
            if (expression)
                sb.Insert(index, string.Format(template, args));
        }


        public static void InsertIfPresent(
            this StringBuilder sb,
            int index,
            string value)
        {
            if (!string.IsNullOrEmpty(value))
                sb.Insert(index, value);
        }


        public static void InsertIfFormat(
            this StringBuilder sb,
            bool expression,
            int index,
            string template,
            params string[] args)
        {
            if (expression)
                sb.InsertFormat(index, string.Format(template, args));
        }


        public static void AppendIf(
            this StringBuilder sb,
            bool expression,
            string value)
        {
            if (expression)
                sb.Append(value);
        }


        public static void AppendIfPresent(
            this StringBuilder sb,
            string value)
        {
            if (!string.IsNullOrEmpty(value))
                sb.Append(value);
        }


        public static void AppendIfFormat(
            this StringBuilder sb,
            bool expression,
            string template,
            params string[] args)
        {
            if (expression)
                sb.AppendFormat(template, args);
        }


        public static void Trim(
            this StringBuilder sb)
        {
            if (sb.Length > 0)
            {
                int c1 = 0;
                for (var i1 = 0; i1 < sb.Length; i1++)
                {
                    if (!char.IsWhiteSpace(sb[i1]))
                        break;
                    c1++;
                }
                if (c1 > 0)
                {
                    sb.Remove(0, c1);
                    c1 = 0;
                }
                for (var i1 = sb.Length - 1; i1 >= 0; i1--)
                {
                    if (!char.IsWhiteSpace(sb[i1]))
                        break;
                    c1++;
                }
                if (c1 > 0)
                    sb.Remove(sb.Length - c1, c1);
            }
        }


        public static void Trim(
            this StringBuilder sb,
            char[] chars)
        {
            if (sb.Length > 0)
            {
                int c1 = 0;
                for (var i1 = 0; i1 < sb.Length; i1++)
                {
                    if (chars.Contains(sb[i1]))
                        break;
                    c1++;
                }
                if (c1 > 0)
                {
                    sb.Remove(0, c1);
                    c1 = 0;
                }
                for (var i1 = sb.Length - 1; i1 >= 0; i1--)
                {
                    if (chars.Contains(sb[i1]))
                        break;
                    c1++;
                }
                if (c1 > 0)
                    sb.Remove(sb.Length - c1, c1);
            }
        }


        public static void ReplaceSpecChars(
            this StringBuilder sb)
        {
            for (int i1 = 0; i1 < sb.Length; ++i1)
                if (sb[i1] < 32)
                    sb[i1] = ' ';
        }


        /* functions */


        /// <summary>
        /// Возвращает индекс начала вхождения строки
        /// </summary>
        public static int IndexOf(
            this StringBuilder sb,
            string value,
            int startIndex,
            bool ignoreCase)
        {
            int l1 = value.Length;
            int max1 = (sb.Length - l1) + 1;
            var v1 = (ignoreCase)
                ? value.ToLower() : value;
            var func1 = (ignoreCase)
                ? new Func<char, char, bool>((x, y) => char.ToLower(x) == y)
                : new Func<char, char, bool>((x, y) => x == y);
            for (int i1 = startIndex; i1 < max1; ++i1)
                if (func1(sb[i1], v1[0]))
                {
                    int i2 = 1;
                    while ((i2 < l1) && func1(sb[i1 + i2], v1[i2]))
                        ++i2;
                    if (i2 == l1)
                        return i1;
                }
            return -1;
        }


        public static StringBuilder ReplaceRecursively(
            this StringBuilder sb,
            string oldText,
            string newText)
        {
            int i1 = sb.IndexOf(oldText, 0, false);
            if (i1 < 0)
                return sb;
            return sb
                .Replace(oldText, newText)
                .ReplaceRecursively(oldText, newText);
        }


        public static StringBuilder GetTransformToLower(
            this StringBuilder sb)
        {
            return new StringBuilder(
                sb.ToString().ToLower());
        }


        public static StringBuilder GetTransformToUpper(
            this StringBuilder sb)
        {
            return new StringBuilder(
                sb.ToString().ToUpper());
        }


        public static StringBuilder GetTransformToStartWithACapital(
            this StringBuilder sb)
        {
            return new StringBuilder(
                sb.ToString().GetStartWithACapital());
        }


        /// <summary>
        /// Возвращает результат преобразования символов с кодом меньше 33 в '_' и больше 126 в их кодовый эквивалент
        /// </summary>
        public static StringBuilder GetTransformToSafeText(
            this StringBuilder sb)
        {
            var sb2 = new StringBuilder();
            for (int i1 = 0; i1 < sb.Length; ++i1)
            {
                char ch1 = sb[i1];
                int code1 = ch1;
                if (code1 < 33)
                    sb2.Append('_');
                else if (code1 > 126)
                    sb2.AppendFormat("^{0}", code1);
                else
                    sb2.Append(ch1);
            }
            return sb2;
        }

    }

}
