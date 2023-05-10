using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Context;
using Tienda.Domain.Entities;
using Tienda.Domain.Interfaces;

namespace Tienda.Domain.Repository
{
    public class VideoRepository : CoreRepository<VideoFilter, VideoEntity>, IVideoRepository
    {
        public VideoRepository(TiendaDBContext context) : base(context) { }
    }


    public class VideoEntityMapping : IEntityTypeConfiguration<VideoEntity>
    {
        public void Configure(EntityTypeBuilder<VideoEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.VideoImages);
            builder.ToTable("Videos");
        }
    }
}
