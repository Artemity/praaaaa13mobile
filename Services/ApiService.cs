using Newtonsoft.Json;
using praaaaa13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace praaaaa13.Services
{
    public class APIService
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private static readonly string _apiBaseUrl = "http://192.168.26.10:5123/";

        public static T Get<T>(string endPoint)
        {
            try
            {
                var response = _httpClient.GetAsync(_apiBaseUrl + endPoint).Result;

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API Error: {response.StatusCode}");
                    return default(T);
                }

                var content = response.Content.ReadAsStringAsync().Result;
                var data = JsonConvert.DeserializeObject<T>(content);
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"API Error: {ex.Message}");
                return default(T);
            }
        }

        public static async Task<string> Post<T>(T body, string endpoint)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync(_apiBaseUrl + endpoint, content);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {result.StatusCode}");
            }

            return await result.Content.ReadAsStringAsync();
        }

        public static async Task<string> Put<T>(T body, int id, string endpoint)
        {
            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var result = await _httpClient.PutAsync(_apiBaseUrl + endpoint + "/" + id, content);

            if (!result.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error: {result.StatusCode}");
            }

            return await result.Content.ReadAsStringAsync();
        }

        public static async Task<bool> Delete(int id, string endpoint)
        {
            var result = await _httpClient.DeleteAsync(_apiBaseUrl + endpoint + "/" + id);
            return result.IsSuccessStatusCode;
        }
    }
}
