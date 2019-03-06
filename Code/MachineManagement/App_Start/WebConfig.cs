using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MachineManagement.App_Start
{
    public class WebConfig
    {
        public static readonly string SQL_CONNECTION_STRING = ConfigurationManager.AppSettings["SqlConnectionString"].ToString().Trim();
    }
}