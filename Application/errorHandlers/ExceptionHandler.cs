using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.errorHandlers
{
    public class ExceptionHandler: Exception
    {
        public HttpStatusCode StatusCode { get; }

        public object? Errors { get; }

        public ExceptionHandler(HttpStatusCode statusCode, object? errors = null){
            StatusCode = statusCode;
            Errors = errors;
        }        
    }
}