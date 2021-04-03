using AppLib;
using AppLib.BLL;
using ASM_04.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASM_04.Presenters {
    class EmpPresenter {
        ILoginFrame _loginView;
        IChangeAccountFrame _changeAccountView;
        EmployeeBLL _empModel;

        public EmpPresenter(ILoginFrame view) {
            _loginView = view;
        }

        public EmpPresenter(IChangeAccountFrame view) {
            _changeAccountView = view;
        }
        public void CheckLogin() {
            if(string.IsNullOrWhiteSpace(_loginView.EmpID) || string.IsNullOrWhiteSpace(_loginView.EmpID)) {
                return;
            }
            _empModel = new EmployeeBLL();
            bool result = _empModel.CheckLogin(_loginView.EmpID, _loginView.EmpPwd);
            if (result) {
                _loginView.Emp = _empModel.Emp;
                _loginView.ProcessMessage = "";
            } else {
                _loginView.ProcessMessage = "EmpID or password is wrong!!!";
            }
        }

        public void ChangePwd() {
            if (_changeAccountView.NewPwd.Equals(_changeAccountView.ConfirmPwd)) {
                _empModel = new EmployeeBLL();
                Employee emp = (Employee)_changeAccountView.Emp;
                bool result = _empModel.ChangePwd(emp.EmpID, _changeAccountView.NewPwd);
                if (result) {
                    _changeAccountView.ProcessMessage = "Password changed successfully!";
                } else {
                    _changeAccountView.ProcessMessage = "Failed!";
                }
            } else {
                _changeAccountView.ProcessMessage = "Your confirm pass does not match your pass.";
            }
        }
    }
}
