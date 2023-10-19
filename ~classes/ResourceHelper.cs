using Ans.Net7.Common;
using Ans.Net7.Common.Resources;
using System.Resources;

namespace Ans.Net7.Common
{

	public class ResourceHelper
	{

		/* ctor */


		public ResourceHelper(
			ResourceManager resources,
			ResourceManager commonResources = null)
		{
			Resources = resources;
			CommonResources = commonResources;
		}


		public ResourceHelper(
			IDictionary<object, object> items,
			string formResourcesKey)
		{
			if (items.ContainsKey(formResourcesKey))
			{
				var res1 = items[formResourcesKey] as ResourceManager[];
				if (res1.Length > 0)
				{
					Resources = res1[0];
					if (res1.Length > 1)
						CommonResources = res1[1];
				}
			}
		}


		/* readonly properties */


		public ResourceManager Resources { get; private set; }
		public ResourceManager CommonResources { get; private set; }


		/* functions */


		public string GetValue(
			string key)
		{
			if (Resources == null)
				return key;
			var s1 = Resources.GetString(key);
			if (!string.IsNullOrEmpty(s1))
				return s1;
			if (CommonResources != null)
				s1 = CommonResources.GetString(key);
			if (!string.IsNullOrEmpty(s1))
				return s1;
			s1 = Faces.ResourceManager.GetString(key);
			return (string.IsNullOrEmpty(s1))
				? $"{{{key}}}" : s1;
		}


		public ResourceFace GetFace(
			string name)
		{
			return new ResourceFace(GetValue(name));
		}

	}

}
