﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.In
{
    public class PutTicketLabelDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.TicketLabel>
    {

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Color { get; set; }

        public int CustomerOrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutTicketLabelDTO, Core.Entities.Ticketing.TicketLabel>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
