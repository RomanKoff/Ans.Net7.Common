using System.Text.Encodings.Web;
using System.Text.Json;

namespace Ans.Net7.Common
{

    public static class SuppJson
    {

        /*
         * JsonSerializerOptions DefaultJsonSerializerOptions { get; }
         * 
         * T GetObjectFromJsonString<T>(string json, JsonSerializerOptions options = null)
		 * T GetObjectFromJson<T>(Stream stream, JsonSerializerOptions options = null);
		 * async Task<T> GetObjectFromJsonAsync<T>(Stream stream, JsonSerializerOptions options = null);
		 * T GetObjectFromJsonFile<T>(string filename, JsonSerializerOptions options = null);
		 * string GetJsonStringFromObject(object obj, JsonSerializerOptions options = null);
		 * 
		 * void WriteObjectToStreamJson(object obj, Stream stream, JsonSerializerOptions options = null);
		 * void SaveObjectToJsonFile(object obj, string filename, JsonSerializerOptions options = null);
         */


        /* readonly properties */


        /// <summary>
        /// Настройки сериализации по умолчанию
        /// </summary>
        public static JsonSerializerOptions DefaultJsonSerializerOptions
            => new()
            {
                //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                PropertyNameCaseInsensitive = false,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            };


        /* functions */


        /// <summary>
        /// Возвращает объект типа T из строки json
        /// </summary>
        public static T GetObjectFromJsonString<T>(
            string json,
            JsonSerializerOptions options = null)
        {
            return JsonSerializer.Deserialize<T>(
                json, options ?? DefaultJsonSerializerOptions);
        }


        /// <summary>
        /// Возвращает объект типа T из потока json
        /// </summary>
        public static T GetObjectFromJson<T>(
            Stream stream,
            JsonSerializerOptions options = null)
        {
            var res1 = JsonSerializer.Deserialize<T>(
                stream, options ?? DefaultJsonSerializerOptions);
            return res1;
        }


        /// <summary>
        /// (async) Возвращает объект типа T из потока json 
        /// </summary>
        public static async Task<T> GetObjectFromJsonAsync<T>(
            Stream stream,
            JsonSerializerOptions options = null)
        {
            var res1 = await JsonSerializer.DeserializeAsync<T>(
                stream, options ?? DefaultJsonSerializerOptions);
            return res1;
        }


        /// <summary>
        /// Возвращает объект типа T из файла json
        /// </summary>
        public static T GetObjectFromJsonFile<T>(
            string filename,
            JsonSerializerOptions options = null)
        {
            using var stream1 = new FileStream(
                filename, FileMode.Open, FileAccess.Read);
            return GetObjectFromJson<T>(
                stream1, options);
        }


        /// <summary>
        /// Возвращает строку json из объекта
        /// </summary>
        public static string GetJsonStringFromObject(
            object obj,
            JsonSerializerOptions options = null)
        {
            return JsonSerializer.Serialize(
                obj, options ?? DefaultJsonSerializerOptions);
        }


        /* methods */


        /// <summary>
        /// Записывает объект в поток json
        /// </summary>
        public static void WriteObjectToStreamJson(
            object obj,
            Stream stream,
            JsonSerializerOptions options = null)
        {
            using var writer1 = new Utf8JsonWriter(
                stream,
                new JsonWriterOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping });
            JsonSerializer.Serialize(
                writer1, obj, options ?? DefaultJsonSerializerOptions);
        }


        /// <summary>
        /// Сохраняет объект в файл json
        /// </summary>
        public static void SaveObjectToJsonFile(
            object obj,
            string filename,
            JsonSerializerOptions options = null)
        {
            using var stream1 = new FileStream(
                filename, FileMode.Create, FileAccess.Write);
            WriteObjectToStreamJson(
                obj, stream1, options);
        }

    }

}
