using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visitors.Commands.EditVisitor
{
    public class EditVisitorCommandHandler : IRequestHandler<EditVisitorCommand, List<string>>
    {
        private readonly IAppDbContext _context;
        private readonly ILogger<EditVisitorCommandHandler> _logger;

        public EditVisitorCommandHandler(IAppDbContext context, ILogger<EditVisitorCommandHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<string>> Handle(EditVisitorCommand request, CancellationToken cancellationToken)
        {
            List<string> errors = new();
            VisitorEntry vstr = await _context.VisitorEntries.Where(v => v.Id == request.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (vstr == null)
            {
                errors.Add($"Unable to find visitor entry with id {request.Id}");
            }

            // change the visitor entry fields based on the request
            vstr.VisitorName = request.VisitorName;
            vstr.CompanyName = request.CompanyName;
            vstr.PhoneNumber = request.PhoneNumber;
            vstr.VistorAddress = request.VistorAddress;
            vstr.IdType = request.IdType;
            vstr.IdProofNumber = request.IdProofNumber;
            vstr.Devices = request.Devices;
            vstr.PurposeOfVisit = request.PurposeOfVisit;
            vstr.PersonToMeet = request.PersonToMeet;
            vstr.InTime = request.InTime;
            vstr.OutTime = request.OutTime;
            
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.VisitorEntries.Any(e => e.Id == request.Id))
                {
                    return new List<string>() { $"Visitor entry Id {request.Id} not present for editing" };
                }
                else
                {
                    throw;
                }
            }

            return errors;
        }
    }
}
