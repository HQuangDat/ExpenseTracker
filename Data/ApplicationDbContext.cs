using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTracker.Data
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<Wallet> Wallets { get; set; }

        public virtual DbSet<WalletType> WalletTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B17940A7A");
            });


            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId).HasName("PK__Expenses__1445CFD3FABE84A3");

                entity.HasOne(d => d.Category).WithMany(p => p.Expenses).HasConstraintName("FK__Expenses__Catego__5070F446");

                entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__UserId__4E88ABD4");

                entity.HasOne(d => d.Wallet).WithMany(p => p.Expenses)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__Wallet__4F7CD00D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A574BC200");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4C62EC0A9E");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        r => r.HasOne<Role>().WithMany()
                            .HasForeignKey("RoleId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__UserRoles__RoleI__403A8C7D"),
                        l => l.HasOne<User>().WithMany()
                            .HasForeignKey("UserId")
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK__UserRoles__UserI__3F466844"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD8843AB45");
                            j.ToTable("UserRoles");
                        });
            });

            modelBuilder.Entity<Wallet>(entity =>
            {
                entity.HasKey(e => e.WalletId).HasName("PK__Wallets__84D4F90EE6E74C62");

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User).WithMany(p => p.Wallets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wallets__UserId__47DBAE45");

                entity.HasOne(d => d.WalletType).WithMany(p => p.Wallets)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Wallets__WalletT__48CFD27E");
            });

            modelBuilder.Entity<WalletType>(entity =>
            {
                entity.HasKey(e => e.WalletTypeId).HasName("PK__WalletTy__6807E8B25746FEA8");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
