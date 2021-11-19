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

namespace Application.Visitors.Queries.GetVisitorsByDates
{
    public class GetVisitorsByDatesQuery : IRequest<List<VisitorEntry>>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public class GetVisitorsByDatesQueryHandler : IRequestHandler<GetVisitorsByDatesQuery, List<VisitorEntry>>
        {
            private readonly IAppDbContext _context;

            public GetVisitorsByDatesQueryHandler(IAppDbContext context)
            {
                _context = context;
            }

            public async Task<List<VisitorEntry>> Handle(GetVisitorsByDatesQuery request, CancellationToken cancellationToken)
            {
                var endDt = new DateTime(request.EndDate.Year, request.EndDate.Month, request.EndDate.Day, 23, 59, 59);
                List<VisitorEntry> res = await _context.VisitorEntries.Where(s => s.InTime >= request.StartDate && s.InTime <= endDt).ToListAsync(cancellationToken: cancellationToken);
                return res;
            }
        }
    }
}
