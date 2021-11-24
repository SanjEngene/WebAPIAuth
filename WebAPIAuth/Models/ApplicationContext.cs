using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebAPIAuth.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<MangaArtist> MangaArtists { get; set; }
        public DbSet<MangaCreation> MangaCreations { get; set; }
        public DbSet<User> Users { get; set; }
        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MangaArtist>().HasKey(i => i.Id);
            modelBuilder.Entity<MangaCreation>().HasKey(i => i.Id);
            modelBuilder.ApplyConfiguration(new MangaCreationConfiguration());
        }
    }
    public class MangaCreationConfiguration : IEntityTypeConfiguration<MangaCreation>
    {
        public void Configure(EntityTypeBuilder<MangaCreation> builder)
        {
            builder.HasOne(c => c.MangaArtist)
                .WithMany(a => a.Mangas)
                .HasForeignKey(c => c.MangaArtistId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
