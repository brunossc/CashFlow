using CashFlow.Financial.API.Domain.Entities;
using CashFlow.Financial.API.Infrastructure.Database.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CashFlow.Financial.API.Infrastructure.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            this.Database.EnsureCreatedAsync().Wait();
        }

        public DbSet<CashOperationEntity> CashOperation => Set<CashOperationEntity>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            builder.ApplyConfiguration(new CashOperationConfiguration());

            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
