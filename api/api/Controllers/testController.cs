using api.Filter;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace api.Controllers
{
    [ErroLogFilter]
    public class testController : ApiController
    {
        [HttpGet]
        public IHttpActionResult aa()
        {
            int b = 0;
            int i = 1 / b;
            return Json(new { b});
        }
    }
}
