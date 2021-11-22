using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.Visitors.Commands.EditVisitor
{
    public class EditVisitorCommandValidator : AbstractValidator<EditVisitorCommand>
    {
        public EditVisitorCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.VisitorName).NotEmpty();
            RuleFor(x => x.CompanyName).NotEmpty();
            RuleFor(x => x.PhoneNumber).NotEmpty();
            RuleFor(x => x.IdType).NotEmpty();
            RuleFor(x => x.IdProofNumber).NotEmpty();
            RuleFor(x => x.PurposeOfVisit).NotEmpty();
            RuleFor(x => x.PersonToMeet).NotEmpty();
            RuleFor(x => x.InTime).NotEmpty();
        }
    }
}
