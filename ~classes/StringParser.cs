namespace Ans.Net7.Common
{

	public class StringParser
	{
		private readonly string[] _items;
		private readonly int _count;

		public StringParser(
			string source)
		{
			if (string.IsNullOrEmpty(source))
				return;
			_items = source.Split('|');
			_count = _items.Length;
		}

		public string Get(
			int index,
			string defaultValue = null)
		{
			if (index < _count)
				return _items[index];
			return defaultValue;
		}
	}

}
