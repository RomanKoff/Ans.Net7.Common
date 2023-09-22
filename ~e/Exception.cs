namespace Ans.Net7.Common
{

    public static partial class _e
    {

        /*
         * string GetExceptionMessage(this Exception exception);
         * bool TestContains(this Exception exception, string value);
         */


        public static string GetExceptionMessage(
            this Exception exception)
        {
            return (exception.InnerException == null)
                ? exception.Message
                : exception.InnerException.GetExceptionMessage();
        }


        public static bool TestContains(
            this Exception exception,
            string value)
        {
            if (exception.Message.Contains(value))
                return true;
            return exception.InnerException != null
                && exception.InnerException.TestContains(value);
        }

    }

}
