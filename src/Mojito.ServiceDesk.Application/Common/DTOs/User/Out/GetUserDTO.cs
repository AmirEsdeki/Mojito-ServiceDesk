using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.Out
{
    public class GetUserDTO : IMapFrom<Core.Entities.Identity.User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public virtual string PhoneNumber { get; set; }

        public virtual string UserName { get; set; }

        public virtual string Email { get; set; }

        public bool IsEmployee { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }


        #region relations
        public PostDTO_Cross Post { get; set; }

        //public CustomerOrganization CustomerOrganization { get; set; }

        //public ProfileImage ProfileImage { get; set; }

        //public ICollection<Group> Groups { get; set; }

        //public ICollection<IssueUrl> IssueUrls { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.User, GetUserDTO>();
                //.ForMember(dest => dest.Post.PostId, opt => opt.MapFrom(src => src.PostId != null ? src.PostId : 0))
                //.ForMember(dest => dest.Post.PostTitle, opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : ""))
                //.ForMember(dest => dest.CustomerOrganization.CustomerOrganizationId,
                //    opt => opt.MapFrom(src => src.CustomerOrganizationId != null ? src.CustomerOrganizationId : 0))
                //.ForMember(dest => dest.CustomerOrganization.CustomerOrganizationTitle,
                //    opt => opt.MapFrom(src => src.CustomerOrganization != null ? src.CustomerOrganization.Name : ""))
                //.ForMember(dest => dest.ProfileImage.ProfileImageId,
                //    opt => opt.MapFrom(src => src.ProfileImageId != null ? src.ProfileImageId : 0))
                //.ForMember(dest => dest.ProfileImage.Image,
                //    opt => opt.MapFrom(src => src.ProfileImage != null ? src.ProfileImage.Image : null));
        }
    }

    public class CustomerOrganization
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ProfileImage
    {
        public int Id { get; set; }
        public byte[] Image { get; set; }
    }

    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class IssueUrl
    {
        public int Id { get; set; }
        public string Url { get; set; }
    }
}
