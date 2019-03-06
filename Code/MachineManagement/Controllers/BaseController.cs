using DatabaseObject.Domain;
using MachineManagement.Models;
using System;
using System.IO;
using System.Web.Mvc;

namespace MachineManagement.Controllers
{
    public class BaseController : Controller
    {
        #region "session"
        public const string SESSION_USER = "LoginUser";

        public bool CheckSession()
        {
            if (Session[SESSION_USER] == null)
                return false;
            else
                return true;
        }

        public void SaveSession(UserInfo userInfo)
        {
            Session[SESSION_USER] = userInfo;
        }

        #endregion

        public string ParseBase64String(string base64Str)
        {
            if (!string.IsNullOrEmpty(base64Str) && base64Str.StartsWith("data:", StringComparison.InvariantCultureIgnoreCase))
            {
                if (base64Str.IndexOf("base64,") >= 0)
                    return base64Str.Remove(0, base64Str.IndexOf("base64,") + ("base64,").Length).Trim();
                else
                    return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }

        public JsonResult JsonResult(ResultModel module)
        {
            JsonResult jr = Json(module, JsonRequestBehavior.AllowGet);
            jr.MaxJsonLength = Int32.MaxValue;
            return jr;
        }

        public ActionResult OpenLocalFile(string fileName, string base64String)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (!string.IsNullOrEmpty(fileName))
                {
                    fileName = new FileInfo(fileName).Name;
                    base64String = ParseBase64String(base64String);

                    byte[] fileContent = Convert.FromBase64String(base64String);

                    return File(fileContent, System.Web.MimeMapping.GetMimeMapping(fileName), fileName);
                }
            }
            catch (Exception ex)
            {
                result.SetFailed(ex.Message);
            }

            return null;
        }
    }
}