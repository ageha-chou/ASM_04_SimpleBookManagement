using System.Data;
using System.Data.SqlClient;

namespace AppLib.BLL {
    public class EmployeeBLL {
        private static string CON_STRING = @"server=SE140622\SQLEXPRESS;database=ASM04_BookStore;uid=sa;pwd=123456";
        public Employee Emp { get; set; }
        private static EmployeeBLL _instance = null;
        public static EmployeeBLL Instance {
            get
            {
                if(_instance == null) {
                    _instance = new EmployeeBLL();
                }
                return _instance;
            }
        }
        private EmployeeBLL() {

        }
        public bool CheckLogin(string empID, string pwd) {
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "SELECT EmpRole FROM Employee " +
                         "WHERE EmpID = @id AND EmpPassword = @pwd";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", empID);
            cmd.Parameters.AddWithValue("@pwd", pwd);
            try {
                if (con.State == ConnectionState.Closed) {
                    con.Open();
                }

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read()) {
                    Emp = new Employee {
                        EmpID = empID,
                        EmpRole = dr.GetBoolean(0)
                    };
                    dr.Close();
                    return true;
                }
            } catch (SqlException) {
                throw;
            } finally {
                if (con.State != ConnectionState.Closed) {
                    con.Close();
                }
            }
            return false;
        }

        public bool ChangePwd(string empID, string newPwd) {
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "UPDATE Employee " +
                         "SET EmpPassword = @pwd " +
                         "WHERE EmpID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", empID);
            cmd.Parameters.AddWithValue("@pwd", newPwd);
            try {
                if (con.State == ConnectionState.Closed) {
                    con.Open();
                }

                return cmd.ExecuteNonQuery() > 0;
            } catch (SqlException) {
                throw;
            } finally {
                if (con.State != ConnectionState.Closed) {
                    con.Close();
                }
            }
        }
    }
}
