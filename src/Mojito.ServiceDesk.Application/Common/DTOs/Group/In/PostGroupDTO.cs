﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Group.In
{
    public class PostGroupDTO : BaseDTOPost, IMapFrom<Core.Entities.Identity.Group>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        public int GroupTypeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostGroupDTO, Core.Entities.Identity.Group>();
        }

    }
}
