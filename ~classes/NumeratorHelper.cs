namespace Ans.Net7.Common
{

    public class NumeratorHelper
    {

        /* ctors */


        public NumeratorHelper(
            int maxLevel)
        {
            MaxLevel = maxLevel;
            Counters = new int[maxLevel];
            Level = 0;
            CounterReset(1);
        }


        /* readonly properties */


        public int MaxLevel { get; private set; }
        public int Level { get; private set; }
        public int[] Counters { get; private set; }


        /* funcs */


        public IEnumerable<int> GetCurrent()
        {
            return Counters.Take(Level + 1);
        }


        public string GetId()
        {
            return GetCurrent().MakeFromCollection(
                x => x.ToString(), null, null, "_");
        }


        public string GetView()
        {
            return GetCurrent().MakeFromCollection(
                x => x.ToString(), null, "{0}.", null);
        }


        /* methods */


        public void CounterReset(
            int start)
        {
            Counters[Level] = start;
        }


        public void CounterIncrease()
        {
            Counters[Level]++;
        }


        public void CounterDecrease()
        {
            Counters[Level]--;
        }


        public void LevelIncrease()
        {
            if (Level < MaxLevel)
            {
                CounterDecrease();
                Level++;
                CounterReset(1);
            }
        }


        public void LevelDecrease()
        {
            if (Level > 0)
            {
                Level--;
                CounterIncrease();
            }
        }

    }

}
