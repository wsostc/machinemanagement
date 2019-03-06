using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MachineManagement.Models
{
    public class SessionTimeoutModel
    {
        public bool IsValid { get { return false; } }
        public string ErrorMessage { get { return "timeout"; } }
    }
}