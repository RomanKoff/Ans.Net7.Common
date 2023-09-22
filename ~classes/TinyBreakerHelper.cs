namespace Ans.Net7.Common
{

    public class TinyBreakerHelper
    {
        public int Step { get; private set; }
        public int Current { get; private set; }

        public TinyBreakerHelper(
            int step)
        {
            Step = step;
            Current = 0;
        }

        public TinyBreakerHelper()
            : this(1234)
        {
        }

        public bool Next()
        {
            if (Current++ != Step)
                return false;
            Current = 0;
            return true;
        }
    }

}
