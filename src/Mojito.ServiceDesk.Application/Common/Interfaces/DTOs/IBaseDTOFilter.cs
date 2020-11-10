namespace Mojito.ServiceDesk.Application.Common.Interfaces.DTOs
{
    public interface IBaseDTOFilter
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
