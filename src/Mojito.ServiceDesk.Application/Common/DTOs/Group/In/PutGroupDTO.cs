using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Group.In
{
    public class PutGroupDTO : BaseDTOPut, IMapFrom<Core.Entities.Identity.Group>
    {
        [StringLength(255)]
        public string Name { get; set; }

        public int? GroupTypeId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutGroupDTO, Core.Entities.Identity.Group>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
