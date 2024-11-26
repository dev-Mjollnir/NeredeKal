using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Configurations
{
    public class ContactEntityConfigurations : IEntityTypeConfiguration<ContactEntity>
    {
        public void Configure(EntityTypeBuilder<ContactEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ContactInfoType).IsRequired().HasMaxLength(2);
            builder.Property(x => x.ContactInfoContent).IsRequired().HasMaxLength(100);

            builder
              .HasOne<HotelEntity>()
              .WithMany(h => h.Contacts)
              .HasForeignKey(ci => ci.HotelId);
        }
    }
}
