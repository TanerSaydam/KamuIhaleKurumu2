using CleanArchitecture.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

internal sealed class ChatRoomDetailConfiguration : IEntityTypeConfiguration<ChatRoomDetail>
{
    public void Configure(EntityTypeBuilder<ChatRoomDetail> builder)
    {
        builder.ToTable("ChatRoomDetails");
    }
}