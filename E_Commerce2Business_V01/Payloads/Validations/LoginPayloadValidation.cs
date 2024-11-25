using FluentValidation;

namespace E_Commerce2Business_V01.Payloads.Validations
{
    public class LoginPayloadValidation : AbstractValidator<LoginPayload>
    {
        public LoginPayloadValidation()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("invalid email")
                .EmailAddress().WithMessage("invalid email");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("invalid paswoord")
                .MinimumLength(8).WithMessage("invalid paswoord")
                .Matches("[A-Z]").WithMessage("invalid paswoord")
                .Matches("[a-z]").WithMessage("invalid paswoord")
                .Matches("[0-9]").WithMessage("invalid paswoord")
                .Matches("[^a-zA-Z0-9]").WithMessage("invalid paswoord");
        }
    }
}
