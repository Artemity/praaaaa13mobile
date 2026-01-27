using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using praaaaa13.Models;

namespace praaaaa13.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiBaseUrl;

        public ApiService()
        {
            // Инициализируем HttpClient
            _httpClient = new HttpClient();
            // Укажите здесь адрес вашего Web API (используйте ваш IP и порт)
            _apiBaseUrl = "http://192.168.1.120:5123/api/";
            // Устанавливаем таймаут
            _httpClient.Timeout = TimeSpan.FromSeconds(30);
        }

        // GET: Получить все товары
        public async Task<List<Product>> GetProductsAsync()
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Product>>(content);
            }
            return new List<Product>();
        }

        // GET: Получить товар по ID
        public async Task<Product> GetProductByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"{_apiBaseUrl}Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<Product>(content);
            }
            return null;
        }

        // POST: Добавить товар
        public async Task<bool> AddProductAsync(Product product)
        {
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"{_apiBaseUrl}Products", content);
            return response.IsSuccessStatusCode;
        }

        // PUT: Обновить товар
        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            var json = JsonSerializer.Serialize(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"{_apiBaseUrl}Products/{id}", content);
            return response.IsSuccessStatusCode;
        }

        // DELETE: Удалить товар
        public async Task<bool> DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiBaseUrl}Products/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
