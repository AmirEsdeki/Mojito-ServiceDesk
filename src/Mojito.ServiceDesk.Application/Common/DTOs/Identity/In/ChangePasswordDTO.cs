using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string UserId { get; set; }

        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Code { get; set; }

        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Password { get; set; }

        [Required(ErrorMessage = "تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [Compare(nameof(Password), ErrorMessage = "رمز عبور و تکرار آن یکسان نیست.")]
        public string ConfirmPassword { get; set; }
    }
}
