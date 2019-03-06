using System;
using System.Collections;
using System.Collections.Generic;

namespace DatabaseObject.Util
{
    public static class SQLUtil
    {
        public static String TrimNull(Object obj)
        {
            if (obj == DBNull.Value)
                return "";
            else if (obj == null)
                return "";
            else
                return obj.ToString().Trim();
        }

        public static int ConvertInt(Object obj)
        {
            if (obj == DBNull.Value)
                return 0;
            else if (obj == null)
                return 0;
            else
                return Convert.ToInt32(obj.ToString().Replace(",", ""));
        }

        public static long ConvertLong(Object obj)
        {
            if (obj == DBNull.Value)
                return 0;
            else if (obj == null)
                return 0;
            else
                return Convert.ToInt64(obj.ToString().Replace(",", ""));
        }

        public static double ConvertDouble(Object obj)
        {
            if (obj == DBNull.Value)
                return 0;
            else if (obj == null)
                return 0;
            else
                return Convert.ToDouble(obj);
        }

        public static DateTime ConvertDateTime(Object obj)
        {
            if (obj == DBNull.Value)
                return DateTime.MinValue;
            else if (obj == null)
                return DateTime.MinValue;
            else
                return Convert.ToDateTime(obj);
        }

        public static byte[] ConvertBytes(object obj)
        {
            if (obj == DBNull.Value)
                return new byte[0];
            else if (obj == null)
                return new byte[0];
            else
                return (byte[])obj;
        }

        public static Boolean ConvertBoolean(Object obj)
        {
            if (obj == DBNull.Value)
                return false;
            else if (obj == null)
                return false;
            else if (obj.ToString() == "1")
                return true;
            else if (obj.ToString() == "0")
                return false;
            else
                return Convert.ToBoolean(obj);
        }

        public static List<int> ConvertToIntList(string[] stringArray)
        {
            List<int> values = new List<int>();
            foreach (string s in stringArray)
            {
                values.Add(ConvertInt(s));
            }
            return values;
        }

        public static string ConvertToStr(List<string> values)
        {
            string inStr = "";
            foreach (string value in values)
            {
                if (inStr.Length > 0)
                    inStr += ",";
                inStr += value;
            }

            return inStr;
        }

        public static string ConvertToStr(List<Object> values)
        {
            string inStr = "";
            foreach (Object value in values)
            {
                if (inStr.Length > 0)
                    inStr += ",";
                inStr += TrimNull(value);
            }

            return inStr;
        }

        public static string ConvertToInStr(List<int> values)
        {
            string inStr = "";
            foreach (long value in values)
            {
                if (inStr.Length > 0)
                    inStr += ",";
                inStr += value.ToString();
            }

            return inStr;
        }

        public static string ConvertToInStr(List<string> values)
        {
            string inStr = "";
            foreach (string value in values)
            {
                if (inStr.Length > 0)
                    inStr += ",";
                inStr += "'" + value.Replace("'", "''") + "'";
            }

            return inStr;
        }

        #region "Return DBNull"
        public static object EmptyStringToNull(string strData)
        {
            if (string.IsNullOrEmpty(strData))
                return DBNull.Value;
            else
                return strData.Trim();
        }

        public static object ZeroToNull(int intData)
        {
            if (intData == 0)
                return DBNull.Value;
            else
                return intData;
        }

        public static object MinDateToNull(DateTime daData)
        {
            if (daData == DateTime.MinValue)
                return DBNull.Value;
            else
                return daData;
        }

        public static object FalseToNull(bool boolData)
        {
            if (boolData == false)
                return DBNull.Value;
            else
                return boolData;
        }

        public static object NullToDBNull(object objData)
        {
            if (objData == null)
                return DBNull.Value;
            else
                return objData;
        }
        #endregion

        public static List<int> GetIDListFromObjectList(IList objects, string idFeildName = "ID")
        {
            List<int> ids = new List<int>();

            foreach (Object obj in objects)
            {
                ids.Add((int)obj.GetType().GetProperty(idFeildName).GetValue(obj, null));
            }

            return ids;
        }

        public static List<Object> GetValueListFromObjectList(IList objects, string valueFeildName = "Value")
        {
            List<Object> values = new List<Object>();

            foreach (Object obj in objects)
            {
                values.Add(obj.GetType().GetProperty(valueFeildName).GetValue(obj, null));
            }

            return values;
        }
    }
}