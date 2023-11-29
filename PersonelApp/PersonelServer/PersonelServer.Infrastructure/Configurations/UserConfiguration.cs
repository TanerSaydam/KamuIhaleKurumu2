using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonelServer.Domain.Users;

namespace PersonelServer.Infrastructure.Configurations;
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.OwnsOne(p => p.Address);
        builder.Property(p => p.Name).HasConversion(name => name.Value, value => new(value));
        builder.Property(p => p.Lastname).HasConversion(lastname => lastname.Value, value => new(value));
        builder.Property(p => p.PhoneNumber).HasConversion(phoneNumber => phoneNumber.Value, value => new(value));
        builder.Property(p => p.Email).HasConversion(email => email.Value, value => new(value));
    }
}
