using AcikArrtirmaServer.Domain.Notices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AcikArrtirmaServer.Infrastructure.Configurations;

internal sealed class NoticeConfiguration : IEntityTypeConfiguration<Notice>
{
    public void Configure(EntityTypeBuilder<Notice> builder)
    {
        builder.ToTable("Notices");
        builder.OwnsOne(p => p.StartingFee, startingFeeBuilder =>
        {
            startingFeeBuilder.Property(p => p.Amount).HasColumnType("money");
        });
    }
}