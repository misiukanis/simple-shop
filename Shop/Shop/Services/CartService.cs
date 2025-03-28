using Blazored.LocalStorage;
using Shop.HttpClients;
using Shop.Models;

namespace Shop.Services
{
    public class CartService
    {
        private const string CartStorageKey = "cart";
        private readonly ILocalStorageService _localStorage;
        private readonly ShopHttpClient _shopHttpClient;

        public CartService(
            ILocalStorageService localStorage,
            ShopHttpClient shopHttpClient)
        {
            _localStorage = localStorage;
            _shopHttpClient = shopHttpClient;
        }

        public async Task<Cart> GetShoppingCartAsync()
        {
            var cart = await _localStorage.GetItemAsync<Cart>(CartStorageKey);
            if (cart == null)
            {
                return new Cart();
            }
            return cart;
        }

        public async Task AddProductToShoppingCartAsync(Guid productId)
        {
            var cart = await GetShoppingCartAsync();

            var cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == productId);

            if (cartItem == null)
            {
                var product = await _shopHttpClient.GetProductByIdAsync(productId);
                cart.CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
            }
            else
            {
                cartItem.Quantity++;
            }

            await _localStorage.SetItemAsync(CartStorageKey, cart);
        }

        public async Task RemoveProductFromShoppingCartAsync(Guid productId)
        {
            var cart = await GetShoppingCartAsync();

            var cartItem = cart.CartItems.SingleOrDefault(x => x.ProductId == productId);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    cart.CartItems.Remove(cartItem);
                }
            }

            await _localStorage.SetItemAsync(CartStorageKey, cart);
        }

        public async Task EmptyCartAsync()
        {
            await _localStorage.RemoveItemAsync(CartStorageKey);
        }

        public async Task RefreshShoppingCartAsync()
        {
            var cart = await GetShoppingCartAsync();

            foreach (var cartItem in cart.CartItems.ToList())
            {
                var product = await _shopHttpClient.FindProductByIdAsync(cartItem.ProductId);
                if (product != null)
                {
                    cartItem.Price = product.Price;
                    cartItem.ProductName = product.Name;
                }
                else
                {
                    cart.CartItems.Remove(cartItem);
                }
            }

            await _localStorage.SetItemAsync(CartStorageKey, cart);
        }
    }
}
