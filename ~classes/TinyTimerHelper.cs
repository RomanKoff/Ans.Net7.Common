namespace Ans.Net7.Common
{

    public class TinyTimerHelper
    {
        public TimeSpan Span { get; set; }
        public DateTime NextStop { get; set; }
        public bool UseEqualIntervals { get; set; }

        public TinyTimerHelper(
            TimeSpan span,
            bool useEqualIntervals)
        {
            Span = span;
            NextStop = DateTime.Now + Span;
            UseEqualIntervals = useEqualIntervals;
        }

        public bool Test()
        {
            if (NextStop < DateTime.Now)
            {
                NextStop = (UseEqualIntervals)
                    ? NextStop + Span
                    : DateTime.Now + Span;
                return true;
            }
            return false;
        }
    }

}
