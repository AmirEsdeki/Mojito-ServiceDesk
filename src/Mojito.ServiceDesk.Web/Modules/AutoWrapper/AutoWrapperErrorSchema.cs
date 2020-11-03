using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Modules.AutoWrapper
{
    public class AutoWrapperErrorSchema
    {

        public int statusCode { get; set; }

        public bool isError { get; set; }
        public ExceptionMessage ResponseException { get; set; }

    }

    public class ExceptionMessage
    {
        public string exceptionMessage { get; set; }
    }
}
