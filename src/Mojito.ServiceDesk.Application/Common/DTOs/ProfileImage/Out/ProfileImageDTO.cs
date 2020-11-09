﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out
{
    public class ProfileImageDTO : BaseDTOOut, IMapFrom<Core.Entities.Identity.ProfileImage>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.ProfileImage, ProfileImageDTO>();
        }
    }
}