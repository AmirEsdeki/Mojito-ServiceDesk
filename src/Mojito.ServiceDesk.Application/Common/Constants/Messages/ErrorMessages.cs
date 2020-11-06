namespace Mojito.ServiceDesk.Application.Common.Constants.Messages
{
    public static class ErrorMessages
    {
        public static string AccountHasLocked => "اکانت شما به دلیل پنج بار ورود ناموفق به مدت پنج دقیقه قفل شده است";
        public static string EntityNotFound => "موجودیت موردنظر یافت نشد.";
        public static string InvalidToken => "توکن معتبر نیست.";
        public static string Unauthorized => "شما دسترسی به این سرویس را ندارید.";
        public static string WrongCredentials => "نام کاربری یا رمز عبور اشتباه است.";
        public static string AccountNotVerified => "حساب کاربری شما تایید نشده، ابتدا نسبت به تایید آن اقدام فرمایید.";
    }
}
