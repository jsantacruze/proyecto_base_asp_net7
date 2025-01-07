using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace business_layer.ExceptionsManager
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode { get; }
        public object Errors { get; }

        public CustomException(HttpStatusCode code, object errors = null)
        {
            StatusCode = code;
            Errors = errors;
        }

    }
}
