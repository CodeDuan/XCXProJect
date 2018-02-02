using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace api.Models
{
    public class Result
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}