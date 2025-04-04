using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserProfile.TrustedContacts
{
    namespace YourNamespace.UserProfile.TrustedContacts.Commands.RejectTrustedContactInvitation
    {
        public class RejectTrustedContactInvitationValidator : AbstractValidator<RejectTrustedContactInvitationCommand>
        {
            public RejectTrustedContactInvitationValidator()
            {
                RuleFor(x => x.CustomerId)
                    .NotEmpty().WithMessage("CustomerId is required.");

                RuleFor(x => x.InviterId)
                    .NotEmpty().WithMessage("InviterId is required.");
            }
        }
    }
}
