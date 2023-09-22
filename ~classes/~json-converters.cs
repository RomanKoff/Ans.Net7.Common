using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ans.Net7.Common
{

	/*
     * class BoolConverter : JsonConverter<bool>
     * class AutoNumberToStringConverter : JsonConverter<object>
     * class AutoStringToNumberConverter : JsonConverter<object>
     */



	public class BoolConverter
		: JsonConverter<bool>
	{
		public override bool Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			return reader.TokenType switch
			{
				JsonTokenType.True => true,
				JsonTokenType.False => false,
				JsonTokenType.String => bool.TryParse(reader.GetString(), out var b1) && b1, //throw new JsonException(),
				JsonTokenType.Number => reader.TryGetInt64(out long l1)
					? Convert.ToBoolean(l1)
					: reader.TryGetDouble(out double d1) && Convert.ToBoolean(d1),
				_ => throw new JsonException(),
			};
		}

		public override void Write(
			Utf8JsonWriter writer,
			bool value,
			JsonSerializerOptions options)
		{
			writer.WriteBooleanValue(value);
		}
	}



	public class AutoNumberToStringConverter
		: JsonConverter<object>
	{
		public override bool CanConvert(
			Type typeToConvert)
		{
			return typeof(string) == typeToConvert;
		}

		public override object Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.Number)
				return reader.TryGetInt64(out long l1)
					? l1.ToString()
					: reader.GetDouble().ToString();
			if (reader.TokenType == JsonTokenType.String)
				return reader.GetString();
			using var document1 = JsonDocument.ParseValue(ref reader);
			return document1.RootElement.Clone().ToString();
		}

		public override void Write(
			Utf8JsonWriter writer,
			object value,
			JsonSerializerOptions options)
		{
			writer.WriteStringValue(value.ToString());
		}
	}



	public class AutoStringToNumberConverter
		: JsonConverter<object>
	{
		public override bool CanConvert(
			Type typeToConvert)
		{
			/*
			 * see https://stackoverflow.com/questions/1749966/c-sharp-how-to-determine-whether-a-type-is-a-number
			 */
			switch (Type.GetTypeCode(typeToConvert))
			{
				case TypeCode.Byte:
				case TypeCode.SByte:
				case TypeCode.UInt16:
				case TypeCode.UInt32:
				case TypeCode.UInt64:
				case TypeCode.Int16:
				case TypeCode.Int32:
				case TypeCode.Int64:
				case TypeCode.Decimal:
				case TypeCode.Double:
				case TypeCode.Single:
					return true;
				default:
					return false;
			}
		}

		public override object Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options)
		{
			if (reader.TokenType == JsonTokenType.String)
			{
				var s1 = reader.GetString();
				return int.TryParse(s1, out var i1)
					? i1
					: (double.TryParse(s1, out var d1)
						? d1
						: throw new Exception($"unable to parse {s1} to number"));
			}
			if (reader.TokenType == JsonTokenType.Number)
				return reader.TryGetInt64(out long l1)
					? l1 : reader.GetDouble();
			using var document1 = JsonDocument.ParseValue(ref reader);
			throw new Exception($"unable to parse {document1.RootElement} to number");
		}

		public override void Write(
			Utf8JsonWriter writer,
			object value,
			JsonSerializerOptions options)
		{
			var s1 = value.ToString();
			if (int.TryParse(s1, out var i1))
				writer.WriteNumberValue(i1);
			else if (double.TryParse(s1, out var d1))
				writer.WriteNumberValue(d1);
			else
				throw new Exception($"unable to parse {s1} to number");
		}
	}



	/*

	public class BoolConverter
		: JsonConverter
	{
		public override bool CanConvert(
			Type objectType)
		{
			return false;
		}

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			return (reader.Value.ToString() == "1");
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer)
		{
			writer.WriteValue(((bool)value) ? 1 : 0);
		}
	}

	public class DateTimeConverter
		: JsonConverter
	{
		public override bool CanConvert(
			Type objectType)
		{
			return false;
		}

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			var v1 = reader.Value.ToString().ToDouble(0);
			var d1 = new DateTime(1970, 1, 1, 0, 0, 0, 0);
			return d1.AddSeconds(v1);
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer)
		{
			var v1 = (DateTime)value;
			var t1 = v1.ToUniversalTime()
				.Subtract(new DateTime(1970, 1, 1, 0, 0, 0, 0));
			writer.WriteValue((long)Math.Truncate(t1.TotalSeconds));
		}
	}

	public class DateLiteConverter
		: JsonConverter
	{
		public override bool CanConvert(
			Type objectType)
		{
			return false;
		}

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			return reader.Value.ToString().ToDateTime(); ;
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer)
		{
			writer.WriteValue(
				((DateTime)value).ToShortDateString());
		}
	}

	public enum Sample1Enum
	{
		TextValueDefault,
		TextValue1,
		TextValue2,
		TextValue3
	}

	public class Sample1EnumConverter
		: JsonConverter
	{
		public override bool CanConvert(Type objectType)
			=> false;

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			return reader.Value.ToString() switch
			{
				"value1" => Sample1Enum.TextValue1,
				"value2" => Sample1Enum.TextValue2,
				"value3" => Sample1Enum.TextValue3,
				_ => Sample1Enum.TextValueDefault
			};
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer)
		{
			writer.WriteValue((Sample1Enum)value switch
			{
				Sample1Enum.TextValue1 => "value1",
				Sample1Enum.TextValue2 => "value2",
				Sample1Enum.TextValue3 => "value3",
				_ => "valueDefault"
			});
		}
	}

	public enum Sample2Enum
	{
		IntValueDefault,
		IntValue1,
		IntValue2,
		IntValue3
	}

	public class Sample2EnumConverter
		: JsonConverter
	{
		public override bool CanConvert(Type objectType)
			=> false;

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer)
		{
			return int.Parse(reader.Value.ToString()) switch
			{
				1 => Sample2Enum.IntValue1,
				2 => Sample2Enum.IntValue2,
				3 => Sample2Enum.IntValue3,
				_ => Sample2Enum.IntValueDefault
			};
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer)
		{
			writer.WriteValue((Sample2Enum)value switch
			{
				Sample2Enum.IntValue1 => 1,
				Sample2Enum.IntValue2 => 2,
				Sample2Enum.IntValue3 => 3,
				_ => 0
			});
		}
	}

	*/

}
