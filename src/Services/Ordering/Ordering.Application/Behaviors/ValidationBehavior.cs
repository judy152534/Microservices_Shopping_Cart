using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Exceptions.ValidationException;


namespace Ordering.Application.Behaviors
{
    // Accomodate all validatiors & run them
    // Throw custom ValidationExceptions if needed
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any()) 
            {
                var context = new ValidationContext<TRequest>(request);
                var validateResult = await Task.WhenAll(_validators.Select(p => p.ValidateAsync(context, cancellationToken)));
                var failures = validateResult.SelectMany(p => p.Errors).Where( f => f != null).ToList();
                if (failures.Count != 0) 
                {
                    throw new ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
