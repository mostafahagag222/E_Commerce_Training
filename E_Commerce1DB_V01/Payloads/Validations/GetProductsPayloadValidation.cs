using FluentValidation;

namespace E_Commerce1DB_V01.Payloads.Validations
{
    public class GetProductsPayloadValidation : AbstractValidator<GetProductsPagePayload>
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
