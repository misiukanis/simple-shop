using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;
using Shop.Infrastructure.Persistence;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;
using Shop.Shared.Constants;

namespace Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // MongoDb
            services.AddSingleton<MongoDbContext>();
            services.AddTransient(typeof(IDbRepository<>), typeof(MongoDbRepository<>));

            // EventStore
            var eventStoreConnectionString = configuration.GetValue<string>($"{SettingConstants.DatabaseSettings}:{SettingConstants.EventStoreConnectionString}");
            services.AddEventStoreClient(eventStoreConnectionString);

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddScoped<IEventStoreRepository, EventStoreRepository>();            

            return services;
        }
    }
}
