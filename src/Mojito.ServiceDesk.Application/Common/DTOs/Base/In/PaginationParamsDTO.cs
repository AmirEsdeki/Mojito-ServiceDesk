namespace Mojito.ServiceDesk.Application.Common.DTOs.Base.In
{
    public abstract class PaginationParamsDTO
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
