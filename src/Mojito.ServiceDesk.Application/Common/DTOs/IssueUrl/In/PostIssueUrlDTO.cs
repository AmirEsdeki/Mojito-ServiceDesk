﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In
{
    public class PostIssueUrlDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.IssueUrl>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Url { get; set; }

        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        public int ProductId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostIssueUrlDTO, Core.Entities.Ticketing.IssueUrl>();
        }

    }
}
