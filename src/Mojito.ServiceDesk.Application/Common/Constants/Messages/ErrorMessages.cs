namespace Mojito.ServiceDesk.Application.Common.Constants.Messages
{
    public static class ErrorMessages
    {
        public const string AccountHasLocked  = "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیقه قفل شده است";

        public const string EntityNotFound  = "موجودیت موردنظر یافت نشد.";

        public const string InvalidToken  = "توکن معتبر نیست.";

        public const string Unauthorized  = "شما دسترسی به این سرویس را ندارید.";

        public const string WrongCredentials  = "نام کاربری یا رمز عبور اشتباه است.";

        public const string AccountNotVerified  = "حساب کاربری شما تایید نشده، ابتدا نسبت به تایید آن اقدام فرمایید.";

        public const string TokenIsEmpty  = "وجود توکن در درخواست اجباری است.";

        public const string TokenNotFound  = "توکن یافت نشد.";

        public const string UserNameOrEmailNotAvailable  = "این نام کاربری یا ایمیل در دسترس نیست.";
    }
}
