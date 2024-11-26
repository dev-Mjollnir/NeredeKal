using HotelService.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelService.Infrastructure.Data.Configurations
{
    public class HotelEntityConfigurations : IEntityTypeConfiguration<HotelEntity>
    {
        public void Configure(EntityTypeBuilder<HotelEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.FirmName).IsRequired().HasMaxLength(250);
            builder.Property(x => x.ResponsibleName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ResponsibleSurname).IsRequired().HasMaxLength(50);
        }
    }
}
