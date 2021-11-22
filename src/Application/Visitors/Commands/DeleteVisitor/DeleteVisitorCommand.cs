using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors.Commands.DeleteVisitor
{
    public class DeleteVisitorCommand : IRequest<List<string>>
    {
        public int Id { get; set; }
    }
}
