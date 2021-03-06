using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_04.Views {
    interface IMainFrame {
        string BookID { get; set; }
        string BookName { get; set; }
        string BookPrice { get; set; }
        string TotalPriceLabel { get; set; }
        string ProcessMessage { get; set; }
        string BookNameSearchVal { get; }
        object BookDataTable { get; set; }
        object BookBindingSource { get; set; }
        object BookDataNav { set; }
        object DgvBook { get; set; }
    }
}
