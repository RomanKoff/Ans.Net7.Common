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

		public string Extention { get; set; }
		public string ContentType { get; set; }
		public ContentGroupEnum Group { get; set; }
		public bool IsWebImage { get; set; }
		public bool IsJpeg { get; set; }

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
