using FluentValidation;
using MediatR;

namespace Shop.Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

            var validationFailures = validationResults
                .Where(result => result.Errors.Any())
                .SelectMany(result => result.Errors)
                .ToList();

            if (validationFailures.Any())
            {
                throw new ValidationException(validationFailures);
            }

            return await next();
        }
    }
}
