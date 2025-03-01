﻿// <auto-generated />
using System;
using ExpenseTracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ExpenseTracker.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250219045400_newDatabase")]
    partial class newDatabase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExpenseTracker.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoryId")
                        .HasName("PK__Categori__19093A0B17940A7A");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Expense", b =>
                {
                    b.Property<int>("ExpenseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExpenseId"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletId")
                        .HasColumnType("int");

                    b.HasKey("ExpenseId")
                        .HasName("PK__Expenses__1445CFD3FABE84A3");

                    b.HasIndex("CategoryId");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("RoleId")
                        .HasName("PK__Roles__8AFACE1A574BC200");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ExpenseTracker.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("UserId")
                        .HasName("PK__Users__1788CC4C62EC0A9E");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Wallet", b =>
                {
                    b.Property<int>("WalletId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletId"));

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("WalletTypeId")
                        .HasColumnType("int");

                    b.HasKey("WalletId")
                        .HasName("PK__Wallets__84D4F90EE6E74C62");

                    b.HasIndex("UserId");

                    b.HasIndex("WalletTypeId");

                    b.ToTable("Wallets");
                });

            modelBuilder.Entity("ExpenseTracker.Models.WalletType", b =>
                {
                    b.Property<int>("WalletTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WalletTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("WalletTypeId")
                        .HasName("PK__WalletTy__6807E8B25746FEA8");

                    b.ToTable("WalletTypes");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PK__UserRole__AF2760AD8843AB45");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);
                });

            modelBuilder.Entity("ExpenseTracker.Models.Expense", b =>
                {
                    b.HasOne("ExpenseTracker.Models.Category", "Category")
                        .WithMany("Expenses")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("FK__Expenses__Catego__5070F446");

                    b.HasOne("ExpenseTracker.Models.User", "User")
                        .WithMany("Expenses")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Expenses__UserId__4E88ABD4");

                    b.HasOne("ExpenseTracker.Models.Wallet", "Wallet")
                        .WithMany("Expenses")
                        .HasForeignKey("WalletId")
                        .IsRequired()
                        .HasConstraintName("FK__Expenses__Wallet__4F7CD00D");

                    b.Navigation("Category");

                    b.Navigation("User");

                    b.Navigation("Wallet");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Wallet", b =>
                {
                    b.HasOne("ExpenseTracker.Models.User", "User")
                        .WithMany("Wallets")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__Wallets__UserId__47DBAE45");

                    b.HasOne("ExpenseTracker.Models.WalletType", "WalletType")
                        .WithMany("Wallets")
                        .HasForeignKey("WalletTypeId")
                        .IsRequired()
                        .HasConstraintName("FK__Wallets__WalletT__48CFD27E");

                    b.Navigation("User");

                    b.Navigation("WalletType");
                });

            modelBuilder.Entity("UserRole", b =>
                {
                    b.HasOne("ExpenseTracker.Models.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK__UserRoles__RoleI__403A8C7D");

                    b.HasOne("ExpenseTracker.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK__UserRoles__UserI__3F466844");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Category", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ExpenseTracker.Models.User", b =>
                {
                    b.Navigation("Expenses");

                    b.Navigation("Wallets");
                });

            modelBuilder.Entity("ExpenseTracker.Models.Wallet", b =>
                {
                    b.Navigation("Expenses");
                });

            modelBuilder.Entity("ExpenseTracker.Models.WalletType", b =>
                {
                    b.Navigation("Wallets");
                });
#pragma warning restore 612, 618
        }
    }
}
