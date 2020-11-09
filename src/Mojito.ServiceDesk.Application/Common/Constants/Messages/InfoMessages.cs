namespace Mojito.ServiceDesk.Application.Common.Constants.Messages
{
    public static class InfoMessages
    {
        public static string UserCreated => "کاربر با موفقیت ساخته شد، کد اعتبارسنجی به ایمیل ارسال گردید.";

        public static string UserVerified => "اعتبارسنجی ایمیل با موفقیت انجام شد";

        public static string CodeHasSent => "کد اعتبارسنجی مجددا برای ایمیل شما ارسال شد.";

        public static string SuccesfullySignedIn => "احراز هویت با موفقیت انجام شد.";

        public static string RoleCreated => "نقش با موفقیت ساخته شد.";

        public static string RefreshTokenRevokedSuccessfully => "رفرش توکن به غیرفعال شد.";

        public static string UserUpdated => "کاربر با موفقیت ویرایش شد.";

        public static string UserDeleted => "کاربر با موفقیت حذف شد.";

        public static string UserCreatedByAdmin => "کاربر با موفقیت ایجاد شد";

        public static string GroupAdded { get; set; }
        public static string GroupRemoved { get; set; }
        public static string PostAdded { get; set; }
        public static string PostRemoved { get; set; }
        public static string CustomerOrganizationAdded { get; set; }
        public static string CustomerOrganizationRemoved { get; set; }
        public static string IssueUrlAdded { get; set; }
        public static string IssueUrlRemoved { get; set; }
    }
}
