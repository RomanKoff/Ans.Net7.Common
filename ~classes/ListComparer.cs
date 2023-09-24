namespace Ans.Net7.Common
{

	public class ListComparer<T>
		: _ListComparer_Base<T>
	{
		public ListComparer(
			IEnumerable<T> currentList,
			IEnumerable<T> newestList,
			Func<T, string> compareValue,
			Func<T, bool> removeTest = null)
			: base(
				 currentList, newestList,
				 (x1, x2) => compareValue(x1).Equals(compareValue(x2)),
				 (x1, x2) => !compareValue(x1).Equals(compareValue(x2)),
				 removeTest)
		{
		}
	}

}
