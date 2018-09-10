using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{

    public class MessageController : ControllerBase
    {
        [Route("api/message/addusermessage")]
        public IActionResult AddUserMessage()
        {
            return null;
        }
    }
}