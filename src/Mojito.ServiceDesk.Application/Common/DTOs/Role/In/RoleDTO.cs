﻿using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Role.In
{
    public class RoleDTO
    {
        [Required]
        [StringLength(255)]
        public string RoleName { get; set; }
    }
}
