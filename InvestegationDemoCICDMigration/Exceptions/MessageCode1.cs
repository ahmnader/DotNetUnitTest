using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevOpsWebApp.Exceptions
{
    public static class CommonHelpersMessageCode
    {
        public const string GeneralError = "E000001";
        public const string ValidationBehaviourError = "E000002"; 
        public const string DatabaseConflict = "E000003";
        public const string ResourceNotFound = "E000004";
    }
}
