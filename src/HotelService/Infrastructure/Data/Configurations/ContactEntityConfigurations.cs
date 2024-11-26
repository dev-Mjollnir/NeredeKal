using HotelService.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace HotelService.Infrastructure.Data.Configurations
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
