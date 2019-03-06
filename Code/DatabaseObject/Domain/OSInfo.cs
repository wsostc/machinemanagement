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
    public class OSInfo
    {
        public int ID { get; set; }
        public KeyValueInfo Type { get; set; }
        public string Desctription { get; set; }

        public OSInfo()
        {
            this.Type = new KeyValueInfo();
        }

        public OSInfo(DataRow dr, string field = "") : this()
        {
            this.ID = SQLUtil.ConvertInt(dr[field + "ID"]);
            this.Type.ID = SQLUtil.ConvertInt(dr[field + "TypeID"]);
            this.Type.Name = LookupManager.GetOSTypeDesc(this.Type.ID);
            this.Desctription = SQLUtil.TrimNull(dr[field + "Desctription"]);
        }
    }
}
