﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Group.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.Out
{
    public class GetUserDTO : IMapFrom<Core.Entities.Identity.User>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool IsEmployee { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }


        #region relations
        public PostDTO_Cross Post { get; set; }

        public GetCustomerOrganizationDTO_Cross CustomerOrganization { get; set; }

        public ProfileImageDTO_Cross ProfileImage { get; set; }

        public ICollection<GroupDTO_Cross> Groups { get; set; }

        public ICollection<IssueUrlDTO_Cross> IssueUrls { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.User, GetUserDTO>()
                .ForMember(dto => dto.Groups, opt => opt.MapFrom(x => x.Groups.Select(y => y.Group).ToList()))
                .ForMember(dto => dto.IssueUrls, opt => opt.MapFrom(x => x.IssueUrls.Select(y => y.IssueUrl).ToList()));
        }
    }
}
