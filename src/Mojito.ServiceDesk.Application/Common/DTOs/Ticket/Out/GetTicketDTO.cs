﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Group.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Priority.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.User.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out
{
    public class GetTicketDTO : BaseDTOGetWithGuidId, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        public string Identifier { get; set; }

        public string Title { get; set; }

        public bool IsClosed { get; set; }

        public int ConversationCount { get; set; }

        public int AttachmentCount { get; set; }

        public string LastMessage { get; set; }

        public UserDTO_Cross OpenedBy { get; set; }

        public UserDTO_Cross Assignee { get; set; }

        public UserDTO_Cross ClosedBy { get; set; }

        public GroupDTO_Cross NomineeGroup { get; set; }

        public IssueUrlDTO_Cross IssueUrl { get; set; }

        public TicketStatusDTO_Cross TicketStatus { get; set; }

        public TicketIssueDTO_Cross TicketIssue { get; set; }

        public PriorityDTO_Cross Priority { get; set; }

        public ICollection<TicketLabelDTO_Cross> TicketLabels { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Ticket, GetTicketDTO>()
                .ForMember(dto => dto.TicketLabels, opt => opt.MapFrom(x => x.TicketLabels.Select(y => y.TicketLabel).ToList()))
                .ForMember(dto => dto.LastMessage, opt => opt.MapFrom(x => x.Conversations.OrderByDescending(p => p.Created).FirstOrDefault().Message))
                .ForMember(dto => dto.ConversationCount, opt => opt.MapFrom(x => x.Conversations.Count()))
                .ForMember(dto => dto.AttachmentCount, opt => opt.MapFrom(x => x.Conversations.Aggregate(
                    0,
                    (convCount, item) => convCount + item.Attachment.Count())));
        }
    }

    public class TicketDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Ticket, TicketDTO_Cross>();
        }
    }
}
