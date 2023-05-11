using BookStore.Data;

namespace BookShop
{
    public partial class Form1 : Form
    {
        BookRepository bookRepository = new BookRepository();
        public Form1()
        {
            InitializeComponent();
            this.Load += FormMain_Load;
        }

        private void FormMain_Load(object? sender, EventArgs e)
        {
            dgvBooks.DataSource = bookRepository.Books;
        }



        private void btnDelete_Click(object sender, EventArgs e)
        {
            int? id = dgvBooks.SelectedRows[0].Cells["ID"].Value as int?;
            Book book = bookRepository.GetBookById(id);
            book.IsDeleted = true;
            bookRepository.SaveChanges();
            bookRepository.Load();
            dgvBooks.DataSource = null;
            dgvBooks.DataSource = bookRepository.Books;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            FormAdd formAddBook = new FormAdd();
            if (formAddBook.ShowDialog() == DialogResult.OK)
            {
                Book book = formAddBook.Book;

                bookRepository.Books.Add(book);

                bookRepository.SaveChanges();
                bookRepository.Load();
                this.dgvBooks.DataSource = null;
                this.dgvBooks.DataSource = bookRepository.Books;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvBooks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a book to edit");
                return;
            }
            int? id = dgvBooks.SelectedRows[0].Cells["ID"].Value as int?;
            Book book = bookRepository.GetBookById(id);
            FormAdd formAddBook = new FormAdd(book);
            if (formAddBook.ShowDialog() == DialogResult.OK)
            {
                bookRepository.SaveChanges();
                bookRepository.Load();
                dgvBooks.DataSource = null;
                dgvBooks.DataSource = bookRepository.Books;
            }
        }
    }
}