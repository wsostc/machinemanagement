using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachineManagement.Controllers
{
    public class MachineController : Controller
    {
        // GET: Machine
        public ActionResult MachineList()
        {
            return View();
        }
    }
}