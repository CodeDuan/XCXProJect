using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace manage.Controllers
{
    public class CommonController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public CommonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        public IActionResult uploadimg()
        {
            var files = Request.Form.Files;
            var basepath = _hostingEnvironment.WebRootPath;
            var path = "/uploadimg/" + DateTime.Now.ToString("yyyyMMdd") + "/";
            if (!Directory.Exists(basepath + path))
            {
                Directory.CreateDirectory(basepath + path);
            }
            string imgfilename = basepath + path + files[0].FileName;

            using (FileStream fs = new FileStream(imgfilename, FileMode.Create))
            {
                files[0].CopyTo(fs);
                fs.Flush();
            }
            return Json(new { Code = 100, Message = path + files[0].FileName });
        }
    }
}