using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharingApplication.Infrastructure.Data {
    public class PhotoSharingDbContext : DbContext {
        public PhotoSharingDbContext(DbContextOptions<PhotoSharingDbContext> options) : base(options) {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Photo>()
                .Property(b => b.Title)
                .HasMaxLength(100);
            modelBuilder.Entity<Photo>()
                .Property(b => b.Description)
                .HasMaxLength(250);
            modelBuilder.Entity<Photo>()
                .Property(b => b.ContentType)
                .HasMaxLength(30);

            modelBuilder.Entity<Comment>()
                .Property(b => b.Title)
                .HasMaxLength(100);
            modelBuilder.Entity<Comment>()
                .Property(b => b.Body)
                .HasMaxLength(250);
        }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
