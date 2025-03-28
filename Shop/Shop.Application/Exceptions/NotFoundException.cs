namespace Shop.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string entityName, object key, object value) : base($"Entity {entityName} for {key} = {value} was not found") { }
    }
}
