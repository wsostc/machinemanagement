using DatabaseObject.Domain;
using DatabaseObject.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseObject.DadaAccess
{
    public class LookupDao : BaseDao
    {
        public List<KeyValueInfo> GetLookups(string tableName)
        {
            List<KeyValueInfo> infos = new List<KeyValueInfo>();

            sqlStr = string.Format("SELECT * FROM {0} ORDER BY ID", tableName);

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                using (DataTable dt = GetDataTable(command))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        infos.Add(new KeyValueInfo() { ID = SQLUtil.ConvertInt(dr["ID"]), Name = SQLUtil.TrimNull(dr["Description"]) });
                    }
                }
            }

            return infos;
        }

    }
}
