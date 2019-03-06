using System;

namespace DatabaseObject.Util
{
    public static class TransactionUtil
    {
        [NonSerialized]
        private static ConnectionUtil dbConn = null;

        public static void OnEntry()
        {
            dbConn = new ConnectionUtil();

            dbConn.BeginTransaction();
        }

        public static void OnSuccess()
        {
            dbConn.CommitTransaction();
        }

        public static void OnException()
        {
            dbConn.RollbackTransaction();
        }

        public static void OnExit()
        {
            dbConn.Dispose();
        }
    }
}