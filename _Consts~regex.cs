using System.Text.RegularExpressions;

namespace Ans.Net7.Common
{

	public static partial class _Consts
	{

		public const string REGEX_ENCODE = @"[]^\\-]";
		public const string REGEX_NAME = @"^([a-z_][0-9a-z_-]+)$";
		public const string REGEX_VARNAME = @"^([a-zA-Z_][0-9a-zA-Z_]+)$";
		public const string REGEX_IPATH = @"^(([a-zA-Z_][0-9a-zA-Z_-]+)/){0,}([a-zA-Z_][0-9a-zA-Z_-]+)$";
		public const string REGEX_NAME_STRICT = @"^([a-z_][0-9a-z_-]+)$";
		public const string REGEX_VARNAME_STRICT = @"^([a-z_][0-9a-z_]+)$";
		public const string REGEX_IPATH_STRICT = @"^(([a-z_][0-9a-z_-]+)/){0,}([a-z_][0-9a-z_-]+)$";
		public const string REGEX_EMAIL = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";
		public const string REGEX_EMAIL2 = @"^(?("")(""[^""]+""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$";
		public const string REGEX_URL = @"(https?://)[-_./:\?=&%#a-zA-Z0-9]+";
		public const string REGEX_INT = @"\d+";
		public const string REGEX_IP4 = @"\b(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b";
		public const string REGEX_PASSWORD = @"^([0-9a-zA-Z `\'"":;,\.<>+=\~_\?\!\@#\$%\^\&\*\(\)\[\]\{\}/\\\|-]+)$";
		public const string REGEX_FIO_RU = @"^[A-Za-zА-ЯЁа-яё-]{1,23}( [A-Za-zА-ЯЁа-яё-]{1,23})+$";


		[GeneratedRegex("[]^\\\\-]")]
		public static partial Regex G_REGEX_ENCODE();

		[GeneratedRegex(REGEX_NAME)]
		public static partial Regex G_REGEX_NAME();

		[GeneratedRegex(REGEX_VARNAME)]
		public static partial Regex G_REGEX_VARNAME();

		[GeneratedRegex(REGEX_IPATH)]
		public static partial Regex G_REGEX_IPATH();

		[GeneratedRegex(REGEX_NAME_STRICT)]
		public static partial Regex G_REGEX_NAME_STRICT();

		[GeneratedRegex(REGEX_VARNAME_STRICT)]
		public static partial Regex G_REGEX_VARNAME_STRICT();

		[GeneratedRegex(REGEX_IPATH_STRICT)]
		public static partial Regex G_REGEX_IPATH_STRICT();

		[GeneratedRegex(REGEX_EMAIL)]
		public static partial Regex G_REGEX_EMAIL();

		[GeneratedRegex(REGEX_EMAIL2)]
		public static partial Regex G_REGEX_EMAIL2();

		[GeneratedRegex(REGEX_URL)]
		public static partial Regex G_REGEX_URL();

		[GeneratedRegex(REGEX_INT)]
		public static partial Regex G_REGEX_INT();

		[GeneratedRegex(REGEX_IP4)]
		public static partial Regex G_REGEX_IP4();

		[GeneratedRegex(REGEX_PASSWORD)]
		public static partial Regex G_REGEX_PASSWORD();

		[GeneratedRegex(REGEX_FIO_RU)]
		public static partial Regex G_REGEX_FIO_RU();

	}

}
