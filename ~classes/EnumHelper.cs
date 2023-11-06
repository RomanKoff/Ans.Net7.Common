namespace Ans.Net7.Common
{

	public class EnumHelper
	{

		public EnumHelper(
			int minIndex,
			int maxIndex,
			bool useRandomStart = false)
		{
			MinIndex = minIndex;
			MaxIndex = maxIndex;
			if (useRandomStart)
				CurrentIndex = SuppRandom.Next(minIndex, maxIndex);
			else
				CurrentIndex = minIndex;
		}


		public int MinIndex { get; private set; }
		public int MaxIndex { get; private set; }
		public int CurrentIndex { get; private set; }


		public int Next()
		{
			var i1 = CurrentIndex;
			CurrentIndex = (CurrentIndex == MaxIndex)
				? MinIndex : CurrentIndex + 1;
			return i1;
		}

	}

}
