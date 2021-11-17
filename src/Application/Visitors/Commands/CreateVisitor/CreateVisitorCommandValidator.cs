using FluentValidation;
using System;

namespace Application.Visitors.Commands.CreateVisitor
{
    public class CreateVisitorCommandValidator : AbstractValidator<CreateVisitorCommand>
    {
        public CreateVisitorCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.ImageUri).NotEmpty();
            RuleFor(x => x.Organization).NotEmpty();
            RuleFor(x => x.Purpose).NotEmpty();
            RuleFor(x => x.ConcerenedPersonName).NotEmpty();
        }
    }
}
