using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out
{
    public class GetProfileImageDTO : BaseDTOOut_Cross, IMapFrom<Core.Entities.Identity.ProfileImage>
    {
        public byte[] Image { get; set; }

        public byte[] ImageThumbnail { get; set; }

        public string ContentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.ProfileImage, GetProfileImageDTO>();
        }
    }

    public class ProfileImageDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Identity.ProfileImage>
    {
        public byte[] ImageThumbnail { get; set; }

        public string ContentType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.ProfileImage, ProfileImageDTO_Cross>();
        }
    }
}
