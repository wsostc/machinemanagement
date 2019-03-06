using DatabaseObject.Domain;
using DatabaseObject.Manager;
using MachineManagement.App_Start;
using MachineManagement.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MachineManagement.Controllers
{
    public class UserController : BaseController
    {
        private UserManager userManager = new UserManager();

        public ActionResult UserList()
        {
            return View();
        }

        public JsonResult QueryUserList(string filterField, string filterText, int currentPage)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
                int totalRecord = this.userManager.QueryUsersCount(filterField, filterText);
                int pageSize = ConstDefinition.PAGE_SIZE;
                result.SetTotalPages(totalRecord, pageSize);

                List<UserInfo> infos = this.userManager.QueryUsers(filterField, filterText, result.GetCurRowNum(currentPage, pageSize), pageSize);
                result.JsonObject = JsonConvert.SerializeObject(infos, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }

        public JsonResult GetUser(int id)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
                UserInfo info = this.userManager.GetUser(id);
                result.JsonObject = JsonConvert.SerializeObject(info, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }

        [HttpPost]
        public JsonResult SaveUser(UserInfo info)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
                if (info.ID == 0)
                {
                    UserInfo newInfo = this.userManager.AddUser(info);
                    result.JsonObject = JsonConvert.SerializeObject(newInfo, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
                }
                else
                {
                    this.userManager.UpdateUser(info);
                }
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }

        [HttpPost]
        public JsonResult UpdatePassword(int id, string loginPwd)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
               this.userManager.UpdatePassword(id, loginPwd);
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }

        [HttpPost]
        public JsonResult DeleteUser(int id)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
                bool isAdmin = ((UserInfo)Session[SESSION_USER]).IsAdmin;
                if(isAdmin)
                    this.userManager.DeleteUser(id);
                else
                    result.SetFailed("You do not have this permission, please notify the administrator");
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }

        public JsonResult CheckUserLoginID(int id, string loginID)
        {
            if (CheckSession() == false)
            {
                return Json(new SessionTimeoutModel(), JsonRequestBehavior.AllowGet);
            }

            ResultModel result = new ResultModel();
            try
            {
                bool existed = this.userManager.CheckUserLoginID(id, loginID);

                if (existed == false)
                    result.JsonObject = "0";
                else
                    result.JsonObject = "1";
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }
            return JsonResult(result);
        }
    }
}