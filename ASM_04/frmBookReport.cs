using AppLib.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ASM_04 {
    public partial class frmBookReport : Form {
        public frmBookReport() {
            InitializeComponent();
        }

        public frmBookReport(DataTable dt) : this() {
            dgvBooks.DataSource = dt;
            bsBook.DataSource = dt;
            bsBook.Sort = "BookPrice DESC";
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}
