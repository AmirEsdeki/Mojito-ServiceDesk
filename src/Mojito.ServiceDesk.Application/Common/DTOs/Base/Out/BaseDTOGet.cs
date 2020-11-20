using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Base.Out
{
    public abstract class BaseDTOGet : IBaseDTOGet
    {
        //taking the Id long for all output classes because we have both int and long Id 
        //and for mapping it is consumed to be ok in terms of perfomance.
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
    }

    public abstract class BaseDTOGetWithGuidId : IBaseDTOGetWithGuidId
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
    }
}
