using DatabaseObject.Domain;
using DatabaseObject.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseObject.DadaAccess
{
    public class UserDao : BaseDao
    {
        #region "tblUser"
        public UserInfo GetUser(string loginID)
        {
            sqlStr = "SELECT * FROM tblUser WHERE upper(LoginID) = @LoginID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = loginID.ToUpper();

                DataRow dr = GetDataRow(command);
                if (dr != null)
                    return new UserInfo(dr);
                else
                    return null;
            }
        }

        public UserInfo GetUser(int id)
        {
            sqlStr = "SELECT * FROM tblUser WHERE ID = @ID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                DataRow dr = GetDataRow(command);
                if (dr != null)
                    return new UserInfo(dr);
                else
                    return null;
            }
        }

        public int QueryUsersCount(string filterField, string filterText)
        {
            sqlStr = "SELECT COUNT(ID) FROM tblUser WHERE 1 = 1 ";
            if (!String.IsNullOrEmpty(filterText))
                sqlStr += string.Format(" AND upper({0}) LIKE @FilterField", filterField);

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                if (!String.IsNullOrEmpty(filterText))
                    command.Parameters.Add("@FilterField", SqlDbType.NVarChar).Value = "%" + filterText.ToUpper() + "%";

                return SQLUtil.ConvertInt(command.ExecuteScalar());
            }
        }

        public List<UserInfo> QueryUsers(string filterField, string filterText, int curRowNum = 0, int pageSize = 0)
        {
            List<UserInfo> infos = new List<UserInfo>();

            sqlStr = "SELECT * FROM tblUser WHERE 1 = 1 ";
            if (!String.IsNullOrEmpty(filterText))
                sqlStr += string.Format(" AND upper({0}) LIKE @FilterField", filterField);
            sqlStr += " ORDER BY IsAdmin DESC, Name ";

            sqlStr = AppendLimitClause(sqlStr, curRowNum, pageSize);

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                if (!String.IsNullOrEmpty(filterText))
                    command.Parameters.Add("@FilterField", SqlDbType.NVarChar).Value = "%" + filterText.ToUpper() + "%";

                using (DataTable dt = GetDataTable(command))
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        infos.Add(new UserInfo(dr));
                    }
                }
            }

            return infos;
        }
        
        public bool CheckUserLoginID(int id, string loginID)
        {
            sqlStr = "SELECT count(ID) FROM tblUser WHERE UPPER(LoginID) = @LoginID AND ID <> @ID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = loginID.ToUpper();

                return SQLUtil.ConvertInt(command.ExecuteScalar()) > 0;
            }
        }

        public UserInfo AddUser(UserInfo info)
        {
            sqlStr = "INSERT INTO tblUser(IsAdmin,LoginID,LoginPwd,Name,Email,CreatedDate) " +
                    " VALUES(@IsAdmin,@LoginID,@LoginPwd,@Name,@Email,GETDATE());" +
                    " SELECT @@IDENTITY;";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = info.IsAdmin;
                command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = info.LoginID;
                command.Parameters.Add("@LoginPwd", SqlDbType.VarChar).Value = info.LoginPwd;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = info.Name;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = SQLUtil.TrimNull(info.Email);

                info.ID = SQLUtil.ConvertInt(command.ExecuteScalar());

                return info;
            }
        }

        public void UpdateUser(UserInfo info)
        {
            sqlStr = "UPDATE tblUser SET IsAdmin = @IsAdmin, LoginID = @LoginID, Name = @Name, Email = @Email ";
            if (info.IsUpdatePassword)
                sqlStr += " ,LoginPwd = @LoginPwd ";
            sqlStr += " WHERE ID = @ID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@IsAdmin", SqlDbType.Bit).Value = info.IsAdmin;
                command.Parameters.Add("@LoginID", SqlDbType.VarChar).Value = info.LoginID;
                if (info.IsUpdatePassword)
                    command.Parameters.Add("@LoginPwd", SqlDbType.VarChar).Value = info.LoginPwd;
                command.Parameters.Add("@Name", SqlDbType.NVarChar).Value = info.Name;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = SQLUtil.TrimNull(info.Email);
                command.Parameters.Add("@ID", SqlDbType.Int).Value = info.ID;

                command.ExecuteNonQuery();
            }
        }

        public void UpdatePassword(int id, string loginPwd)
        {
            sqlStr = "UPDATE tblUser SET LoginPwd = @LoginPwd WHERE ID = @ID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@LoginPwd", SqlDbType.VarChar).Value = loginPwd;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteUser(int id)
        {
            sqlStr = "DELETE FROM tblUser WHERE ID = @ID";

            using (SqlCommand command = ConnectionUtil.GetCommand(sqlStr))
            {
                command.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                command.ExecuteNonQuery();
            }
        }

        #endregion
    }
}