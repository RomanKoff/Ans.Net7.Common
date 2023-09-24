using System.Reflection;

namespace Ans.Net7.Common
{

	public class InfoBuilder
	{

		private readonly Type _type;


		/* ctor */


		public InfoBuilder(
			Type type,
			object obj = null)
		{
			_type = type;
			_parse();
			if (obj != null)
			{
				foreach (var item1 in Properties)
					item1.Value = SuppObject.GetCSharpValue(
						obj.GetPropertyValue(item1.Name));
				foreach (var item1 in ReadonlyProperties)
					item1.Value = SuppObject.GetCSharpValue(
						obj.GetPropertyValue(item1.Name));
			}
		}


		/* readonly properties */


		public IEnumerable<InfoProperty> Properties { get; private set; }
		public IEnumerable<InfoProperty> ReadonlyProperties { get; private set; }
		public IEnumerable<InfoProperty> WriteonlyProperties { get; private set; }
		public IEnumerable<InfoFunction> Functions { get; private set; }
		public IEnumerable<InfoMethod> Methods { get; private set; }


		/* privates */


		private void _parse()
		{
			var type0 = typeof(System.Object);
			var methods1 = _type.GetMethods()
				.Where(x => x.DeclaringType != type0)
				.OrderBy(x => x.Name);
			List<MethodInfo> getter1 = new();
			List<MethodInfo> setter1 = new();
			List<MethodInfo> funcs1 = new();
			List<MethodInfo> meths1 = new();
			foreach (var item1 in methods1)
				if (item1.Name.StartsWith("get_"))
					getter1.Add(item1);
				else if (item1.Name.StartsWith("set_"))
					setter1.Add(item1);
				else if (item1.ReturnType.Name == "Void")
					meths1.Add(item1);
				else
					funcs1.Add(item1);
			var props1 = new List<InfoProperty>();
			foreach (var item1 in getter1)
				props1.Add(new InfoProperty
				{
					HasGetter = true,
					Name = item1.GetPropName(),
					Type = item1.ReturnType.GetCSharpTypeName()
				});
			foreach (var item1 in setter1)
			{
				var name1 = item1.GetPropName();
				var prop1 = props1.FirstOrDefault(x => name1.Equals(x.Name));
				if (prop1 != null)
					prop1.HasSetter = true;
				else
					props1.Add(new InfoProperty
					{
						HasSetter = true,
						Name = name1,
						Type = item1.ReturnType.GetCSharpTypeName()
					});
			}
			Properties = props1.Where(x => x.HasGetter && x.HasSetter);
			ReadonlyProperties = props1.Where(x => !x.HasSetter);
			WriteonlyProperties = props1.Where(x => !x.HasGetter);
			Functions = funcs1.Select(x => new InfoFunction
			{
				Return = x.ReturnType.GetCSharpTypeName(),
				Name = x.Name,
				Generics = x.GetCSharpGenerics(),
				Parameters = x.GetCSharpParams()
			});
			Methods = meths1.Select(x => new InfoMethod
			{
				Name = x.Name,
				Generics = x.GetCSharpGenerics(),
				Parameters = x.GetCSharpParams()
			});
		}

	}



	public class _Info_Base
	{
		public string Name { get; set; }
	}



	public class InfoProperty
		: _Info_Base
	{
		public bool HasGetter { get; set; }
		public bool HasSetter { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
	}



	public class InfoMethod
		: _Info_Base
	{
		public string Generics { get; set; }
		public string Parameters { get; set; }
	}



	public class InfoFunction
		: InfoMethod
	{
		public string Return { get; set; }
	}

}
