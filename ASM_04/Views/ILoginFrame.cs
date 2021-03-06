using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_04.Views {
    interface ILoginFrame {
        string EmpID { get; }
        string EmpPwd { get; }
        object Emp { get; set; }
        string ProcessMessage { get; set; }
    }
}
