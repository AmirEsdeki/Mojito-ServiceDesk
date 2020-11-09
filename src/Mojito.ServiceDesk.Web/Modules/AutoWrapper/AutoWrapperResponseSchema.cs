using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Modules.AutoWrapper
{
    public class AutoWrapperResponseSchema<T> where T : class
    {
        public string Version { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool? IsError { get; set; }
        public T Result { get; set; }
    }
}
