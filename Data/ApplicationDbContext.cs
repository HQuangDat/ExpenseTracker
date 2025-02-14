using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
namespace ExpenseTracker.Data
{
    public partial class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Expense> Expenses { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=HQD;Database=ExpenseTracker;Trusted_Connection=true;TrustServerCertificate=true");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0BF4F4224E");

                entity.HasIndex(e => e.Name, "UQ__Categori__737584F6A798516B").IsUnique();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Expense>(entity =>
            {
                entity.HasKey(e => e.ExpenseId).HasName("PK__Expenses__1445CFD378DF88DD");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Date).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(255);

                entity.HasOne(d => d.Category).WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK__Expenses__Catego__46E78A0C");

                entity.HasOne(d => d.User).WithMany(p => p.Expenses)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Expenses__UserId__45F365D3");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1AC8C9351F");

                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61600E98963C").IsUnique();

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CBFB03880");

                entity.HasIndex(e => e.Username, "UQ__Users__536C85E47A74EA50").IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534313FF9E0").IsUnique();

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("(getdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.PasswordHash).HasMaxLength(255);
                entity.Property(e => e.Username).HasMaxLength(50);

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
                            j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760AD5B227590");
                            j.ToTable("UserRoles");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
