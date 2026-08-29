using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Support_Ticket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.DataContext
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Notification> Notifications { get; set; }

        public virtual DbSet<Ticket> Tickets { get; set; }

        public virtual DbSet<TicketComment> TicketComments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(e => e.Message).HasMaxLength(500);
                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasMaxLength(50);
                entity.Property(e => e.CreatedBy).HasMaxLength(50);
                entity.Property(e => e.Priority).HasMaxLength(50);
                entity.Property(e => e.Status).HasMaxLength(50);
                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Category).WithMany(p => p.Tickets)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_Tickets_Categories");
            });

            modelBuilder.Entity<TicketComment>(entity =>
            {
                entity.Property(e => e.Comment).HasMaxLength(50);
                entity.Property(e => e.CreatedAt).HasMaxLength(50);
                entity.Property(e => e.UserId).HasMaxLength(450);

                entity.HasOne(d => d.Ticket).WithMany(p => p.TicketComments)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK_TicketComments_Tickets");
            });
            base.OnModelCreating(modelBuilder);
            // Configure your entity mappings here
        }
    }
}