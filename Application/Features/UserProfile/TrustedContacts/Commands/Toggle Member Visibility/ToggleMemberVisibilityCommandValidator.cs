using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FluentValidation;

namespace Application.Features.UserProfile
{
    public class ToggleProfileVisibilityCommandValidator : AbstractValidator<ToggleMemberVisibilityCommand>
    {
        public ToggleProfileVisibilityCommandValidator()
        {
            RuleFor(x => x.model.CustomerId)
                .NotEmpty().WithMessage("CustomerId is required.");

            RuleFor(x => x.model.FriendId)
                .NotEmpty().WithMessage("FriendId is required.");
        }
    }
}

