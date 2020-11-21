using Microsoft.AspNetCore.Identity;
using System.Text;

namespace Mojito.ServiceDesk.Infrastructure.Modules
{
    public class PersianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError DuplicateEmail(string email)
            => new IdentityError()
            {
                Code = nameof(DuplicateEmail),
                Description = new StringBuilder()
                .Append("ایمیل")
                .Append(" ")
                .Append(email)
                .Append(" ")
                .Append("توسط شخص دیگری انتخاب شده است")
                .Append(".")
                .ToString()
            };

        public override IdentityError DuplicateUserName(string userName)
            => new IdentityError()
            {
                Code = nameof(DuplicateUserName),
                Description = new StringBuilder()
                .Append("نام کاربری")
                .Append(" ")
                .Append(userName)
                .Append(" ")
                .Append("توسط شخص دیگری انتخاب شده است")
                .Append(".")
                .ToString()
            };

        public override IdentityError InvalidEmail(string email)
            => new IdentityError()
            {
                Code = nameof(InvalidEmail),
                Description = new StringBuilder()
                .Append("ایمیل")
                .Append(" ")
                .Append(email)
                .Append(" ")
                .Append("یک ایمیل معتبر نیست")
                .Append(".")
                .ToString()
            };

        public override IdentityError DuplicateRoleName(string role)
            => new IdentityError()
            {
                Code = nameof(DuplicateRoleName),
                Description = new StringBuilder()
                .Append("مقام")
                .Append(" ")
                .Append(role)
                .Append(" ")
                .Append("قبلا ثبت شده است")
                .Append(".")
                .ToString()
            };

        public override IdentityError InvalidRoleName(string role)
            => new IdentityError()
            {
                Code = nameof(InvalidRoleName),
                Description = new StringBuilder()
                .Append("نام")
                .Append(" ")
                .Append(role)
                .Append(" ")
                .Append("معتبر نیست")
                .Append(".")
                .ToString()
            };

        public override IdentityError PasswordRequiresDigit()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresDigit),
                Description = $"رمز عبور باید حداقل دارای یک عدد باشد"
            };

        public override IdentityError PasswordRequiresLower()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresLower),
                Description = $"رمز عبور باید حداقل دارای یک کاراکتر انگلیسی کوچک باشد ('a'-'z')"
            };

        public override IdentityError PasswordRequiresUpper()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresUpper),
                Description = $"رمز عبور باید حداقل دارای یک کاراکتر انگلیسی بزرگ باشد ('A'-'Z')"
            };

        public override IdentityError PasswordRequiresNonAlphanumeric()
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = $"رمز عبور باید حداقل دارای یک کاراکتر ویژه باشد مثل '@#%^&'"
            };

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
            => new IdentityError()
            {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = new StringBuilder()
                .Append("رمز عبور باید حداقل دارای")
                .Append(" ")
                .Append(uniqueChars)
                .Append(" ")
                .Append("کاراکتر منحصر به فرد باشد")
                .Append(".")
                .ToString()
            };

        public override IdentityError PasswordTooShort(int length)
            => new IdentityError()
            {
                Code = nameof(PasswordTooShort),
                Description = new StringBuilder()
                .Append("رمز عبور نباید کمتر از")
                .Append(" ")
                .Append(length)
                .Append(" ")
                .Append("کاراکتر باشد")
                .Append(".")
                .ToString()
            };

        public override IdentityError InvalidUserName(string userName)
            => new IdentityError()
            {
                Code = nameof(InvalidUserName),
                Description = new StringBuilder()
                .Append("نام کاربری")
                .Append(" ")
                .Append(userName)
                .Append(" ")
                .Append("معتبر نیست")
                .Append(".")
                .ToString()
            };

        public override IdentityError UserNotInRole(string role)
            => new IdentityError()
            {
                Code = nameof(UserNotInRole),
                Description = new StringBuilder()
                .Append("کاربر مورد نظر در مقام")
                .Append(" ")
                .Append(role)
                .Append(" ")
                .Append("نیست")
                .Append(".")
                .ToString()
            };

        public override IdentityError UserAlreadyInRole(string role)
            => new IdentityError()
            {
                Code = nameof(UserAlreadyInRole),
                Description = new StringBuilder()
                .Append("کاربر مورد نظر همین اکنون در مقام")
                .Append(" ")
                .Append(role)
                .Append(" ")
                .Append("است")
                .Append(".")
                .ToString()
            };

        public override IdentityError DefaultError()
            => new IdentityError()
            {
                Code = nameof(DefaultError),
                Description = "خطای پیشبینی نشده رخ داد"
            };

        public override IdentityError ConcurrencyFailure()
            => new IdentityError()
            {
                Code = nameof(ConcurrencyFailure),
                Description = "خطای همزمانی رخ داد"
            };

        public override IdentityError InvalidToken()
            => new IdentityError()
            {
                Code = nameof(InvalidToken),
                Description = "توکن معتبر نیست"
            };

        public override IdentityError RecoveryCodeRedemptionFailed()
            => new IdentityError()
            {
                Code = nameof(RecoveryCodeRedemptionFailed),
                Description = "کد بازیابی معتبر نیست"
            };

        public override IdentityError UserLockoutNotEnabled()
            => new IdentityError()
            {
                Code = nameof(UserLockoutNotEnabled),
                Description = "قابلیت قفل اکانت کاربر فعال نیست"
            };

        public override IdentityError UserAlreadyHasPassword()
            => new IdentityError()
            {
                Code = nameof(UserAlreadyHasPassword),
                Description = "کاربر از قبل رمزعبور دارد"
            };

        public override IdentityError PasswordMismatch()
            => new IdentityError()
            {
                Code = nameof(PasswordMismatch),
                Description = "عدم تطابق رمزعبور"
            };

        public override IdentityError LoginAlreadyAssociated()
            => new IdentityError()
            {
                Code = nameof(LoginAlreadyAssociated),
                Description = "از قبل اکانت خارجی به حساب این کاربر متصل اصت"
            };
    }
}
