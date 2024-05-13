﻿using BlazorEcommerce.Shared;
using System.Net.Http.Json;

namespace BlazorEcommerce.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _http;

        public event Action ProductsChanged;

        public ProductService(HttpClient http)
        {
            _http = http;
        }
        public List<Product> Products { get; set; } = new();
        public string Message { get; set; } = "Loading products...";

        public async Task GetProducts(string? categoryUrl = null)
        {
            var apiEndpoint = categoryUrl == null ? 
                "api/product/featured" : $"api/product/category/{categoryUrl}";
           
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Product>>>(apiEndpoint);
            
            if (result != null && result.Data != null) Products = result.Data;

            ProductsChanged.Invoke();
        }

        public async Task<ServiceResponse<Product>> GetProduct(int productId)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<Product>>($"api/product/{productId}");
            return result;
        }

        public async Task SearchProducts(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/product/search/{searchText}");
            
            if (result != null && result.Data != null) Products = result.Data;
            if (Products.Count == 0) Message = "No products found.";

            ProductsChanged?.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string searchText)
        {
            var result = await _http
                .GetFromJsonAsync<ServiceResponse<List<string>>>($"api/product/searchsuggestions/{searchText}");
            return result.Data;
        }
    }
}
