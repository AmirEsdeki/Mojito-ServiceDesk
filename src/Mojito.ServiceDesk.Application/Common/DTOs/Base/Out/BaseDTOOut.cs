using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Base.Out
{
    public class BaseDTOOut : IBaseDTOOut
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
    }
}
