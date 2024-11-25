using FluentValidation;

namespace E_Commerce2Business_V01.Payloads.Validations
{
    public class RegistrationPayloadValidation : AbstractValidator<RegistrationPayload>
    {
        public RegistrationPayloadValidation()
        {
            RuleFor(r => r.DisplayName)
                .NotEmpty().WithMessage("the name is required")
                .MinimumLength(3).WithMessage("name cannot be less than 3 letters")
                .MaximumLength(50).WithMessage("name cannot be greater than 50 letters");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("email is required")
                .EmailAddress().WithMessage("invalid email");
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password cannot be empty.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches("[0-9]").WithMessage("Password must contain at least one digit.")
                .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
        }
    }
}
