using DatabaseObject.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseObject.DadaAccess
{
    public class BaseDao
    {
        protected string sqlStr;

        protected string AppendLimitClause(string sqlStr, int curRowNum, int pageSize)
        {
            if (pageSize > 0)
                sqlStr += string.Format(" OFFSET {0} ROWS FETCH NEXT {1} ROWS ONLY ", curRowNum, pageSize);

            return sqlStr;
        }

        protected DataTable GetDataTable(SqlCommand command, string tableName = "")
        {
            DataTable dt = null;
            if (string.IsNullOrEmpty(tableName))
                dt = new DataTable();
            else
                dt = new DataTable(tableName);

            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                adapter.Fill(dt);
            }

            return dt;
        }

        protected DataRow GetDataRow(SqlCommand command)
        {
            using (DataTable dt = new DataTable())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);
                    if (dt.Rows.Count > 0)
                        return dt.Rows[0];
                    else
                        return null;
                }
            }
        }

        protected List<int> GetList(SqlCommand command)
        {
            List<int> list = new List<int>();
            using (DataTable dt = new DataTable())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        list.Add(SQLUtil.ConvertInt(dr[0]));
                    }
                }
                return list;
            }
        }

        protected Dictionary<int, int> GetDictionary(SqlCommand command)
        {
            Dictionary<int, int> dicCount = new Dictionary<int, int>();
            using (DataTable dt = new DataTable())
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dt);

                    foreach (DataRow dr in dt.Rows)
                    {
                        dicCount.Add(SQLUtil.ConvertInt(dr[0]), SQLUtil.ConvertInt(dr[1]));
                    }
                }
                return dicCount;
            }
        }
    }
}