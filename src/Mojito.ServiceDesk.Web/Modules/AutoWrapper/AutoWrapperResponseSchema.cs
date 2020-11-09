namespace Mojito.ServiceDesk.Web.Modules.AutoWrapper
{
    public class AutoWrapperResponseSchema<T> where T : class
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool? IsError { get; set; }
        public T Result { get; set; }
    }
}
