using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.In;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProfileImageService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.ProfileImageService
{
    public class ProfileImageService : IProfileImageService
    {
        #region ctor
        private readonly ApplicationDBContext db;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public ProfileImageService(ApplicationDBContext db,
            IImageService imageService,
            IMapper mapper)
        {
            this.db = db;
            this.imageService = imageService;
            this.mapper = mapper;
        }
        #endregion

        public async Task<GetProfileImageDTO> CreateAsync(PostProfileImageDTO entity)
        {
            entity.Image = imageService.ResizeImage(entity.Image, 1024);

            var mappedEntity = mapper.Map<ProfileImage>(entity);

            mappedEntity.ImageThumbnail = imageService.ResizeImage(entity.Image, 128);

            var previousImage = await db.ProfileImages.FirstOrDefaultAsync(f => f.UserId == entity.UserId);

            if (previousImage != null)
                db.ProfileImages.Remove(previousImage);

            var addedEntity = db.ProfileImages.Add(mappedEntity);

            await db.SaveChangesAsync();

            var dto = mapper.Map<GetProfileImageDTO>(addedEntity.Entity);

            return dto;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await db.ProfileImages.FirstOrDefaultAsync(x => x.Id == id);

            if (entity != null)
            {
                db.ProfileImages.Remove(entity);

                await db.SaveChangesAsync();
            }
        }

        public async Task<GetProfileImageDTO> GetAsync(int id)
        {
            try
            {
                var entity = await db.ProfileImages.FirstOrDefaultAsync(f => f.Id == id);

                if (entity == null)
                    throw new EntityNotFoundException();

                var dto = mapper.Map<GetProfileImageDTO>(entity);

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #region PrivateMethods
        #endregion
    }
}
