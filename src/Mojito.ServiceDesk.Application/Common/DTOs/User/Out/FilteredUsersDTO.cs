using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.Out
{
    public class FilteredUsersDTO : IMapFrom<Core.Entities.Identity.User>
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.User, FilteredUsersDTO>();
        }
    }
}
