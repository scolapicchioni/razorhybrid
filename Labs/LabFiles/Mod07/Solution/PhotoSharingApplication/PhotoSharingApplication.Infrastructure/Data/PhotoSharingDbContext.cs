﻿using Microsoft.EntityFrameworkCore;
using PhotoSharingApplication.Core.Entities;

namespace PhotoSharingApplication.Infrastructure.Data;

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
    }
    public DbSet<Photo> Photos { get; set; }
}
