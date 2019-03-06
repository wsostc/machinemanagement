using DatabaseObject.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DatabaseObject.Domain
{
    public class UserInfo
    {
        public int ID { get; set; }
        public bool IsAdmin { get; set; }
        public string LoginID { get; set; }         //email format
        public string LoginPwd { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedDate { get; set; }

        //temp
        public bool IsUpdatePassword { get; set; }
        public string IsAdminDesc { get { return this.IsAdmin ? "administrator" : ""; } }

        public UserInfo() { }

        public UserInfo(DataRow dr) : this()
        {
            this.ID = SQLUtil.ConvertInt(dr["ID"]);
            this.IsAdmin = SQLUtil.ConvertBoolean(dr["IsAdmin"]);
            this.LoginID = SQLUtil.TrimNull(dr["LoginID"]);
            this.LoginPwd = SQLUtil.TrimNull(dr["LoginPwd"]);
            this.Name = SQLUtil.TrimNull(dr["Name"]);
            this.Email = SQLUtil.TrimNull(dr["Email"]);
            this.CreatedDate = SQLUtil.ConvertDateTime(dr["CreatedDate"]);
        }
    }
}