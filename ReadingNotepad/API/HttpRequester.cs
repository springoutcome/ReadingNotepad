using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http.Headers;

namespace ReadingNotepad.API
{
    public static class HttpRequester
    {
        private static HttpClient client = new HttpClient();

        public static async Task<T> Get<T>(Dictionary<string, string> queryParameters, string url) where T : class
        {
            var response =
            await client.GetAsync(url + $"? {await new FormUrlEncodedContent(queryParameters).ReadAsStringAsync()}");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }

        }

        public static async Task<T> NoQueryGet<T>(string url) where T : class
        {
            var response =
            await client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }

        }

        public static async Task<T> Post<T>(Dictionary<string, string> reqParameters, string url) where T : class
        {
            var response = await client.PostAsync(url, new FormUrlEncodedContent(reqParameters));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        public static async Task<T> Put<T>(Dictionary<string, string> reqParameters, string url) where T : class
        {
            var response = await client.PutAsync(url, new FormUrlEncodedContent(reqParameters));
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }


        public static async Task<T> Delete<T>(string url) where T : class
        {
            var response = await client.DeleteAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        public static async Task<T> PostFile<T>(string filePath, string url) where T : class
        {
            var fileContent = new StreamContent(File.OpenRead(filePath));
            fileContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = Path.GetFileName(filePath)
            };
            var content = new MultipartFormDataContent();
            content.Add(fileContent);

            var response = await client.PostAsync(url, content);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                return default(T);
            }
        }

        public static async void FileNoQueryGet(string url, string outputPath)
        {
            var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {

                using (var fileStream = File.Create(outputPath))
                {
                    using (var httpStream = await response.Content.ReadAsStreamAsync())
                    {
                        httpStream.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                }
            }

        }
    }
}