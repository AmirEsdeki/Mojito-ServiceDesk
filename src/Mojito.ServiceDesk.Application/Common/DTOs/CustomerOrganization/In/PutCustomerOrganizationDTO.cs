﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.In
{
    public class PutCustomerOrganizationDTO : BaseDTOPut, IMapFrom<Core.Entities.Identity.CustomerOrganization>
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostCustomerOrganizationDTO, Core.Entities.Identity.CustomerOrganization>();
        }
    }
}
