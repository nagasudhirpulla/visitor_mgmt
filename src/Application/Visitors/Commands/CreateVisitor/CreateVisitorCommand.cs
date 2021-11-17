using MediatR;
using System.Collections.Generic;
using System.Text;

namespace Application.Visitors.Commands.CreateVisitor
{
    public class CreateVisitorCommand : IRequest<List<string>>
    {
        public string Name { get; set; }
        public string ImageUri { get; set; }
        public string Organization { get; set; }
        public string Purpose { get; set; }
        public string ConcerenedPersonName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
