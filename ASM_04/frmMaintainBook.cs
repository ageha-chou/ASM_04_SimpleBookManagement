using ASM_04.Presenters;
using ASM_04.Views;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using AppLib;

namespace ASM_04 {
    public partial class frmMaintainBook : Form, IMainFrame {
        public frmMaintainBook() {
            InitializeComponent();
        }

        public frmMaintainBook(Employee e) : this() {
            lblEmp.Text = $"EmpID: {e.EmpID}";
        }

        private DataTable dtBook;

        BookPresenter presenter;
        private bool isNew = false;
        private bool isError = false;

        public string BookID { get => txtBookID.Text; set => txtBookID.Text = value; }
        public string BookName { get => txtBookName.Text; set => txtBookName.Text = value; }
        public string BookPrice { get => txtBookPrice.Text; set => txtBookPrice.Text = value; }
        public object BookDataTable { get => dtBook; set => dtBook = (DataTable)value; }
        public object BookBindingSource { get => bsBook; set => bsBook.DataSource = value; }
        public object BookDataNav { set => bnBook.BindingSource = (BindingSource)value; }
        public string TotalPriceLabel { get => lblPriceTotal.Text; set => lblPriceTotal.Text = value; }
        public object DgvBook { get => dgvBooks.DataSource; set => dgvBooks.DataSource = value; }
        public string ProcessMessage { get; set; }
        public string BookNameSearchVal => txtSearch.Text;

        private void frmMaintainBook_Load(object sender, EventArgs e) {
            LoadData();
        }

        private void btnGetAll_Click(object sender, EventArgs e) {
            LoadData();
        }

        private void LoadData() {
            presenter = new BookPresenter(this);
            presenter.GetAllBooks();

            txtBookName.ReadOnly = true;
            txtBookPrice.ReadOnly = true;

            ClearDataBinding();
            SetDataBinding();
        }

        private void SetDataBinding() {
            txtBookID.DataBindings.Add("Text", BookBindingSource, "BookID");
            txtBookName.DataBindings.Add("Text", BookBindingSource, "BookName");
            txtBookPrice.DataBindings.Add("Text", BookBindingSource, "BookPrice");
        }

        private void ClearDataBinding() {
            txtBookID.DataBindings.Clear();
            txtBookName.DataBindings.Clear();
            txtBookPrice.DataBindings.Clear();
        }

        private void btnNew_Click(object sender, EventArgs e) {
            isNew = true;
            txtBookName.ReadOnly = false;
            txtBookPrice.ReadOnly = false;

            ClearDataBinding();

            txtBookID.Clear();
            txtBookName.Clear();
            txtBookPrice.Clear();

            txtBookName.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e) {
            txtBookName.ReadOnly = false;
            txtBookPrice.ReadOnly = false;
            ClearDataBinding();
            txtBookName.Focus();
        }

        private void dgvBooks_SelectionChanged(object sender, EventArgs e) {
            if(isNew) {
                SetDataBinding();
                isNew = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e) {
            if (!isError) {
                presenter = new BookPresenter(this);
                if(isNew) {
                    presenter.AddBook();
                    isNew = false;
                } else {
                    presenter.UpdateBook();
                }
                MessageBox.Show(ProcessMessage);
                SetDataBinding();
                txtBookName.ReadOnly = true;
                txtBookPrice.ReadOnly = true;
            }
        }

        private void SetErrorMsg(Control control, CancelEventArgs e, string msg) {
            isError = true;
            e.Cancel = true;
            control.Focus();
            control.BackColor = Color.Tomato;
            errorProvider.SetError(control, msg);
        }

        private void SetNormal(Control control, CancelEventArgs e, string msg) {
            isError = false;
            e.Cancel = false;
            control.BackColor = Color.White;
            errorProvider.SetError(control, msg);
        }

        private void txtBookName_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtBookName.Text)) {
                SetErrorMsg(txtBookName, e, "Please enter Book Name");
            } else {
                SetNormal(txtBookName, e, "");
            }
        }

        private void txtBookPrice_Validating(object sender, CancelEventArgs e) {
            if (string.IsNullOrWhiteSpace(txtBookPrice.Text)) {
                SetErrorMsg(txtBookPrice, e, "Please enter Book Price");
            } else {
                try {
                    float price = float.Parse(txtBookPrice.Text);
                    if(price < 0) {
                        SetErrorMsg(txtBookPrice, e, "Book Price is >= 0.");
                    } else {
                        SetNormal(txtBookPrice, e, "");
                    }
                } catch (FormatException) {
                    SetErrorMsg(txtBookPrice, e, "Book Price is a float.");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e) {
            if(!isNew) {
                DialogResult r = MessageBox.Show("Do you want to delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(r == DialogResult.Yes) {
                    presenter = new BookPresenter(this);
                    presenter.DeleteBook();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e) {
            presenter = new BookPresenter(this);
            presenter.FilterByBookName();
        }
    }
}
