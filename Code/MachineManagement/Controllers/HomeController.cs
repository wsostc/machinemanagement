using DatabaseObject.Domain;
using DatabaseObject.Manager;
using DatabaseObject.Util;
using MachineManagement.App_Start;
using MachineManagement.Models;
using System;
using System.Web.Mvc;

namespace MachineManagement.Controllers
{
    public class HomeController : BaseController
    {
        private UserManager userManager = new UserManager();

        public ActionResult Login()
        {
            Session.Clear();

            return View();
        }

        public ActionResult Menu()
        {
            if (CheckSession() == false)
            {
                Response.Redirect(Url.Action(ConstDefinition.HOME_ACTION, ConstDefinition.HOME_CONTROLLER), true);
                return null;
            }

            return View();
        }

        [HttpPost]
        public JsonResult Login(string loginID, string loginPwd)
        {
            ResultModel result = new ResultModel();

            try
            {
                Session.Clear();

                UserInfo existingInfo = this.userManager.GetUser(loginID);

                if (existingInfo == null)
                    result.SetFailed("The login account has not registered or has deleted");
                else if (existingInfo.LoginPwd != SQLUtil.TrimNull(loginPwd))
                    result.SetFailed("The password is incorrect");
                else
                {
                    existingInfo.LoginPwd = null;
                    SaveSession(existingInfo);
                }
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }

            return JsonResult(result);
        }
        
    }
}