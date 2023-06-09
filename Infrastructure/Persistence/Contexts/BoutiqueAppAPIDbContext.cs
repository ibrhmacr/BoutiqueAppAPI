using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Contexts;

public class BoutiqueAppAPIDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public BoutiqueAppAPIDbContext(DbContextOptions options) : base(options) //Class Libraryde bu nesnenin cons ne gidecegini bilmedigi icin Unable to create an object of type 'BoutiqueAppAPIDbContext' hatasini verir bunun designtime factory olusturulmasi gerekmektedir.
    {
        //Class Libraryde bu nesnenin cons ne gidecegini bilmedigi icin
        //Unable to create an object of type 'BoutiqueAppAPIDbContext' hatasini verir bunun Designtimefactory olusturulmasi gerekmektedir.
    }

    public DbSet<Product> Products { get; set; }

    public DbSet<SubCategory> SubCategories { get; set; }

    public DbSet<Category> Categories { get; set; }

    public DbSet<ProductImageFile> ProductImageFiles { get; set; }

    public DbSet<Cart> Carts { get; set; }

    public DbSet<CartItem> CartItems { get; set; }

    public DbSet<Address> Addresses { get; set; }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Order>()
            .HasKey(b => b.Id);

        builder.Entity<Order>()
            .HasIndex(o => o.OrderCode)
            .IsUnique();

        builder.Entity<Cart>()
            .HasOne(c => c.Order)
            .WithOne(o => o.Cart)
            .HasForeignKey<Order>(o => o.Id);
        
        base.OnModelCreating(builder);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //ChangeTracker : Entityler üzerinden yapılan değişiklerin ya da yeni eklenen verinin yakalanmasını sağlayan propertydir.
        //Update operasyonlarında Track edilen verileri yakalayıp elde etmemizi sağlar.
        //Bu sayede veritabanina islem yapildiiginda created dateini tutabilecegiz.

        var datas = ChangeTracker
            .Entries<BaseEntity>();

        foreach (var data in datas)
        {
            _ = data.State switch
            {
                EntityState.Added => data.Entity.CreatedDate = DateTime.UtcNow,
                _ => DateTime.UtcNow
            };
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}