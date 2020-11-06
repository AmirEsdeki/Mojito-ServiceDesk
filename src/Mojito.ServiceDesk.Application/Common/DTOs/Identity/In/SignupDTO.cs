using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using Mojito.ServiceDesk.Application.Common.Mappings;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class SignUpDTO : IMapFrom<User>
    {
        [Required]
        [StringLength(255)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(255)]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignUpDTO, User>();
        }
    }
}
