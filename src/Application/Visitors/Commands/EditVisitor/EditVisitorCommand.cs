using AutoMapper;
using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Common.Mappings.MappingProfile;

namespace Application.Visitors.Commands.EditVisitor
{
    public class EditVisitorCommand : IRequest<List<string>>, IMapFrom<VisitorEntry>
    {
        public int Id { get; set; }
        public string VisitorName { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string VistorAddress { get; set; }
        public string IdType { get; set; }
        public string IdProofNumber { get; set; }
        public string Devices { get; set; }
        public string PurposeOfVisit { get; set; }
        public string PersonToMeet { get; set; }
        public DateTime InTime { get; set; }
        public DateTime? OutTime { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<VisitorEntry, EditVisitorCommand>();
        }
    }
}
