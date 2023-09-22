using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace Ans.Net7.Common
{

    public interface IWebApiHelper<T>
    {
        string BaseUrl { get; set; }
        JsonSerializerOptions JsonOptions { get; set; }
        ParamsBuilder Params { get; set; }

        WebApiResult<T> SendQuery();
        WebApiResult<T> SendQuery(string queryString);
    }



    public class WebApiHelper<T>
        : IWebApiHelper<T>
    {

        private readonly HttpClient _httpClient;


        /* ctors */


        public WebApiHelper(
            HttpClient httpClient,
            string baseUrl,
            JsonSerializerOptions jsonOptions)
        {
            //using var httpClient = new HttpClient(_Const.IgnoreSSL_ClientHandler);
            _httpClient = httpClient;
            BaseUrl = baseUrl;
            JsonOptions = jsonOptions;
            Params = new ParamsBuilder();
        }


        public WebApiHelper(
            HttpClient httpClient,
            string baseUrl,
            bool propertyNameCaseInsensitive = false)
            : this(httpClient, baseUrl, propertyNameCaseInsensitive
                  ? new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
                  : null)
        {
        }


        /* properties */


        public string BaseUrl { get; set; }
        public ParamsBuilder Params { get; set; }
        public JsonSerializerOptions JsonOptions { get; set; }


        /* functions */


        public virtual WebApiResult<T> SendQuery(
            string queryString)
        {
            return _getAsync(queryString).Result;
        }


        public WebApiResult<T> SendQuery()
        {
            return SendQuery(Params.ToString());
        }


        /* privates */


        private async Task<WebApiResult<T>> _getAsync(
            string queryString)
        {
            var res1 = await _httpClient.GetAsync($"{BaseUrl}{queryString}");
            if (res1.IsSuccessStatusCode)
                return new WebApiResult<T>
                {
                    StatusCode = res1.StatusCode,
                    Content = res1.Content.ReadFromJsonAsync<T>().Result
                };
            return new WebApiResult<T>
            {
                StatusCode = res1.StatusCode,
                Content = default
            };
        }

    }



    public class WebApiResult<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public T Content { get; set; }
    }

}
