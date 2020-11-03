namespace Mojito.ServiceDesk.Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
		public int UserId { get; set; }

		public int SematId { get; set; }

		public string UserName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool IsValid { get; set; }

		public bool IsVerified { get; set; }
	}
}
