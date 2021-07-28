using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace APITesting.Resources
{
    public static class HTTPHelper
    {
        private static HttpClient client;
        private static readonly object httpClientLock = new object();
        private static string baseUrl;
        public static HttpStatusCode lastStatusCode;


       static HTTPHelper()
        {
            InitializeClient();
            baseUrl = GlobalProperties.baseUrl;
        }

        public static void Dispose()
        {
            client.Dispose();
        }

        private static void InitializeClient()
        {
            if (client == null)
            {
                lock (httpClientLock)
                {
                    if (client == null)
                    {
                        client = new HttpClient();
                    }
                }
            }
        }
        public static U GetRestCall<U>(string method, object[] args)
        {
            return ExecuteRestCall<object, U>(method, null, args, false);
        }
        public static U PostRestCall<T, U>(string method, T request, object[] args)
        {
            return ExecuteRestCall<T, U>(method, request, args, true);
        }
        private static U ExecuteRestCall<T, U>(string method, T request, object[] args, bool isPost)
        {
            try
            {
                HttpResponseMessage response = null;
                var fullPath = $"{baseUrl}";
                if (args != null)
                {
                    foreach (var arg in args)
                    {
                        fullPath += $"/{arg.ToString()}";
                    }
                }
                if (isPost)
                {
                    response = client.PostAsync(fullPath, new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")).Result;
                }
                else
                {
                    response = client.GetAsync(fullPath).Result;
                }
                var respStr = response.Content.ReadAsStringAsync().Result;
                lastStatusCode = response.StatusCode;
                if (response.StatusCode == HttpStatusCode.OK)
                {                    
                    return JsonSerializer.Deserialize<U>(respStr);
                }
                else if (response.StatusCode >= (HttpStatusCode)400)
                {
                    throw new Exception(respStr);
                }
                return default;
            }
            catch (Exception ex)
            {
                ex.Data.Add("method", method);
                ex.Data.Add("baseURL", baseUrl);
                throw;
            }
        }
    }
}
