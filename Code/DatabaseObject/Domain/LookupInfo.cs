using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseObject.Domain
{
    public static class OSType
    {
        public const int LINUX = 1;
        public const int UNIX = 2;

        public static string GetDesc(int typeId)
        {
            switch (typeId)
            {
                case LINUX:
                    return "Linux";
                case UNIX:
                    return "Unix";
                default:
                    return "";
            }
        }
    }

    public static class ServerType
    {
        public const int HYPE_V = 1;
        public const int MGMTSVR = 2;
        public const int OTHER = 3;

        public static string GetDesc(int typeId)
        {
            switch (typeId)
            {
                case HYPE_V:
                    return "Hype-v";
                case MGMTSVR:
                    return "MGMTSVR";
                default:
                    return "OTHER";
            }
        }
    }

    public static class Status
    {
        public const int ACTIVE = 1;
        public const int USING = 2;
        public const int FAIL = 99;

        public static string GetDesc(int id)
        {
            switch (id)
            {
                case ACTIVE:
                    return "Active";
                case USING:
                    return "Using";
                default:
                    return "Fail";
            }
        }
    }
}
