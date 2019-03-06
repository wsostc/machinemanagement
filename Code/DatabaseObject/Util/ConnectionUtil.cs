using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseObject.Util
{
    public class ConnectionUtil : IDisposable
    {
        public static string connectionStr = null;

        private static SqlConnection conn = null;
        private static SqlTransaction trans = null;

        private static bool connOwner = false;
        private static bool transOwner = false;

        public ConnectionUtil()
        {
            if (conn == null)
            {
                conn = new SqlConnection(connectionStr);
                conn.Open();

                connOwner = true;
            }
        }

        public static void Init(string configString)
        {
            connectionStr = configString;
        }

        public static SqlCommand GetCommand(string sqlStr, CommandType type = CommandType.Text)
        {
            SqlCommand command = new SqlCommand(sqlStr, conn);
            command.CommandTimeout = 300;
            command.CommandType = type;
            if (trans != null)
                command.Transaction = trans;

            return command;
        }

        public void BeginTransaction()
        {
            if (trans == null)
            {
                trans = conn.BeginTransaction();

                transOwner = true;
            }
        }

        public void CommitTransaction()
        {
            if (transOwner == true)
            {
                trans.Commit();
                this.CleanTransaction();
            }
        }

        public void RollbackTransaction()
        {
            if (transOwner == true)
            {
                trans.Rollback();
                this.CleanTransaction();
            }                
        }

        public void CleanTransaction()
        {
            trans = null;
            transOwner = false;
        }
    
        public void CloseConnection()
        {
            if (conn != null)
            {
                conn.Close();
                conn = null;
            }
        }

        public void Dispose()
        {
            if (connOwner == true && transOwner == false)
            {
                if (conn != null)
                {
                    conn.Close();
                    conn.Dispose();

                    conn = null;
                }
            }
        }
    }
}