using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Common
{
    public static class ControllerHelper
    {
        public static JsonResult CreateResult(this ControllerBase action, object data,int res=1, string msg = "")
        {
            return new JsonResult(new { res, msg, data });
        }
    }
}
