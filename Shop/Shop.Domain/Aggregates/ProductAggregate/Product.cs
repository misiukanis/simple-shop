using Shop.Domain.Core;
using Shop.Domain.Events;

namespace Shop.Domain.Aggregates.ProductAggregate
{
    public class Product : AggregateRoot
    {
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool IsDeleted { get; private set; }


        public Product() { }

        public Product(Guid id, string name, decimal price)
        {
            ApplyChange(new ProductCreatedEvent(id, name, price));
        }


        public void Change(string name, decimal price)
        {
            ApplyChange(new ProductChangedEvent(Id, name, price));
        }

        public void Delete()
        {
            ApplyChange(new ProductDeletedEvent(Id));
        }


        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case ProductCreatedEvent e:
                    Id = e.Id;
                    Name = e.Name;
                    Price = e.Price;
                    break;
                case ProductChangedEvent e:
                    Name = e.Name;
                    Price = e.Price;
                    break;
                case ProductDeletedEvent e:
                    IsDeleted = true;
                    break;
            }
        }
    }
}
