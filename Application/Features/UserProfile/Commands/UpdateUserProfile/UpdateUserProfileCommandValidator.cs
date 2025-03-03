using Application.Features.UserProfile.Commands.UpdateUserProfile;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class UpdateCustomerProfileCommandValidator : AbstractValidator<UpdateCustomerProfileCommand>
    {
        public UpdateCustomerProfileCommandValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");
        }
    }
}
