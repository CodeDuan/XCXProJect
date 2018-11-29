using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Filter
{
    public class ErrFilter: IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Object result = new { Code = 500, Message = filterContext.Exception.Message, StackTrace=filterContext.Exception.StackTrace };

            filterContext.Result = new ObjectResult(result);
        }
    }
}
