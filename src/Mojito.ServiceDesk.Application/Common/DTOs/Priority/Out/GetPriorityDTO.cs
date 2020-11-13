using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Priority.Out
{
    public class GetPriorityDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.Priority>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Priority, GetPriorityDTO>();
        }
    }

    public class PriorityDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.Priority>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Priority, PriorityDTO_Cross>();
        }
    }
}
