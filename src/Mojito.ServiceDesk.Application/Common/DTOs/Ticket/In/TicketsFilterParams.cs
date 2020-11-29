using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In
{
    public class TicketsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }

        public bool OnlyTicketsOfAssignee { get; set; }

        public bool OnlyTicketsOfGroup { get; set; }

        public bool OnlyClosedByUser { get; set; }

        public bool OnlyOpenedByUser { get; set; }

        public bool OnlyIfUserHasCreated { get; set; }

        public bool IsClosed { get; set; }

        public int PriorityId { get; set; }

        public int IssueUrlId { get; set; }

        public int TicketStatusId { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string OrderByProperty { get; set; }

        public string HowToOrder { get; set; }

        #region OnlyForAdminOrObserver
        public int CustomerOrganizationId { get; set; }

        public int NomineeGroupId { get; set; }

        public string AssigneeId { get; set; }
        
        public bool? HasAssignee { get; set; }
        #endregion
    }
}
