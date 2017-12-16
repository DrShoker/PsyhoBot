using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebClientBot.Models;

namespace ClientRequestService.Common
{
    public static class ClientExtenctions
    {
        private static HttpClient _client;

        public static HttpClient Client
        {
            get
            {
                if (_client != null)
                    return _client;

                _client = new HttpClient();

                try
                {
                    _client.BaseAddress = new Uri($"{UrlContacts.BaseUrl}");
                }
                catch (Exception)
                {
                    // Error will be thrown on first attempt to connect
                }

                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                return _client;
            }
        }

        public static void Post(string address)
        {
            HttpResponseMessage response;
            try
            {
                response = Client.PostAsJsonAsync(address, string.Empty).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
        }

        public static void PostData<T>(T data, string address)
        {
            var response = PostDataAndGetHttpResponse(data, address);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(response.Content.ReadAsStringAsync().Result);
            }
        }

        public static TR PostDataAndGetResult<TI, TR>(TI data, string address) where TR : class, new()
        {
            var response = PostDataAndGetHttpResponse(data, address);

            if (response.IsSuccessStatusCode)
            {
                // Get reponse and parse result
                var responseText = response.Content.ReadAsStringAsync().Result;
                var result = responseText.TryParseJson<TR>();
                return result;
            }

            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        public static T GetResult<T>(string address) where T : class, new()
        {
            HttpResponseMessage response;
            try
            {
                response = Client.GetAsync(address).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }

            if (response.IsSuccessStatusCode)
            {
                // Get reponse and parse result
                var responseText = response.Content.ReadAsStringAsync().Result;
                var result = responseText.TryParseJson<T>();
                return result;
            }

            throw new Exception(response.Content.ReadAsStringAsync().Result);
        }

        private static HttpResponseMessage PostDataAndGetHttpResponse<T>(T data, string address)
        {
            HttpResponseMessage response;
            try
            {
                response = Client.PostAsJsonAsync(address, data).Result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.GetBaseException().Message);
            }

            return response;
        }

        public static T TryParseJson<T>(this string json) where T : new()
        {
            try
            {
                var deserializedObject = JsonConvert.DeserializeObject<T>(json);
                return deserializedObject;
            }
            catch (JsonException)
            {
                return default(T);
            }
        }
    }
}
