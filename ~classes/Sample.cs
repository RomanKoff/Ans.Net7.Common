namespace Ans.Net7.Common
{

	public class Sample
	{
		public void Parse(
			params string[] name)
		{
		}

		public IEnumerable<Sample> Masters { get; }
		public IEnumerable<Sample> Slaves { get; }
		public List<Sample> Items { get; }
		public Sample Master { get; }

		public string[] Tags { get; set; }
		public string Name { get; set; } = "";
		public string Description { get; set; }
		public string Author { get; set; }

		public DateTime Create { get; private set; } = DateTime.Now;
		public int Count { get; private set; } = SuppRandom.Next();
		public int? Count2 { get; private set; }
		public bool IsDisabled { get; private set; }

		public string GetSummary()
		{
			return Description;
		}

		public string GetSummary(
			bool isDisabled)
		{
			return Description;
		}

		public string GetSummary(
			string name,
			bool isDisabled)
		{
			return Description;
		}

		public void Calc(
			int start = 0)
		{
		}

		public Dictionary<string, T> Send<T>(
			T obj,
			dynamic props)
		{
			return null;
		}

		private void _update()
		{
		}

	}

}
