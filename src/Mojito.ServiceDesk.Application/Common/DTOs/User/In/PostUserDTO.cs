using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.In
{
    public class PostUserDTO : BaseDTOPost, IMapFrom<Core.Entities.Identity.User>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
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

        public bool CreateAsAdmin { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostUserDTO, Core.Entities.Identity.User>();
        }
    }
}
