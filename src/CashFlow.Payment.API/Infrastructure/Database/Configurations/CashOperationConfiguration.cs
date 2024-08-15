using CashFlow.Payment.API.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Payment.API.Infrastructure.Database.Configurations
{
    public class CashOperationConfiguration : IEntityTypeConfiguration<CashOperationEntity>
    {
        public void Configure(EntityTypeBuilder<CashOperationEntity> builder)
        {
            builder.HasIndex(a => a.Id);

            builder.Property(a => a.Type).IsRequired();
            builder.Property(a => a.Value).IsRequired();
            builder.Property(a => a.Description).HasMaxLength(100).IsRequired();
        }
    }
}
