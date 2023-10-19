namespace Ans.Net7.Common
{

	public class ResourceFace
	{

		/// <param name="face">
		/// Title|Description|Sample|ListHeader|MoreLink
		/// </param>
		public ResourceFace(
			string face)
		{
			var a1 = new StringParser(face);
			Title = a1.Get(0);
			Description = a1.Get(1);
			Sample = a1.Get(2);
			ListHeader = a1.Get(3);
			MoreLink = a1.Get(4);
		}


		public string Title { get; private set; }
		public string Description { get; private set; }
		public string Sample { get; private set; }
		public string ListHeader { get; private set; }
		public string MoreLink { get; private set; }

		public bool HasDescription
			=> !string.IsNullOrEmpty(Description);

		public bool HasSample
			=> !string.IsNullOrEmpty(Sample);

		public bool HasListHeader
			=> !string.IsNullOrEmpty(ListHeader);

		public bool HasMoreLink
			=> !string.IsNullOrEmpty(MoreLink);

	}

}
