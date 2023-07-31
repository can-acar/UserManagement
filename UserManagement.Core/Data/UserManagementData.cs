using Microsoft.EntityFrameworkCore;
using UserManagement.Core.Models;

namespace UserManagement.Core.Data;

public class UserManagementData : DbContext
{
    public UserManagementData()
    {
    }

    public UserManagementData(DbContextOptions<UserManagementData> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.Username).IsRequired();
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });
    }


    public DbSet<User> Users { get; set; }
}