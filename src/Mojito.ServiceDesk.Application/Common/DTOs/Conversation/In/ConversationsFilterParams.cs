using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In
{
    public class ConversationsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        [Required]
        public string TicketId { get; set; }

        public string Title { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }
    }
}
