using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.In
{
    public class PostProfileImageDTO : IMapFrom<Core.Entities.Identity.ProfileImage>
    {
        public byte[] Image { get; set; }

        [StringLength(255)]
        public string UserId { get; set; }

        [StringLength(2000)]
        public string ContentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostProfileImageDTO ,Core.Entities.Identity.ProfileImage > ();
        }
    }
}
