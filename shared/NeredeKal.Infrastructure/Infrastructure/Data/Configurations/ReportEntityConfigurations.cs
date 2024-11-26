using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NeredeKal.Infrastructure.Infrastructure.Data.Entities;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Configurations
{
    public class ReportEntityConfigurations : IEntityTypeConfiguration<ReportEntity>
    {
        public void Configure(EntityTypeBuilder<ReportEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventType).IsRequired().HasMaxLength(2);
            builder.Property(x => x.Status).IsRequired().HasMaxLength(2);
            builder.Property(x => x.Query).IsRequired().HasMaxLength(100);
        }
    }
}
