using Shop.HttpClients;
using Shop.Shared.Constants;

namespace Shop
{
    public static class DependencyInjection
    {
        public static void AddHttpClients(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationUrl = configuration.GetValue<string>($"{SettingConstants.ApplicationUrl}");

            services.AddHttpClient<ShopHttpClient>(c =>
            {
                c.BaseAddress = new Uri(applicationUrl);
            });
        }
    }
}
