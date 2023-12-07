using Domain.Models.DomainModels;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.DomainModels;

namespace WebApplication1.Context;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer()
    }*/

    public DbSet<Address> Addresses { get; set; }
    public DbSet<Operator> Operators { get; set; }
    public DbSet<Package> Packages { get; set; }
    public DbSet<Warehouse> Warehouses { get; set; }
    public DbSet<OperatorValidation> OperatorValidations { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(e => e.Name).HasColumnName("Name").IsRequired();
            entity.Property(e => e.Email).HasColumnName("Email").IsRequired();
            entity.Property(e => e.Password).HasColumnName("Password").IsRequired();
            entity.Property(e => e.PreferredUsername).HasColumnName("PreferredUsername").IsRequired();
            entity.Property(e => e.GivenName).HasColumnName("GiveName").IsRequired();
            entity.Property(e => e.FamilyName).HasColumnName("FamilyName").IsRequired();
            entity.Property(e => e.EmailVerified).HasColumnName("EmailVerified").IsRequired();
            entity.Property(e => e.TenantId).HasColumnName("TenantId").IsRequired(false);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasMany(e => e.Roles)
                .WithMany(e => e.Users)
                .UsingEntity(
                    "UserRole",
                    r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId")
                );
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt)
                .HasColumnName("CreatedAt")
                .HasColumnType("timestamp with time zone")
                .IsRequired();

            entity.Property(e => e.Name).HasColumnName("Name").IsRequired();

            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .UsingEntity(
                    "UserRole",
                    r => r.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne(typeof(User)).WithMany().HasForeignKey("UserId")
                );
        });
        
        
        modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshToken");
                
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("Id").ValueGeneratedOnAdd();
                entity.Property(e => e.CreatedAt)
                    .HasColumnName("CreatedAtUtc")
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();
                
                
                entity.Property(e => e.Token).HasColumnName("Token").IsRequired();
                entity.Property(e => e.ExpiresAt)
                    .HasColumnName("ExpiresAt")
                    .HasColumnType("timestamp with time zone")
                    .IsRequired();
            }
        );
        
        
        /*
        modelBuilder.Entity<Warehouse>()
            .HasOne(w => w.Address)
            .WithOne(a => a.Warehouse)
            .HasForeignKey<Address>(w => w.WarehouseId);

        modelBuilder.Entity<Warehouse>()
            .HasOne(w => w.Inventory)
            .WithOne(i => i.Warehouse)
            .HasForeignKey<Inventory>(i => i.WarehouseId);
            */

        /*modelBuilder.Entity<Warehouse>()
            .HasOne(w)*/

        /*modelBuilder.Entity<Package>()
            .HasOne(p => p.Reserved)
            .WithOne( r => r.Package)
            .HasForeignKey<Reserved>(r => r.PackageId);

        modelBuilder.Entity<Inventory>()
            .HasMany(i => i.Packages)
            .WithOne(p => p.Inventory)
            .HasForeignKey(p => p.InventoryId);*/

    }
}