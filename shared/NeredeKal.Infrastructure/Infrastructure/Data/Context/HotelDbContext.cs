using NeredeKal.Infrastructure.Infrastructure.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NeredeKal.Infrastructure.Infrastructure.Data.Context
{
    public class HotelDbContext(DbContextOptions<HotelDbContext> options) : DbContext(options), IHotelDbContext
    {
        public DbSet<HotelEntity> Hotels { get; set; }
        public DbSet<ContactEntity> Contacts { get; set; }
        public DbSet<ReportEntity> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public class HotelDbtContextDesignFactory : IDesignTimeDbContextFactory<HotelDbContext>
        {
            public HotelDbContext CreateDbContext(string[] args)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .AddEnvironmentVariables()
                    .Build();

                var optionsBuilder = new DbContextOptionsBuilder<HotelDbContext>()
                    .UseNpgsql(configuration.GetConnectionString("HotelDb"))
                    .UseQueryTrackingBehavior(queryTrackingBehavior: QueryTrackingBehavior.NoTracking);

                return new HotelDbContext(optionsBuilder.Options);
            }
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {

            var entities = ChangeTracker.Entries().Where(x =>
                         x.Entity is IBaseEntity &&
                         (x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted)
            );


            foreach (var entity in entities)
            {


                if (entity.Entity is IBaseEntity)
                {
                    switch (entity.State)
                    {
                        case EntityState.Modified:
                            ((IBaseEntity)entity.Entity).ModifiedAt = DateTime.UtcNow;
                            break;
                        case EntityState.Added:
                            ((IBaseEntity)entity.Entity).CreatedAt = DateTime.UtcNow;
                            break;
                    }
                }
            }
            return await base.SaveChangesAsync(true, cancellationToken);
        }
    }
}
