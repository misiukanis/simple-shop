using Shop.Shared.DTOs;
using Shop.Shared.Enums;
using System.Net.Http.Json;
using System.Text.Json;

namespace Shop.Client.HttpClients
{
    public class ShopHttpClient
    {
        private readonly HttpClient _httpClient;        

        public ShopHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<OrderDTO>> GetOrdersAsync()
        {
            var items = await _httpClient.GetFromJsonAsync<List<OrderDTO>>("api/Orders");
            return items;
        }

        public async Task<OrderDTO> GetOrderByIdAsync(Guid id)
        {
            var item = await _httpClient.GetFromJsonAsync<OrderDTO>($"api/Orders/{id}");
            return item;
        }

        public async Task ChangeOrderStatusAsync(Guid id, OrderStatus orderStatus)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Orders/{id}/Status", Enum.GetName(typeof(OrderStatus), orderStatus));
            response.EnsureSuccessStatusCode();
        }

        public async Task<Guid> CreateOrderAsync(CreateOrderDTO order)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Orders", order);
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

        public async Task<Guid> CreateProductAsync(CreateProductDTO product)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Products", product);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = data.GetProperty("id").GetGuid();
            return id;
        }

        public async Task UpdateProductAsync(UpdateProductDTO product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{product.Id}", product);
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

        public async Task<Guid> CreateCustomerAsync(CreateCustomerDTO customer)
        {
            var response = await _httpClient.PostAsJsonAsync($"api/Customers", customer);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadFromJsonAsync<JsonElement>();
            var id = data.GetProperty("id").GetGuid();
            return id;
        }

        public async Task UpdateCustomerAsync(UpdateCustomerDTO customer)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Customers/{customer.Id}", customer);
            response.EnsureSuccessStatusCode();
        }
        
    }
}
