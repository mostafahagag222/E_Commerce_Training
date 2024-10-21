using E_Commerce1DB_V01;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Payloads.Validations
{
    public class GetProductsPayloadValidation : AbstractValidator<GetProductsPayload>
    {
        public GetProductsPayloadValidation()
        {
            RuleFor(p => p.PageIndex)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(p => p.PageSize)
                .NotEmpty()
                .GreaterThan(0);
            RuleFor(p => p.Sort)
                .Must(BeAValidOrEmptySortOption).WithMessage("invalid sort option");
        }
        private bool BeAValidOrEmptySortOption(string Sort)
        {
            // Allow null, empty, or whitespace, or valid enum values
            return string.IsNullOrWhiteSpace(Sort) || Enum.TryParse<SortOptions>(Sort.Trim().ToLower(), true, out _);
        }
    }
}
