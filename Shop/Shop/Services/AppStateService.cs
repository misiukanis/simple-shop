namespace Shop.Services
{
    public class AppStateService
    {
        public int TotalCartItemsQuantity { get; private set; }
        public decimal TotalCartItemsPrice { get; private set; }

        public event Action OnChange;

        public void SetCartSummary(int totalQuantity, decimal totalPrice)
        {
            TotalCartItemsQuantity = totalQuantity;
            TotalCartItemsPrice = totalPrice;

            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
