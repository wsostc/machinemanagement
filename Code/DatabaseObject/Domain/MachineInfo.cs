using DatabaseObject.Manager;
using DatabaseObject.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseObject.Domain
{
    public class MachineInfo
    {
        public int ID { get; set; }
        public OSInfo OS { get; set; }
        public ServerInfo Server { get; set; }
        public string Name { get; set; }
        public string IP { get; set; }
        public KeyValueInfo Status { get; set; }
        public UserInfo User { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public string Comments { get; set; }

        public MachineInfo()
        {
            this.OS = new OSInfo();
            this.Server = new ServerInfo();
            this.Status = new KeyValueInfo();
            this.User = new UserInfo();
        }

        public MachineInfo(DataRow dr) : this()
        {
            this.ID = SQLUtil.ConvertInt(dr["ID"]);

            if (dr.Table.Columns.Contains("OSDesctription"))
                this.OS = new OSInfo(dr, "OS");
            else
                this.OS.ID = SQLUtil.ConvertInt(dr["OSID"]);

            if (dr.Table.Columns.Contains("ServerDesctription"))
                this.Server = new ServerInfo(dr, "Server");
            else
                this.Server.ID = SQLUtil.ConvertInt(dr["ServerID"]);

            this.Name = SQLUtil.TrimNull(dr["Name"]);
            this.IP = SQLUtil.TrimNull(dr["IP"]);
            this.Status.ID = SQLUtil.ConvertInt(dr["StatusID"]);
            this.Status.Name = LookupManager.GetStatuDesc(this.Status.ID);

            this.User.ID = SQLUtil.ConvertInt(dr["UserID"]);
            if (dr.Table.Columns.Contains("UserName"))
                this.User.Name = SQLUtil.TrimNull(dr["UserName"]);

            this.LastUpdateTime = SQLUtil.ConvertDateTime(dr["LastUpdateTime"]);
            this.Comments = SQLUtil.TrimNull(dr["Comments"]);
        }

    }
}
