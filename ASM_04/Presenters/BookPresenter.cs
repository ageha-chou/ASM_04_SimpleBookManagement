using AppLib;
using AppLib.BLL;
using ASM_04.Views;
using System;
using System.Data;
using System.Linq;

namespace ASM_04.Presenters {
    class BookPresenter {
        BookBLL _bookModel;
        IMainFrame _view;

        public BookPresenter(IMainFrame view) {
            _view = view;
        }

        public void GetAllBooks() {
            _bookModel = new BookBLL();
            DataTable dtBook = _bookModel.GetBooks();
            dtBook.PrimaryKey = new DataColumn[] { dtBook.Columns["BookID"] };
            _view.BookDataTable = dtBook;
            //set bindingsource
            _view.BookBindingSource = dtBook;
            //Set dgv
            _view.DgvBook = _view.BookBindingSource;
            //set bindingnav
            _view.BookDataNav = _view.BookBindingSource;

            //set total price default
            SetPrice(dtBook, "");
        }

        private void SetPrice(DataTable dt, string filter) {
            string price = "0";
            if (dt.Rows.Count > 0) {
                price = dt.Compute("SUM(BookPrice)", filter).ToString();
            }

            _view.TotalPriceLabel = $"Total price: {price}";
        }

        public void AddBook() {
            Book b = new Book {
                BookName = _view.BookName,
                BookPrice = float.Parse(_view.BookPrice)
            };
            _bookModel = new BookBLL();
            int id = _bookModel.AddBook(b);
            if (id > 0) {
                _view.ProcessMessage = "Added successfully";
                //update grid
                ((DataTable)_view.BookDataTable).Rows.Add(id, b.BookName, b.BookPrice);
                SetPrice(((DataTable)_view.BookDataTable), "");
            } else {
                _view.ProcessMessage = "Added failed";
            }
        }

        public void UpdateBook() {
            Book b = new Book {
                BookID = int.Parse(_view.BookID),
                BookName = _view.BookName,
                BookPrice = float.Parse(_view.BookPrice)
            };
            _bookModel = new BookBLL();
            bool result = _bookModel.UpdateBook(b);
            if (result) {
                _view.ProcessMessage = "Updated successfully";
                DataRow row = ((DataTable)_view.BookDataTable).Rows.Find(b.BookID);
                row["BookName"] = b.BookName;
                row["BookPrice"] = b.BookPrice;
            } else {
                _view.ProcessMessage = "Updated failed";
            }
        }

        public void DeleteBook() {
            int bookID = int.Parse(_view.BookID);
            _bookModel = new BookBLL();
            bool result = _bookModel.DeleteBook(bookID);
            if (result) {
                _view.ProcessMessage = "Deleted successfully";
                //update grid
                DataRow row = ((DataTable)_view.BookDataTable).Rows.Find(bookID);
                ((DataTable)_view.BookDataTable).Rows.Remove(row);
                SetPrice(((DataTable)_view.BookDataTable), "");
            } else {
                _view.ProcessMessage = "Deleted failed";
            }
        }

        public void FilterByBookName() {
            DataView dv = ((DataTable)_view.BookDataTable).DefaultView;
            string filter = $"BookName LIKE '%{_view.BookNameSearchVal}%'";
            dv.RowFilter = filter;
            SetPrice(((DataTable)_view.BookDataTable), filter);
        }
    }
}
