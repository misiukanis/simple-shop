using Shop.Application.DTOs;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Models.Requests;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Shop.HttpClients
{
    public class ShopHttpClient(HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new JsonStringEnumConverter() }
            };

            var items = await _httpClient.GetFromJsonAsync<List<OrderDTO>>("api/Orders", options);
            return items;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            var options = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                Converters = { new JsonStringEnumConverter() }
            };

            var item = await _httpClient.GetFromJsonAsync<OrderDTO>($"api/Orders/{id}", options);
            return item;
        }

        public async Task ChangeOrderStatusAsync(Guid id, OrderStatus orderStatus)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Orders/{id}/Status", orderStatus);
            response.EnsureSuccessStatusCode();
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderRequest orderRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Orders", orderRequest);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = data.GetProperty("id").GetGuid();
            return id;
        }

        public async Task<List<ProductDTO>> GetProductsAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<ProductDTO>>("api/Products");
            return items;
        }

        public async Task<ProductDTO> GetProductByIdAsync(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<ProductDTO>($"api/Products/{id}");
            return item;
        }

        public async Task<ProductDTO> FindProductByIdAsync(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/Products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ProductDTO>();
            }

            return null;
        }

        public async Task<Guid> CreateProductAsync(CreateProductRequest productRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Products", productRequest);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = data.GetProperty("id").GetGuid();
            return id;
        }

        public async Task UpdateProductAsync(UpdateProductRequest productRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{productRequest.Id}", productRequest);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<CustomerDTO>>("api/Customers");
            return items;
        }

        public async Task<CustomerDTO> GetCustomerByIdAsync(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<CustomerDTO>($"api/Customers/{id}");
            return item;
        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerRequest customerRequest)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Customers", customerRequest);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = data.GetProperty("id").GetGuid();
            return id;
        }

        public async Task UpdateCustomerAsync(UpdateCustomerRequest customerRequest)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Customers/{customerRequest.Id}", customerRequest);
            response.EnsureSuccessStatusCode();
        }

    }
}
