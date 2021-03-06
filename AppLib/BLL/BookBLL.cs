using System.Data.SqlClient;
using System.Data;
using System;

namespace AppLib.BLL {
    public class BookBLL {
        private static string CON_STRING = @"server=SE140622\SQLEXPRESS;database=ASM04_BookStore;uid=sa;pwd=123456";

        public DataTable GetBooks() {
            DataTable dt = null;
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "SELECT BookID, BookName, BookPrice FROM Book";
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            try {
                if(con.State == ConnectionState.Closed) {
                    con.Open();
                }

                dt = new DataTable();
                da.Fill(dt);
            }
            catch (SqlException){
                throw;
            }
            finally{
                if (con.State != ConnectionState.Closed) {
                    con.Close();
                }
            }
            return dt;
        }

        public int AddBook(Book b) {
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "INSERT INTO Book(BookName, BookPrice) output INSERTED.BookID " +
                         "VALUES(@name, @price)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@name", b.BookName);
            cmd.Parameters.AddWithValue("@price", b.BookPrice);
            try {
                if(con.State == ConnectionState.Closed) {
                    con.Open();
                }

                return int.Parse(cmd.ExecuteScalar().ToString());
            }
            catch(SqlException) {
                throw;
            } catch (FormatException) {
                return 0;
            }
            finally {
                if(con.State != ConnectionState.Closed) {
                    con.Close();
                }
            }
        }

        public bool UpdateBook(Book b) {
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "UPDATE Book " +
                         "SET BookName = @name, BookPrice = @price " +
                         "WHERE BookID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", b.BookID);
            cmd.Parameters.AddWithValue("@name", b.BookName);
            cmd.Parameters.AddWithValue("@price", b.BookPrice);
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

        public bool DeleteBook(int bookID) {
            SqlConnection con = new SqlConnection(CON_STRING);
            string sql = "DELETE Book " +
                         "WHERE bookID = @id";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@id", bookID);
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
