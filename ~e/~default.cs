namespace Ans.Net7.Common
{

	public static partial class _e
	{

		/*
		 * string ForEmpty(this string source, string defaultValue);
         * string ReplaceIfEqual(this string source, string compared, string newest);
         */


		public static string ForEmpty(
			this string source,
			string defaultValue)
		{
			return (source == string.Empty)
				? defaultValue : source;
		}


		public static string ReplaceIfEqual(
			this string source,
			string compared,
			string newest)
		{
			return (source == compared)
				? newest
				: source;
		}

	}

}
