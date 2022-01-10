using Application.Common.Interfaces;
using Core.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Visitors.Queries.GetVisitorById
{
    public class GetVisitorByIdQuery : IRequest<VisitorEntry>
    {
        public int Id { get; set; }
        public class GetVisitorByIdQueryHandler : IRequestHandler<GetVisitorByIdQuery, VisitorEntry>
        {
            private readonly IAppDbContext _context;

            public GetVisitorByIdQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<VisitorEntry> Handle(GetVisitorByIdQuery request, CancellationToken cancellationToken)
            {
                VisitorEntry res = await _context.VisitorEntries
                                    .Where(s => s.Id == request.Id)
                                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
