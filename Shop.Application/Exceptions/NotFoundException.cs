namespace Shop.Application.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"Entity {name} with key {key} was not found") { }
    }
}
