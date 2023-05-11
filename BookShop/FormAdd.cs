using BookStore.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookShop
{
    public partial class FormAdd : Form
    {
        Book book = new Book();
        public Book Book { get { return book; } }
        public FormAdd()
        {
            InitializeComponent();
        }
        public FormAdd(Book book) :this()
        {
            this.book = book;
            txtName.Text = book.Name;
            txtPrice.Text = book.Price.ToString();
            txtYear.Text = book.Year.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            book.Name = txtName.Text;
            book.Year = int.Parse(txtYear.Text);
            book.Price = decimal.Parse(txtPrice.Text);

            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
