using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Product.In
{
    public class PostProductDTO : BaseDTOPost, IMapFrom<Core.Entities.Proprietary.Product>
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public int ProductId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostProductDTO, Core.Entities.Proprietary.Product>();
        }

    }
}
