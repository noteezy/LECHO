using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LECHO.Web.Models
{
    public class RoleTransferModel
    {
        static Dictionary<string, string> map = new Dictionary<string, string>() { 
            {"1", "Адмін"},
            {"2", "Викладач"},
            {"3", "Студент"}
        };
    }
}
