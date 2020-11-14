using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In
{
    public class ConversationsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
