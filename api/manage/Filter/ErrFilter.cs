using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manage.Filter
{
    public class ErrFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            Object result = new {Code=500,Message= filterContext.Exception.Message};
            
            filterContext.Result = new ObjectResult(result);
        }
    }
}
