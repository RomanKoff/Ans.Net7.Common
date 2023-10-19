namespace Ans.Net7.Common
{

	public static partial class _e
	{

		/*
		 * string ReplaceIfEqual(this string source, string compared, string newest);
         */


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
