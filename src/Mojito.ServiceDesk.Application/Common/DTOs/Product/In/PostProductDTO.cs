using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Product.In
{
    public class PostProductDTO : BaseDTOPost, IMapFrom<Core.Entities.Proprietary.Product>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        public int ProductId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostProductDTO, Core.Entities.Proprietary.Product>();
        }

    }
}
