using Core.Common;
using System;

namespace Core.Entities
{
    public class VisitorEntry : AuditableEntity
    {
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
        public string ImageFilename { get; set; }
    }
}
