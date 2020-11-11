using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Product.In
{
    public class PutProductDTO : BaseDTOPut, IMapFrom<Core.Entities.Proprietary.Product>
    {
        [StringLength(255)]
        public string Name { get; set; }

        public int? CustomerId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutProductDTO, Core.Entities.Proprietary.Product>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        }
    }
}
