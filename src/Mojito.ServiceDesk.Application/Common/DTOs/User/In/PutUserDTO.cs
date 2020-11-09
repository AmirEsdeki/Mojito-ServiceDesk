using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.In
{
    public class PutUserDTO : IMapFrom<Core.Entities.Identity.User>
    {
        [StringLength(255)]
        public string Username { get; set; }

        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Phone]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [StringLength(255)]
        public string FirstName { get; set; }

        [StringLength(255)]
        public string LastName { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutUserDTO, Core.Entities.Identity.User>()
                .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null)); 
        }
    }
}
