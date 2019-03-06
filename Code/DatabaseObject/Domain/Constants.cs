using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DatabaseObject.Domain
{
    public static class Constants
    {
        public const string SYSTEM_NAME = "Machine Management System";
        public const string BACKLIST = "Back to list";

        private const string FOLDER_DOCUMENTS = "Documents";

        public static string MachineExcelFolder = null;

        //please call this method when web server startup
        public static void SetFolders(string serverPath)
        {
            MachineExcelFolder = Path.Combine(serverPath, FOLDER_DOCUMENTS);
        }
    }
}