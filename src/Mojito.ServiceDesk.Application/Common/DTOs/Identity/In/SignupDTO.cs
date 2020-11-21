﻿using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class SignUpDTO : IMapFrom<Core.Entities.Identity.User>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Username { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [EmailAddress(ErrorMessage = "فرمت ایمیل صحیح نمی باشد.")]
        [StringLength(255)]
        public string Email { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [Phone(ErrorMessage = "فرمت شماره تلفن صحیح نمی باشد.")]
        [StringLength(255)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [Range(0,300)]
        public int CustomerOrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<SignUpDTO, Core.Entities.Identity.User>();
        }
    }
}
