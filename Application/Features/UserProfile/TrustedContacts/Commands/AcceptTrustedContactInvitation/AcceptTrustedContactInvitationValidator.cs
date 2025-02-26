using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile
{
    public class AcceptTrustedContactInvitationValidator : AbstractValidator<AcceptTrustedContactInvitationCommand>
    {
        public AcceptTrustedContactInvitationValidator()
        {
            RuleFor(x => x.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.InviterId)
                .NotEmpty().WithMessage("InviterId is required.");
        }
    }
}
