using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FEGProject.API.Infrastructure.Filters
{
    public class HandleExceptionFilter : ActionFilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {

            //requesti ressponse middleware
            //Log.Logger.(context);

            var exception = context.Exception;
            context.HttpContext.Response.StatusCode = 500;
            context.Result = new ObjectResult(new ErrorResponse()
            {
                ErrorMessage = new StringBuilder()
                        .Append(exception.Message).AppendLine()
                        .Append(exception.InnerException).AppendLine()
                        .Append(exception.StackTrace).ToString()
            });
        }
    }
}
