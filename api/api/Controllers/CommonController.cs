using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    public class CommonController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public CommonController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        /// <summary>
        /// 上传图片(单个)
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        [Route("api/common/uploadimg")]
        public IActionResult uploadimg()
        {
            var path = string.Empty;
            var files = Request.Form.Files.Where(c=>c.Name=="img");
            if (files == null || files.Count() <= 0)
            {
                return new JsonResult(new { code = 500, msg ="请选择上传的文件"});
            }
            var allowType = new string[] { "image/jpeg","image/jpg", "image/png", "image/gif" };
            if (files.Any(c => allowType.Contains(c.ContentType)))
            {
                if (files.Sum(c => c.Length) <= 1024 * 1024 * 4)
                {
                    var tmppath = string.Empty;
                    var filename = string.Empty;
                    foreach (var item in files)
                    {
                        filename = item.FileName;
                        var ExtendName = item.FileName.Split('.')[item.FileName.Split('.').Length - 1];
                        filename = $"{Guid.NewGuid()}.{ExtendName}";
                        tmppath = Path.Combine("Upload",DateTime.Now.ToString("yyyyMMdd"));
                        path = Path.Combine(_hostingEnvironment.ContentRootPath,"wwwroot" ,tmppath);
                        if(!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }
                        var filepath = Path.Combine(path, filename);
                        using (var stream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                        {
                            item.CopyTo(stream);
                        }
                    }
                    
                    return new JsonResult(new {code=200, msg =Path.Combine(tmppath,filename).Replace("\\","/") });
                }
                else
                {
                    return new JsonResult(new { code = 500, msg = "文件大小不能超过4M" });
                }
            }
            else
            {
                return new JsonResult(new { code = 500, msg = "文件格式错误" });
            }
        }
    }

}