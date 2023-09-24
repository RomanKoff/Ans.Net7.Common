using System.Reflection;

namespace Ans.Net7.Common
{

	public static partial class _e
	{

		/*
		 * string GetCSharpTypeName(this Type type);
		 * string GetCSharpTypesName(this Type[] types);
		 * string GetCSharpGenerics(this MethodInfo info);
		 * string GetCSharpParams(this MethodInfo info);
		 * string GetPropName(this MethodInfo info);
		 * 
		 * object GetPropertyValue(this object obj, string name, Type type);
		 * object GetPropertyValue(this object obj, string name);
		 * T GetPropertyValue<T>(this object obj, string name, Type type);
		 * T GetPropertyValue<T>(this object obj, string name);
		 * 
		 * T DefaultObject<T>(this object value, T defaultValue);
		 * T DefaultObject<T>(this object value);
		 * 
		 * T[] Insert<T>(this T[] items, int index, T item);
		 * T[] RemoveAt<T>(this T[] items, int index);
		 */



		public static string GetCSharpTypeName(
			this Type type)
		{
			var s1 = type.Name;
			if (s1.EndsWith("[]"))
				return $"{SuppObject.GetCSharpName(s1[..^2])}[]";
			if (s1.StartsWith("Nullable`"))
				return $"{Nullable.GetUnderlyingType(type)?.GetCSharpTypeName()}?";
			int i1 = s1.IndexOf('`');
			if (i1 > 0)
				return $"{s1[..i1]}<{type.GenericTypeArguments.GetCSharpTypesName()}>";
			return SuppObject.GetCSharpName(s1);
		}


		public static string GetCSharpTypesName(
			this Type[] types)
		{
			return types.MakeFromCollection(
				x => $"{x.GetCSharpTypeName()}", null, null, ", ");
		}


		public static string GetCSharpGenerics(
			this MethodInfo info)
		{
			return info.IsGenericMethod
				? $"<{GetCSharpTypesName(info.GetGenericArguments())}>" : "";
		}


		public static string GetCSharpParams(
			this MethodInfo info)
		{
			return info.GetParameters().MakeFromCollection(
				x =>
					$"{(x.GetCustomAttribute(typeof(ParamArrayAttribute)) == null ? "" : "params ")}" +
					$"{x.ParameterType.GetCSharpTypeName()} {x.Name}" +
					$"{(x.HasDefaultValue ? $" = {SuppObject.GetCSharpValue(x.DefaultValue)}" : "")}",
				null, null, ", ");
		}


		public static string GetPropName(
			this MethodInfo info)
		{
			return info.Name.StartsWith("get_")
				? info.Name[4..] : info.Name.StartsWith("set_")
					? info.Name[4..] : info.Name;
		}



		public static object GetPropertyValue(
			this object obj,
			string name,
			Type type)
		{
			return type.GetProperty(name).GetValue(obj, null);
		}


		public static object GetPropertyValue(
			this object obj,
			string name)
		{
			return obj.GetPropertyValue(name, obj.GetType());
		}


		public static T GetPropertyValue<T>(
			this object obj,
			string name,
			Type type)
		{
			return (T)obj.GetPropertyValue(name, type);
		}


		public static T GetPropertyValue<T>(
			this object obj,
			string name)
		{
			return (T)obj.GetPropertyValue(name);
		}


		public static T DefaultObject<T>(
			this object value,
			T defaultValue)
		{
			if (value == null)
				return defaultValue;
			return (T)value;
		}


		public static T DefaultObject<T>(
			this object value)
		{
			return value.DefaultObject<T>(default);
		}


		public static T[] Insert<T>(
			this T[] items,
			int index,
			T item)
		{
			var a1 = items.ToList();
			if (index < 0)
				a1.Add(item);
			else
				a1.Insert(index, item);
			return a1.ToArray();
		}


		public static T[] RemoveAt<T>(
			this T[] items,
			int index)
		{
			var a1 = items.ToList();
			a1.RemoveAt(index);
			return a1.ToArray();
		}

	}

}
