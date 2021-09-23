using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException: ApplicationException
    {
        public ValidationException()
           : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        // constructor 接受來自任何validator的失敗參數
        public ValidationException(IEnumerable<ValidationFailure> failures)
            : this()
        {
            // 這裡的PropertyName 指的是例如Username 或 EmailAddress等 (RuleFor 驗證的地方)
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}
