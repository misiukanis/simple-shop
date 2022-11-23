using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.Domain.Dispatchers.Interfaces;
using Shop.Domain.Repositories.Interfaces;
using Shop.Infrastructure.Persistence.MongoDb;
using Shop.Infrastructure.Repositories;
using Shop.Infrastructure.Services;
using Shop.Shared.Constants;

namespace Shop.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<MongoDbContext>();
            services.AddSingleton<IMongoDbRepository, MongoDbRepository>();

            var eventStoreConnectionString = configuration.GetRequiredSection(AppSettingConstants.DatabaseSettings)[AppSettingConstants.EventStoreConnectionString];
            services.AddEventStoreClient(eventStoreConnectionString);

            services.AddSingleton<IDomainEventDispatcher, DomainEventDispatcher>();
            services.AddTransient<IEventStoreRepository, EventStoreRepository>(); 

            return services;
        }
    }
}
