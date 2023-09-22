using System.Text;

namespace Ans.Net7.Common
{

    public static class SuppText
    {

        /*
         * string GetFamIO(string fio, bool useDots = true, TextCaseEnum textCase = TextCaseEnum.StartWithACapital);
         */


        /// <summary>
        /// Возвращает фамилию и инициалы из строки содержащей фамилию имя и отчество
        /// </summary>
        /// <param name="fio">Исходная строка</param>
        /// <param name="useDots">Использовать точку после букв инициалов, иначе пробел</param>
        /// <param name="textCase">Операция преобразования регистра букв</param>
        /// <returns></returns>
        public static string GetFamIO(
            string fio,
            bool useDots = true,
            TextCaseEnum textCase = TextCaseEnum.StartWithACapital)
        {
            if (string.IsNullOrEmpty(fio))
                return string.Empty;
            var a1 = fio.Split(new char[] { ' ', '.', ',', '-' },
                StringSplitOptions.RemoveEmptyEntries);
            var sb1 = new StringBuilder(a1[0]);
            if (a1.Length > 1)
            {
                sb1.Append(' ');
                var d1 = useDots ? '.' : ' ';
                foreach (var item1 in a1.Skip(1))
                    sb1.Append($"{item1[0]}{d1}");
            }
            var s1 = sb1.ToString();
            return textCase switch
            {
                TextCaseEnum.Upper => s1.ToUpper(),
                TextCaseEnum.Lower => s1.ToLower(),
                TextCaseEnum.StartWithACapital => s1.GetStartWithACapital(),
                _ => s1
            };
        }

    }



    public enum TextCaseEnum
    {
        Original,
        Upper,
        Lower,
        StartWithACapital
    }

}
