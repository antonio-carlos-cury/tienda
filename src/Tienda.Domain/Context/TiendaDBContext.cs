using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Entities;

namespace Tienda.Domain.Context
{
    public class TiendaDBContext : DbContext
    {
        public TiendaDBContext() { }
        public TiendaDBContext(DbContextOptions<TiendaDBContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }
       
        public DbSet<PersonEntity> Persons { get; set; }
        public DbSet<PersonVideosEntity> PersonVideos { get; set; }
        public DbSet<PersonImagesEntity> PersonImages { get; set; }
        public DbSet<VideoEntity> Videos { get; set; }
        public DbSet<ImageEntity> Images { get; set; }
        public DbSet<TagsEntity> Tags { get; set; }
        public DbSet<VideoImagesEntity> VideoImages { get; set; }
        public DbSet<PersonTags> PersonTags { get; set; }
        

        /// <summary>
        /// Sobrescrita da persistência para que a data de criação da entidade seja imutavel
        /// </summary>
        /// <param name="cancellationToken">Token que nunca é usado</param>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("CreatedAt") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreatedAt").CurrentValue = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedAt").IsModified = false;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
