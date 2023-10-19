using System.Text;

namespace Ans.Net7.Common
{

	public static partial class _Consts
	{

		public static readonly string[] FORBIDDEN_FILE_NAMES = {
			"con","prn","aux","nul",
			"com0","com1","com2","com3","com4","com5","com6","com7","com8","com9",
			"lpt0","lpt1","lpt2","lpt3","lpt4","lpt5","lpt6","lpt7","lpt8","lpt9" };


		public static readonly TimeSpan DAY_START_SPAN = new(0, 0, 0, 0);
		public static readonly TimeSpan DAY_END_SPAN = new(0, 23, 59, 59);


		public static readonly string FORMAT_SAFE_DATETIME
			= "yyyy-0MM-dd HH:mmmm:ss";


		public static readonly string FORMAT_SAFE_DATE
			= "yyyy-0MM-dd";


		public static readonly string FORMAT_SAFE_DATETIME_FILE
			= "yyyy-0MM-dd_HH-mmmm-ss";


		public static readonly string FORMAT_SAFE_DATETIME_MIN
			= "yyyy0MMddHHmmmmss";


		public static readonly Encoding ENCODING_UTF8
			= Encoding.UTF8;


        /*
		 * Support win1251 and koi8r:
		 * Using System.Text.Encoding.CodePages 
		 * Program.cs -> 
		 *		SuppIO.Register_CodePagesEncodingProvider();
		 *		// Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
		 * https://ru.stackoverflow.com/questions/1387144/%D0%BF%D0%B5%D1%80%D0%B5%D0%B2%D0%BE%D0%B4-%D0%B8%D0%B7-koi8-r-c
		 */


        public static readonly Encoding ENCODING_WINDOWS1251
			= Encoding.GetEncoding(1251);


		public static readonly Encoding ENCODING_KOI8R
			= Encoding.GetEncoding(20866);


		public static readonly Encoding ENCODING_CP866
			= Encoding.GetEncoding(866);


		public static readonly Encoding ENCODING_ISO88591
			= Encoding.GetEncoding("iso-8859-1");


		public static readonly char[] SPLIT_ITEMS = new char[] { ';', ',' };

	}

}
