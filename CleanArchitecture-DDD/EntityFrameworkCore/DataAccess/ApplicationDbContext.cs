using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace DataAccess;

public sealed class ApplicationDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=TUGAY\\SQLEXPRESS;Initial Catalog=EFCoreDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<MainRole> MainRoles { get; set; }
    public DbSet<RoleMainRole> RoleMainRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RoleMainRole>()
            .HasKey(p => new { p.MainRoleId, p.RoleId });

        List<Category> categories = new();
        for (int i = 0; i < 10; i++)
        {
            categories.Add(new()
            {
                Id = i + 1,
                Name = "Category " + i
            });
        }

        modelBuilder.Entity<Category>()
            .HasData(categories);
        Random random = new();
        List<Product> producst = new();
        for (int i = 0; i < 100; i++)
        {
            producst.Add(new()
            {
                Id = i + 1,
                Name = "Product " + i,
                Price = i * 5,
                CategoryId = random.Next(1, 10)
            });
        }

        modelBuilder.Entity<Product>()
            .HasData(producst);

        modelBuilder.Entity<BillingDetail>()
            .HasDiscriminator<string>("Type")
            .HasValue<BankAccount>("BA")
            .HasValue<CreditCard>("CA");

        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(p => p.CategoryId);

        modelBuilder.Entity<Category>()
            .HasMany(p => p.Products)
            .WithOne(p => p.Category)
            .HasForeignKey(p => p.CategoryId);

        //comperasion key
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder
        .Property(p => p.Price)
        .HasColumnType("decimal(18,10)");

        builder
            .HasIndex(p => new { p.Name, p.Price })
            .IsUnique(true);

        //List<Product> producst = new();
        //for (int i = 0; i < 100; i++)
        //{
        //    producst.Add(new()
        //    {
        //        Id = i + 1,
        //        Name = "Product " + i,
        //        Price = i * 5
        //    });
        //}

        //builder
        //    .Property(p => p.Name)
        //    .HasConversion(p => p.Value, value => new(value));

        //builder
        //    .HasData(producst);
    }
}

public class Product
{
    public int Id { get; set; }

    [Column(TypeName = "varchar(200)")]
    public string Name { get; set; }

    public decimal Price { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }

    public Category Category { get; set; }
}

public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}

public sealed record Name(string Value);

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class MainRole
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class RoleMainRole
{
    //comperison key
    public int RoleId { get; set; }

    public int MainRoleId { get; set; }
}

public class BillingDetail
{
    public int Id { get; set; }
    public string Owner { get; set; }
    public string Number { get; set; }
}

public class BankAccount : BillingDetail
{
    public string BankName { get; set; }
}

public class CreditCard : BillingDetail
{
    public string CartType { get; set; }
}