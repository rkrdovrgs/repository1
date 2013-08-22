using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BackloadDemoController : Controller
    {
        //
        // GET: /BackupDemo/
        [HttpPost]
        public ActionResult UploadHandler()
        {


            var a = ControllerContext;
            return View();
        }
    }
}
