using FluentValidation;
using System;

namespace Application.Visitors.Commands.CreateVisitor
{
    public class CreateVisitorCommandValidator : AbstractValidator<CreateVisitorCommand>
    {
        public CreateVisitorCommandValidator()
        {
            RuleFor(x => x.VisitorName).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.IdType).NotEmpty();
            RuleFor(x => x.IdProofNumber).NotEmpty();
            RuleFor(x => x.PurposeOfVisit).NotEmpty();
            RuleFor(x => x.PersonToMeet).NotEmpty();
            RuleFor(x => x.InTime).NotEmpty();
            RuleFor(x => x.ImageUri).NotEmpty();
        }
    }
}
