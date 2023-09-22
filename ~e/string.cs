using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
		 * string ReplaceKeysFromDict(this string source, Dictionary<string, string> dictionary, char marker = '#');
		 * string ScreeningReg(this string source);
		 * string ReScreeningReg(this string source);
		 * string TrimEnd(this string source, string trimString);
		 * string GetFirstUpper(this string source, bool forcedToLower);
		 * string GetFirstLower(this string source, bool forcedToUpper);
		 * string GetStartWithACapital(this string source, CultureInfo cultureInfo);
		 * string GetStartWithACapital(this string source);
		 * string GetLeft(this string source, char find);
		 * string GetLeft(this string source, string find);
		 * string GetLeft(this string source, int count);
		 * string GetRight(this string source, char find);
		 * string GetRight(this string source, string find);
		 * string GetRight(this string source, int count);
		 * string GetBackTo(this string source, char find, int skip = 0);
		 * string GetCrop(this string source, int startIndex, int length, string beginCropMask, string endCropMask);
		 * string GetSafeText(this string source);
		 * string GetTag(this string source, string before, string after, StringComparison comparisonType = StringComparison.InvariantCulture);
		 * string GetTagAndCut(this string source, string before, string after, StringComparison comparisonType = StringComparison.InvariantCulture);
		 * string GetReplaceRecursively(this string source, string oldText, string newText);
		 * string GetReplaceSpecChars(this string source);
		 */


        public static string ReplaceKeysFromDict(
            this string source,
            Dictionary<string, string> dictionary,
            char marker = '#')
        {
            var sb1 = new StringBuilder();
            for (int i1 = 0; i1 < source.Length; i1++)
            {
                var c1 = source[i1];
                if (c1 != marker)
                {
                    sb1.Append(c1);
                    continue;
                }
                int i2 = i1 + 1;
                int l1 = source.Length - i2 + 1;
                bool notFound1 = true;
                foreach (var item1 in dictionary)
                    if (item1.Key.Length < l1)
                    {
                        bool isMatch1 = true;
                        for (int i3 = 0; i3 < item1.Key.Length; i3++)
                        {
                            if (item1.Key[i3] != source[i2 + i3])
                            {
                                isMatch1 = false;
                                break;
                            }
                        }
                        if (isMatch1)
                        {
                            notFound1 = false;
                            sb1.Append(item1.Value);
                            i1 += item1.Key.Length;
                            break;
                        }
                    }
                if (notFound1)
                    sb1.Append(c1);
            }
            return sb1.ToString();
        }


        public static string ScreeningReg(
            this string source)
        {
            return source.Replace("\\;", "&#59;").Replace("\\=", "&#61;");
        }


        public static string ReScreeningReg(
            this string source)
        {
            return source.Replace("&#59;", ";").Replace("&#61;", "=");
        }


        public static string TrimEnd(
            this string source,
            string trimString)
        {
            var s1 = SuppRegex.ParamEncode(trimString);
            return Regex.Replace(source, $"(?:{s1})+", string.Empty);
        }


        public static string GetFirstUpper(
            this string source,
            bool forcedToLower)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int l1 = source.Length;
            if (l1 == 1)
                return source.ToUpper();
            string s1 = (forcedToLower)
                ? source[1..].ToLower()
                : source[1..];
            return $"{char.ToUpper(source[0])}{s1}";
        }


        public static string GetFirstLower(
            this string source,
            bool forcedToUpper)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int l1 = source.Length;
            if (l1 == 1)
                return source.ToLower();
            string s1 = (forcedToUpper)
                ? source[1..].ToUpper()
                : source[1..];
            return $"{char.ToLower(source[0])}{s1}";
        }


        public static string GetStartWithACapital(
            this string source,
            CultureInfo cultureInfo)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            return cultureInfo.TextInfo.ToTitleCase(source);
        }


        public static string GetStartWithACapital(
            this string source)
        {
            return source.GetStartWithACapital(
                CultureInfo.CurrentCulture);
        }


        /// <summary>
        /// Возвращает левую часть сторки до символа 'find'
        /// </summary>
        public static string GetLeft(
            this string source,
            char find)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = source.IndexOf(find);
            if (i1 == -1)
                return string.Empty;
            return source[..i1];
        }


        /// <summary>
        /// Возвращает левую часть сторки до подстроки 'find'
        /// </summary>
        public static string GetLeft(
            this string source,
            string find)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = source.IndexOf(find);
            if (i1 == -1)
                return string.Empty;
            return source[..i1];
        }


        /// <summary>
        /// Возвращает левую часть сторки из 'count' символов
        /// </summary>
        public static string GetLeft(
            this string source,
            int count)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int l1 = source.Length;
            if (l1 <= count)
                return source;
            return source[..count];
        }


        /// <summary>
        /// Возвращает правую часть сторки до символа 'find'
        /// </summary>
        public static string GetRight(
            this string source,
            char find)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = source.LastIndexOf(find);
            if (i1 == -1)
                return string.Empty;
            return source[(i1 + 1)..];
        }


        /// <summary>
        /// Возвращает правую часть сторки до подстроки 'find'
        /// </summary>
        public static string GetRight(
            this string source,
            string find)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = source.LastIndexOf(find);
            if (i1 == -1)
                return string.Empty;
            return source[(i1 + find.Length)..];
        }


        /// <summary>
        /// Возвращает правую часть сторки из 'count' символов
        /// </summary>
        public static string GetRight(
            this string source,
            int count)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int l1 = source.Length;
            if (l1 <= count)
                return source;
            return source[(l1 - count)..];
        }


        /// <summary>
        /// Возвращает левую часть сторки до символа 'find'
        /// </summary>
        public static string GetBackTo(
            this string source,
            char find,
            int skip = 0)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int step1 = 0;
            int i1 = source.Length - 1;
            int i2;
            do
            {
                i2 = source.LastIndexOf(find, i1);
                i1 = i2 - 1;
                step1++;
            } while (step1 <= skip && i2 != -1);
            if (i2 == -1)
                return string.Empty;
            return source[..i2];
        }


        /// <summary>
        /// Возвращает подстроку с подстановкой по обрезанным краям
        /// </summary>
        public static string GetCrop(
            this string source,
            int startIndex,
            int length,
            string beginCropMask,
            string endCropMask)
        {
            int l1 = source.Length;
            if (source == null || l1 == 0 || startIndex >= l1)
                return string.Empty;
            int i1 = l1 - startIndex;
            bool f1 = startIndex > 0;
            bool f2 = false;
            if (i1 > length)
            {
                f2 = true;
                i1 = length;
            }
            string s1 = source[startIndex..(startIndex + i1)];
            return string.Concat(f1.Make(beginCropMask), s1, f2.Make(endCropMask));
        }


        /// <summary>
        /// Возвращает результат преобразования символов с кодом меньше 33 в '_' и больше 126 в их кодовый эквивалент
        /// </summary>
        public static string GetSafeText(
            this string source)
        {
            var sb1 = new StringBuilder(source);
            sb1.GetTransformToSafeText();
            return sb1.ToString();
        }


        /// <summary>
        /// Возвращает из строки подстроку находящуюся между 'before' и 'after'
        /// </summary>
        public static string GetTag(
            this string source,
            string before,
            string after,
            StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = (string.IsNullOrEmpty(before))
                ? 0 : source.IndexOf(before, comparisonType);
            if (i1 < 0)
                return string.Empty;
            i1 += before.Length;
            int i2 = (string.IsNullOrEmpty(after))
                ? source.Length
                : source.IndexOf(after, i1, comparisonType);
            if (i2 < 0)
                return string.Empty;
            return source[i1..i2];
        }


        /// <summary>
        /// Вырезает и возвращает из строки подстроку находящуюся между 'before' и 'after'
        /// </summary>
        public static string GetTagAndCut(
            this string source,
            string before,
            string after,
            StringComparison comparisonType = StringComparison.InvariantCulture)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;
            int i1 = (string.IsNullOrEmpty(before))
                ? 0 : source.IndexOf(before, comparisonType);
            if (i1 < 0)
                return string.Empty;
            int l1 = before.Length;
            int i2 = i1 + l1;
            int i3 = (string.IsNullOrEmpty(after))
                ? source.Length
                : source.IndexOf(after, i2, comparisonType);
            if (i3 < 0)
                return string.Empty;
            string s1 = source[i2..i3];
            source = source[..i1] + source[(i3 + after.Length)..];
            return s1;
        }


        /// <summary>
        /// Производит рекурсивную замену 'oldString' на 'newString'
        /// </summary>
        public static string GetReplaceRecursively(
            this string source,
            string oldText,
            string newText)
        {
            int i1 = source.IndexOf(oldText, 0);
            if (i1 < 0)
                return source;
            return source
                .Replace(oldText, newText)
                .GetReplaceRecursively(oldText, newText);
        }


        public static string GetReplaceSpecChars(
            this string source)
        {
            var sb1 = new StringBuilder(source);
            sb1.ReplaceSpecChars();
            return sb1.ToString();
        }





        /*
		public static string ReplaceByDict(
			this string source,
			Dictionary<string, string> dictionary,
			RegexOptions options)
		{
			var s1 = source;
			foreach (var item1 in dictionary)
				s1 = Regex.Replace(s1, item1.Key, item1.Value, options);
			return s1;
		}


		public static string ReplaceByDict(
			this string source,
			Dictionary<string, string> dictionary,
			StringComparison comparison)
		{
			var s1 = source;
			foreach (var item1 in dictionary)
			{
				var i1 = s1.IndexOf(item1.Key, comparison);
				while (i1 >= 0)
				{
					var left1 = s1[..i1];
					var right1 = s1[(i1 + item1.Key.Length)..(s1.Length - i1 - item1.Key.Length)];
					s1 = left1 + item1.Value + right1;
					i1 = s1.IndexOf(item1.Key, comparison);
				}
			}
			return s1;
		}
		*/

    }

}
