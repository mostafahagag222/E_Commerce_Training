using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce2Business_V01.Payloads.Validations
{
    public class AddToBasketPayloadValidation : AbstractValidator<AddToBasketPayload>
    {
        public AddToBasketPayloadValidation()
        {
            RuleFor(a=>a.Id).NotEmpty();
        }

    }
}
