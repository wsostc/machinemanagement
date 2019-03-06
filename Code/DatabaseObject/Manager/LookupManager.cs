using DatabaseObject.DadaAccess;
using DatabaseObject.Domain;
using DatabaseObject.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseObject.Manager
{
    public class LookupManager
    {
        private static LookupDao lookupDao = new LookupDao();

        public static void DoInit()
        {
            using (ConnectionUtil conn = new ConnectionUtil())
            {
                _ServerTypes = lookupDao.GetLookups("lkpServerType");
                _OSTypes = lookupDao.GetLookups("lkpOSType");
                _Statuese = lookupDao.GetLookups("lkpStatus");
            }
        }

        #region "lkpServerType"
        private static List<KeyValueInfo> _ServerTypes = null;

        public static List<KeyValueInfo> GetServerTypes()
        {
            return _ServerTypes;
        }

        public static KeyValueInfo GetServerType(int id)
        {
            List<KeyValueInfo> infos = GetServerTypes();

            return (from KeyValueInfo temp in infos where temp.ID == id select temp).FirstOrDefault();
        }

        public static int GetServerTypeId(string desc)
        {
            List<KeyValueInfo> infos = GetServerTypes();

            KeyValueInfo info = (from KeyValueInfo temp in infos where temp.Name.Equals(desc, StringComparison.CurrentCultureIgnoreCase) select temp).FirstOrDefault();

            if (info == null)
                return 0;
            else
                return info.ID;
        }

        public static string GetServerTypeDesc(int id)
        {
            List<KeyValueInfo> infos = GetServerTypes();

            string desc = (from KeyValueInfo temp in infos where temp.ID == id select temp.Name).FirstOrDefault();

            if (desc == null)
                return "";
            else
                return desc;
        }
        #endregion

        #region "lkpOSType"
        private static List<KeyValueInfo> _OSTypes = null;

        public static List<KeyValueInfo> GetOSTypes()
        {
            return _OSTypes;
        }

        public static KeyValueInfo GetOSType(int id)
        {
            List<KeyValueInfo> infos = GetOSTypes();

            return (from KeyValueInfo temp in infos where temp.ID == id select temp).FirstOrDefault();
        }

        public static int GetOSTypeId(string desc)
        {
            List<KeyValueInfo> infos = GetServerTypes();

            KeyValueInfo info = (from KeyValueInfo temp in infos where temp.Name.Equals(desc, StringComparison.CurrentCultureIgnoreCase) select temp).FirstOrDefault();

            if (info == null)
                return 0;
            else
                return info.ID;
        }

        public static string GetOSTypeDesc(int id)
        {
            List<KeyValueInfo> infos = GetServerTypes();

            string desc = (from KeyValueInfo temp in infos where temp.ID == id select temp.Name).FirstOrDefault();

            if (desc == null)
                return "";
            else
                return desc;
        }
        #endregion

        #region "lkpServerType"
        private static List<KeyValueInfo> _Statuese = null;

        public static List<KeyValueInfo> GetStatuese()
        {
            return _Statuese;
        }

        public static KeyValueInfo GetStatu(int id)
        {
            List<KeyValueInfo> infos = GetStatuese();

            return (from KeyValueInfo temp in infos where temp.ID == id select temp).FirstOrDefault();
        }

        public static int GetStatuId(string desc)
        {
            List<KeyValueInfo> infos = GetStatuese();

            KeyValueInfo info = (from KeyValueInfo temp in infos where temp.Name.Equals(desc, StringComparison.CurrentCultureIgnoreCase) select temp).FirstOrDefault();

            if (info == null)
                return 0;
            else
                return info.ID;
        }

        public static string GetStatuDesc(int id)
        {
            List<KeyValueInfo> infos = GetStatuese();

            string desc = (from KeyValueInfo temp in infos where temp.ID == id select temp.Name).FirstOrDefault();

            if (desc == null)
                return "";
            else
                return desc;
        }
        #endregion
    }
}
