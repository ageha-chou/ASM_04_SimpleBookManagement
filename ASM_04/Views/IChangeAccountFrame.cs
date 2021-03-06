using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_04.Views {
    interface IChangeAccountFrame {
        string NewPwd { get; }
        string ConfirmPwd { get; }
        string ProcessMessage { get; set; }
        object Emp { get; }
    }
}
