using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace iemobile.Services
{
    public class BaseService
    {
        protected HttpClient client;
        protected string ControllerRoutePrefix { get; set; }
        private static string token = "";

        public BaseService(string controllerRoutePrefix = "")
        {
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
            {
                return true;
            };

            ControllerRoutePrefix = controllerRoutePrefix;
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(Settings.BackEndBaseUrl);
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

         
           
        }

        private void ApplyToken()
        {
            if (string.IsNullOrEmpty(token)) return;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        public void SetToken(string token)
        {
            BaseService.token = token;
        }

        protected async Task<T> GetFromWebApi<T>(string actionRoute) where T : class
        {
            try
            {
              
                return await DeserializeJson<T>(await GetFromWebApi(actionRoute));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        protected async Task<T> DeleteAsync<T>(string actionRoute) where T : class
        {
            try
            {
                return await DeserializeJson<T>(await DeleteAsync(actionRoute));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        protected async Task<HttpResponseMessage> GetFromWebApi(string actionRoute)
        {
            ApplyToken();
            return await client.GetAsync($"{Settings.BackEndBaseUrl}/{ControllerRoutePrefix}/{actionRoute}").ConfigureAwait(false);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string actionRoute)
        {
            var url = ControllerRoutePrefix;
            if (!string.IsNullOrWhiteSpace(actionRoute))
            {
                url += $"/{actionRoute}";
            }
            ApplyToken();
            return await client.DeleteAsync(url).ConfigureAwait(false);
        }

        protected async Task<TResult> PostToWebApi<TResult>(string actionRoute, object itemToPost) where TResult : class
        {
            try
            {
                return await DeserializeJson<TResult>(await PostToWebApi(actionRoute, itemToPost));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return null;
            }
        }

        protected async Task<HttpResponseMessage> PostToWebApi(string actionRoute, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = ControllerRoutePrefix;
            if (!string.IsNullOrWhiteSpace(actionRoute))
            {
                url += $"/{actionRoute}";
            }
            Debug.WriteLine(url);
            ApplyToken();
            return await client.PostAsync(url, content);
        }

        protected async Task<HttpResponseMessage> PutWebApi(string actionRoute, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var url = ControllerRoutePrefix;
            if (!string.IsNullOrWhiteSpace(actionRoute))
            {
                url += $"/{actionRoute}";
            }
            Debug.WriteLine(url);
            ApplyToken();
            return await client.PutAsync(url, content);
        }

        protected async Task<T> DeserializeJson<T>(HttpResponseMessage message) where T : class
        {
            using (var responseStream = await message.Content.ReadAsStreamAsync().ConfigureAwait(false))
            {
                return JsonConvert.DeserializeObject<T>(await new StreamReader(responseStream).ReadToEndAsync().ConfigureAwait(false));
            }
        }

    }

}
