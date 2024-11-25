using FluentValidation;

namespace E_Commerce2Business_V01.Payloads.Validations
{
    public class AddToBasketPayloadValidation : AbstractValidator<UpdateBasketPayload>
    {
        public AddToBasketPayloadValidation()
        {
            RuleFor(a=>a.Id).NotEmpty();
        }

    }
}
