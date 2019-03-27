using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BoardGames.CoreWebApi.EF
{
    public partial class RestWorkshopContext : DbContext
    {
        public RestWorkshopContext()
        {
        }

        public RestWorkshopContext(DbContextOptions<RestWorkshopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BoardGame> BoardGames { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=SKOWA;Initial Catalog=RestWorkshop;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BoardGame>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasMaxLength(50);

                entity.Property(e => e.MaxPlayersNumber).HasColumnName("maxPlayersNumber");

                entity.Property(e => e.MinPlayersNumber).HasColumnName("minPlayersNumber");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(50);
            });
        }
    }
}
