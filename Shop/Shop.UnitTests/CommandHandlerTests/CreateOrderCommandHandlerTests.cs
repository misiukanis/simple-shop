using Moq;
using Shop.Application.Commands.CreateOrder;
using Shop.Domain.Aggregates.OrderAggregate;
using Shop.Domain.Aggregates.ProductAggregate;
using Shop.Domain.Interfaces.Dispatchers;
using Shop.Domain.Interfaces.Repositories;

namespace Shop.UnitTests.CommandHandlerTests
{
    public class CreateOrderCommandHandlerTests
    {
        private readonly Mock<IDomainEventDispatcher> _domainEventDispatcherMock;
        private readonly Mock<IEventStoreRepository> _eventStoreRepositoryMock;
        private readonly CreateOrderCommandHandler _handler;

        public CreateOrderCommandHandlerTests()
        {
            _domainEventDispatcherMock = new Mock<IDomainEventDispatcher>();
            _eventStoreRepositoryMock = new Mock<IEventStoreRepository>();
            _handler = new CreateOrderCommandHandler(_domainEventDispatcherMock.Object, _eventStoreRepositoryMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateOrderAndSaveIt()
        {
            // Arrange
            var command = new CreateOrderCommand(
                Guid.NewGuid(),
                Guid.NewGuid(),
                "New York",
                "Wall Street",
                new List<CreateOrderItemCommand>
                {
                    new(Guid.NewGuid(), 2)
                });

            var product = new Product(command.OrderItems[0].ProductId, "Test Product", 10);

            _eventStoreRepositoryMock
                .Setup(repo => repo.LoadAsync<Product>(It.IsAny<Guid>()))
                .ReturnsAsync(product);

            _eventStoreRepositoryMock
                .Setup(repo => repo.SaveAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            _domainEventDispatcherMock
                .Setup(dispatcher => dispatcher.DispatchEventsAsync(It.IsAny<Order>()))
                .Returns(Task.CompletedTask);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _eventStoreRepositoryMock.Verify(repo => repo.LoadAsync<Product>(command.OrderItems[0].ProductId), Times.Once);
            _eventStoreRepositoryMock.Verify(repo => repo.SaveAsync(It.IsAny<Order>()), Times.Once);
            _domainEventDispatcherMock.Verify(dispatcher => dispatcher.DispatchEventsAsync(It.IsAny<Order>()), Times.Once);
        }
    }
}
