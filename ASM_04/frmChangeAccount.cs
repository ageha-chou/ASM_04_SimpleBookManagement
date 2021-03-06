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
    public partial class frmChangeAccount : Form, IChangeAccountFrame {
        public frmChangeAccount() {
            InitializeComponent();
        }

        EmpPresenter presenter;

        private Employee emp;

        public string NewPwd => txtPwd.Text;

        public string ConfirmPwd => txtConfirm.Text;

        public string ProcessMessage { get; set; }

        public object Emp => emp;

        public frmChangeAccount(Employee e) : this() {
            emp = e;
            btnChangePass.Enabled = false;
        }

        private void btnChangePass_Click(object sender, EventArgs e) {
            presenter = new EmpPresenter(this);
            presenter.ChangePwd();
            if (!string.IsNullOrEmpty(ProcessMessage)) {
                MessageBox.Show(ProcessMessage);
                if (ProcessMessage.Contains("successfully")) {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        private void txtConfirm_TextChanged(object sender, EventArgs e) {
            if (txtConfirm.Text.Equals(txtPwd.Text)) {
                btnChangePass.Enabled = true;
            }
        }
    }
}
