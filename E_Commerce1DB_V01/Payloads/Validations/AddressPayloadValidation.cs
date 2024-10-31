using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce1DB_V01.Payloads.Validations
{
    public class AddressPayloadValidation : AbstractValidator<AddAddressPayload>
    {
        public AddressPayloadValidation()
        {
            RuleFor(a => a.City)
                .NotEmpty();
            RuleFor(a => a.zipcode)
                .NotEmpty();
            RuleFor(a => a.Street)
                .NotEmpty();
            RuleFor(a => a.State)
                .NotEmpty();
            RuleFor(a => a.FirstName)
                .NotEmpty();
            RuleFor(a => a.LastName)
                .NotEmpty();
        }

    }
}
