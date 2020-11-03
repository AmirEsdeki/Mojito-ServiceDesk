﻿using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Core.Constant;
using System;

namespace Mojito.ServiceDesk.Application.Common.Models
{
	public class JwtClaims : ICurrentUserService
	{
		public string UserId { get; set; }

		public int SematId { get; set; }

		public Roles Role { get; set; }

		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool IsValid { get; set; }

		public bool IsVerified { get; set; }
	}
}
