﻿using DogusBootcampProject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DogusBootcampProject.Data
{
    public class BlogDbContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 🔄 Blog silinirse yorumlar da silinsin
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Blog)
                .WithMany(b => b.Comments)
                .HasForeignKey(c => c.BlogId)
                .OnDelete(DeleteBehavior.Cascade); // 🔥 Değiştirildi

            // User silinirse yorumlar silinmesin (gerekirse düzenlenebilir)
            modelBuilder.Entity<Comment>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Yorum cevap ilişkisi (admin yanıtı)
            modelBuilder.Entity<Comment>()
                .HasMany(c => c.Replies)
                .WithOne(c => c.ParentComment)
                .HasForeignKey(c => c.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
