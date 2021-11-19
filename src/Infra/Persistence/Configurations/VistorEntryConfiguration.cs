using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Core.Entities;

namespace Infra.Persistence.Configurations
{
    class VistorEntryConfiguration : IEntityTypeConfiguration<VisitorEntry>
    {
        public void Configure(EntityTypeBuilder<VisitorEntry> builder)
        {
            builder.Property(b => b.VisitorName)
               .IsRequired();

            builder.Property(b => b.CompanyName)
               .IsRequired();

            builder.Property(b => b.PhoneNumber)
               .IsRequired();

            builder.Property(b => b.IdType)
               .IsRequired();

            builder.Property(b => b.IdProofNumber)
               .IsRequired();

            builder.Property(b => b.PurposeOfVisit)
               .IsRequired();

            builder.Property(b => b.PersonToMeet)
               .IsRequired();

            builder.Property(b => b.InTime)
               .IsRequired();

            builder.Property(b => b.ImageFilename)
               .IsRequired();


            //builder.Property(b => b.VisitorName)
            //    .IsRequired()
            //    .HasMaxLength(250);
        }
    }
}