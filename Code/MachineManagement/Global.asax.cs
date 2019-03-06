using DatabaseObject.Domain;
using DatabaseObject.Manager;
using DatabaseObject.Util;
using MachineManagement.App_Start;
using System.Net;
using System.Web.Mvc;
using System.Web.Routing;

namespace MachineManagement
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ConnectionUtil.Init(WebConfig.SQL_CONNECTION_STRING);
            LookupManager.DoInit();
            Constants.SetFolders(Server.MapPath("~/"));

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
