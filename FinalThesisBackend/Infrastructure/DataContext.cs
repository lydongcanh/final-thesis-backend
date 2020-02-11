﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FinalThesisBackend.Core.Entities;

namespace FinalThesisBackend.Infrastructure
{
    public class DataContext: DbContext
    {
        public DbSet<Category> Categories { get; protected set; }
        public DbSet<Account> Accounts { get; protected set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureCategory(modelBuilder.Entity<Category>());
            CongifureAccount(modelBuilder.Entity<Account>());
        }

        protected virtual void ConfigureCategory(EntityTypeBuilder<Category> builder)
        {
            builder.HasOne(c => c.ParentCategory)
                   .WithMany(c => c.ChildrenCategories)
                   .HasForeignKey(c => c.ParentCategoryId);

            builder.Property(c => c.Name).HasMaxLength(50).IsRequired();
            builder.Property(c => c.Description).HasMaxLength(100);
        }

        protected virtual void CongifureAccount(EntityTypeBuilder<Account> builder)
        {
            builder.Property(a => a.Username).HasMaxLength(100).IsRequired();
            builder.Property(a => a.Password).HasMaxLength(100).IsRequired();
            builder.Property(a => a.AccountType).HasMaxLength(20).IsRequired();
        }
    }
}
