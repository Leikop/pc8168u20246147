using Microsoft.EntityFrameworkCore;
using PC28168.U202416147.Openpay.Platform.Settlements.Domain.Model.Aggregates;
using PC28168.U202416147.Openpay.Platform.Settlements.Domain.Model.ValueObjects;
using PC28168.U202416147.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using PC28168.U202416147.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Interceptors;

namespace PC28168.U202416147.Openpay.Platform.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        builder.AddInterceptors(new AuditableEntityInterceptor());
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<DisbursementOrder>().HasKey(d => d.Id);
        builder.Entity<DisbursementOrder>().Property(d => d.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<DisbursementOrder>()
            .Property(d => d.OrderTrackingCode)
            .HasConversion(valueObject => valueObject.Value, value => new OpenpayReference(value))
            .IsRequired();
        builder.Entity<DisbursementOrder>()
            .HasIndex(d => d.OrderTrackingCode)
            .IsUnique();
        builder.Entity<DisbursementOrder>()
            .Property(d => d.AffiliateStoreId)
            .HasConversion(valueObject => valueObject.Value, value => new StoreIdentifier(value))
            .IsRequired();
        builder.Entity<DisbursementOrder>().OwnsOne(d => d.ExecutionSchedule, schedule =>
        {
            schedule.WithOwner().HasForeignKey("Id");
            schedule.Property(s => s.ScheduledFrom).HasColumnName("ScheduledFrom").IsRequired();
            schedule.Property(s => s.ScheduledTo).HasColumnName("ScheduledTo").IsRequired();
        });
        builder.Entity<DisbursementOrder>().Navigation(d => d.ExecutionSchedule).IsRequired();
        builder.Entity<DisbursementOrder>().OwnsOne(d => d.TransferCap, transferCap =>
        {
            transferCap.WithOwner().HasForeignKey("Id");
            transferCap.Property(t => t.Amount).HasColumnName("TransferCapAmount").HasPrecision(18, 2).IsRequired();
            transferCap.Property(t => t.CurrencyCode).HasColumnName("TransferCapCurrencyCode").IsRequired();
        });
        builder.Entity<DisbursementOrder>().Navigation(d => d.TransferCap).IsRequired();
        builder.Entity<DisbursementOrder>().Property(d => d.Status).HasConversion<int>().IsRequired();
        builder.Entity<DisbursementOrder>().Property(d => d.User)
            .HasMaxLength(DisbursementOrder.UserMaxLength).IsRequired();
        builder.Entity<DisbursementOrder>().Property(d => d.Remarks)
            .HasMaxLength(DisbursementOrder.RemarksMaxLength);

        builder.UseSnakeCaseNamingConvention();
    }
}
