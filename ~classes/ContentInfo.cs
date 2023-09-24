using System.Net.Http.Headers;

namespace Ans.Net7.Common
{

	public class ContentInfo
	{
		public ContentInfo(
			string extention,
			string contentType,
			ContentGroupEnum group,
			bool isWebImage = false,
			bool isJpeg = false)
		{
			Extention = extention;
			ContentType = contentType;
			Group = group;
			IsWebImage = isWebImage;
			IsJpeg = isJpeg;
		}

		public string Extention { get; private set; }
		public string ContentType { get; private set; }
		public ContentGroupEnum Group { get; private set; }
		public bool IsWebImage { get; private set; }
		public bool IsJpeg { get; private set; }

		public MediaTypeHeaderValue MediaType
			=> _mediaType ??= new MediaTypeHeaderValue(ContentType);
		private MediaTypeHeaderValue _mediaType;
	}



	public enum ContentGroupEnum
	{
		Archive,
		Audio,
		Bin,
		Code,
		Document,
		Image,
		Text,
		Video
	}

}
