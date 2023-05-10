using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Context;
using Tienda.Domain.Entities;
using Tienda.Domain.Interfaces;

namespace Tienda.Domain.Repository
{
    public class ImageRepository : CoreRepository<ImageFilter, ImageEntity>, IImageRepository
    {
        public ImageRepository(TiendaDBContext context) : base(context) { }
    }

    public class CacheMapping : IEntityTypeConfiguration<ImageEntity>
    {
        public void Configure(EntityTypeBuilder<ImageEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.ToTable("Images");
        }
    }
}
