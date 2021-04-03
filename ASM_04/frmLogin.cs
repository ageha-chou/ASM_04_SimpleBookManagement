using AppLib;
using ASM_04.Presenters;
using ASM_04.Views;
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
    public partial class frmLogin : Form, ILoginFrame {
        public frmLogin() {
            InitializeComponent();
        }

        EmpPresenter presenter;

        public string EmpID => txtEmpID.Text;

        public string EmpPwd => txtPwd.Text;

        public object Emp { get; set; }
        public string ProcessMessage { get; set; }

        private void btnLogin_Click(object sender, EventArgs e) {
            CheckLogin();
        }

        private void CheckLogin() {
            if(string.IsNullOrWhiteSpace(EmpID) || string.IsNullOrWhiteSpace(EmpPwd)) {
                return;
            }
            presenter = new EmpPresenter(this);
            presenter.CheckLogin();
            if (!string.IsNullOrEmpty(ProcessMessage)) {
                MessageBox.Show(ProcessMessage, "Error!!", (MessageBoxButtons)MessageBoxDefaultButton.Button1, MessageBoxIcon.Warning);
            } else {
                bool role = (Emp as Employee).EmpRole;
                DialogResult r;
                this.Hide();
                if (role) {
                    frmMaintainBook frm = new frmMaintainBook(Emp as Employee);
                    r = frm.ShowDialog();
                } else {
                    frmChangeAccount frm = new frmChangeAccount(Emp as Employee);
                    r = frm.ShowDialog();
                }
                if (r == DialogResult.OK) {
                    this.Show();
                } else if (r == DialogResult.Cancel) {
                    this.Close();
                }
            }
        }

        private void txtPwd_KeyPress(object sender, KeyPressEventArgs e) {
            if (e.KeyChar == (char)13) {
                CheckLogin();
            }
        }
    }
}
