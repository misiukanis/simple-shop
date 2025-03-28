using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Shop.Application.Commands.CreateCustomer;
using Shop.Application.Commands.CreateOrder;
using Shop.Application.Commands.CreateProduct;
using Shop.Application.ReadModels;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.Infrastructure.Persistence
{
    public static class DatabaseInitializer
    {
        public static async Task SeedDatabaseAsync(IServiceProvider serviceProvider)
        {
            var customerDbRepository = serviceProvider.GetRequiredService<IDbRepository<CustomerReadModel>>();
            var productDbRepository = serviceProvider.GetRequiredService<IDbRepository<ProductReadModel>>();

            if (await customerDbRepository.AnyAsync())
            {
                return; // DB has been seeded
            }

            var mediator = serviceProvider.GetRequiredService<IMediator>();

            // customers:
            await mediator.Send(
                new CreateCustomerCommand(Guid.NewGuid(), "John Smith"), 
                CancellationToken.None);

            await mediator.Send(
                new CreateCustomerCommand(Guid.NewGuid(), "Adam Nowak"),
                CancellationToken.None);

            await mediator.Send(
                new CreateCustomerCommand(Guid.NewGuid(), "Jan Kowalski"),
                CancellationToken.None);

            await mediator.Send(
                new CreateCustomerCommand(Guid.NewGuid(), "Albert Jones"),
                CancellationToken.None);

            await mediator.Send(
                new CreateCustomerCommand(Guid.NewGuid(), "Dominic Johnson"),
                CancellationToken.None);

            // products:
            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "T-shirt", 10),
                CancellationToken.None);

            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "Trousers", 20),
                CancellationToken.None);

            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "Umbrella", 30),
                CancellationToken.None);

            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "Jumper", 15),
                CancellationToken.None);

            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "Kettle", 45),
                CancellationToken.None);

            await mediator.Send(
                new CreateProductCommand(Guid.NewGuid(), "Ball", 25),
                CancellationToken.None);

            // orders:   
            var customers = await customerDbRepository.GetAllAsync();
            var products = await productDbRepository.GetAllAsync();

            await mediator.Send(
                new CreateOrderCommand(
                    Guid.NewGuid(), 
                    customers.ElementAt(0).Id,
                    "Warsaw",
                    "Wolska 5/4",
                    new List<CreateOrderItemCommand>
                    {
                        new CreateOrderItemCommand(products.ElementAt(0).Id, 1),
                        new CreateOrderItemCommand(products.ElementAt(1).Id, 2)
                    }),
                CancellationToken.None);

            await mediator.Send(
                new CreateOrderCommand(
                    Guid.NewGuid(),
                    customers.ElementAt(1).Id,
                    "Gdansk",
                    "Bracka 56/9",
                    new List<CreateOrderItemCommand>
                    {
                        new CreateOrderItemCommand(products.ElementAt(2).Id, 2),
                        new CreateOrderItemCommand(products.ElementAt(3).Id, 2)
                    }),
                CancellationToken.None);
        }
    }
}
