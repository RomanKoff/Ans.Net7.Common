namespace Ans.Net7.Common
{

	public static class SuppObject
	{

		/*
		 * string GetCSharpValue(object value);
		 * string GetCSharpName(string name);
		 */



		public static string GetCSharpValue(object value)
		{
			if (value == null)
				return "null";
			return value.GetType().Name switch
			{
				"String" =>
					value.ToString() == string.Empty
						? "Empty" : $"\"{value}\"",
				"Int32" => $"{value}",
				"Boolean" =>
					(bool)value ? "TRUE" : "false",
				_ => "{object}" //value.ToString()
			};
		}



		public static string GetCSharpName(
			string name)
		{
			return name switch
			{
				"Boolean" => "bool",
				"Int32" => "int",
				"String" => "string",
				"Void" => "void",
				_ => name
			};
		}

	}

}
