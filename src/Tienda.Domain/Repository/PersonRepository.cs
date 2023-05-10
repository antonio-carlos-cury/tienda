using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Tienda.Domain.Context;
using Tienda.Domain.Entities;
using Tienda.Domain.Interfaces;
using System.Diagnostics;

namespace Tienda.Domain.Repository
{
    public class PersonRepository : CoreRepository<PersonFilter, PersonEntity>, IPersonRepository
    {
        protected PersonImagesRepository _imagesRepository;
        protected PersonVideosRepository _videosRepository;
        public PersonRepository(TiendaDBContext context) : base(context) 
        {
            _imagesRepository = new(context);
            _videosRepository = new(context);
        }


        public override async Task<bool> InsertAsync(PersonEntity entity)
        {
            DbSet.Add(entity);

            if (entity.Images is not null && entity.Images.Any())
            {
                foreach (PersonImagesEntity personImages in entity.Images)
                {
                    await _imagesRepository.InsertAsync(personImages);
                }
            }

            if (entity.Videos is not null && entity.Videos.Any())
            {
                foreach (PersonVideosEntity personVideo in entity.Videos)
                {
                    if (await ConvertVideo(personVideo))
                    {
                        await _videosRepository.InsertAsync(personVideo);
                    }
                }
            }

            
            return await SaveChangesAsync() > 0;
        }

        private async Task<bool> ConvertVideo(PersonVideosEntity personVideo)
        {
            string localPath = Path.GetTempPath();
            string fulltempName = Path.Combine(localPath, $"{personVideo.CreatedAt.Ticks}.mp4");
            string fullLastName = Path.Combine(localPath, $"{personVideo.CreatedAt.Ticks}_last.mp4");
            await File.WriteAllBytesAsync(fulltempName, personVideo.Video.Contents);

            var command = $" /c ffmpeg -i \"{fulltempName}\" -f mp4 -vcodec libx264 -preset fast -profile:v main -acodec aac \"{fullLastName}\" -hide_banner";
            Execute(localPath, command);

            personVideo.Video.Contents = File.ReadAllBytes(fullLastName);
            return true;
        }

        private bool Execute(string workdir, string command)
        {
            var cmdsi = new ProcessStartInfo("cmd.exe")
            {
                Arguments = command,
                UseShellExecute = false,
                WorkingDirectory = workdir
            };

            var cmd = Process.Start(cmdsi);
            cmd.WaitForExit();
            return true;
        }


        public override async Task<bool> UpdateAsync(PersonEntity entity)
        {
            if (entity.Images is not null && entity.Images.Any())
            {
                foreach (PersonImagesEntity personImages in entity.Images)
                {
                    await _imagesRepository.InsertAsync(personImages);
                }
            }

            if (entity.Videos is not null && entity.Videos.Any())
            {
                foreach (PersonVideosEntity personVideo in entity.Videos)
                {
                    await _videosRepository.InsertAsync(personVideo);
                }
            }

            DbSet.Update(entity);
            return await SaveChangesAsync() > 0;
        }
    }

    public class PersonImagesRepository : CoreRepository<PersonImagesFilter, PersonImagesEntity>, IPersonImagesRepository
    {
        public PersonImagesRepository(TiendaDBContext context) : base(context) { }
    }

    public class PersonVideosRepository : CoreRepository<PersonVideosFilter, PersonVideosEntity>, IPersonVideosRepository
    {
        public PersonVideosRepository(TiendaDBContext context) : base(context) { }
    }

    public class PersonMapping : IEntityTypeConfiguration<PersonEntity>
    {
        public void Configure(EntityTypeBuilder<PersonEntity> builder)
        {
            builder.HasKey(p => p.Id);
            builder.HasMany(p => p.Videos);
            builder.HasMany(p => p.Images);
            builder.ToTable("Persons");
        }
    }
}
