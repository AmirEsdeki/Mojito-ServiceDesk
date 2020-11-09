namespace Mojito.ServiceDesk.Application.Common.DTOs.Base.Out
{
    //In Cross Dtos that are inside in parent classes we dont need All properties existing in BaseDTOOut
    public abstract class BaseDTOOut_Cross
    {
        public long Id { get; set; }
    }
}
