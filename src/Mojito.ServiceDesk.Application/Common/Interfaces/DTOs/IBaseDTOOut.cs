﻿using System;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.DTOs
{
    public interface IBaseDTOOut
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }


        public DateTime? LastModified { get; set; }


        #region relations
        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion
    }
}
