using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace manage.Controllers
{
    public class CommonController : Controller
    {
        public IActionResult uploadimg()
        {
            var files = Request.Form.Files;
            if (!Directory.Exists("~/uploadimg"))
            {
                Directory.CreateDirectory("~/uploadimg");
            }
            foreach (var item in files)
            {
                string imgfilename = "~/uploadimg/" + new Guid().ToString() + ".jpg";
                using (FileStream fs = new FileStream(imgfilename,FileMode.Create))
                {
                    item.CopyTo(fs);
                    fs.Flush();
                }
            }
            return View();
        }
    }
}