using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BookStore.Data
{
    public class BookRepository
    {
        List<Book> books = new List<Book>();
        public List<Book> Books
        {
            get
            {
                if (!books.Any())
                    Load();

                return books;
            }
        }

        public void Load()
        {
            books.Clear();
            string expression = "SELECT * FROM Book";
            using(SqlConnection sqlconnection = new SqlConnection(Utils.connectionString))
            {
                sqlconnection.Open();
                SqlCommand cmd = new SqlCommand(expression, sqlconnection);
                using (SqlDataReader dataReader = cmd.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Book book = new Book();
                        book.ID = (int)dataReader["ID"];
                        book.Name = (string)dataReader["Name"];
                        book.Year = dataReader["Year"] as int?;
                        book.Price = dataReader["Price"] as decimal?;
                        book.IsValueChanged = false;
                        book.IsDeleted = false;
                        books.Add(book);
                    }
                }
;           }
        }
        private void Insert(Book book)
        {
            string expression =  "INSERT INTO Book(Name, Year, Price) VALUES(@Name, @Year, @Price)";
            using(SqlConnection sqlconnection = new SqlConnection(Utils.connectionString))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand(expression, sqlconnection);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@Year", book.Year);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.ExecuteNonQuery();

            }
        }
        private void Update(Book book)
        {
            string expression = "UPDATE Book SET Name = @Name, Year = @Year, Price = @Price WHERE ID = @ID";
            using (SqlConnection sqlconnection = new SqlConnection(Utils.connectionString))
            {
                sqlconnection.Open();
                SqlCommand command = new SqlCommand(expression, sqlconnection);
                command.Parameters.AddWithValue("@ID", book.ID);
                command.Parameters.AddWithValue("@Name", book.Name);
                command.Parameters.AddWithValue("@Year", book.Year);
                command.Parameters.AddWithValue("@Price", book.Price);
                command.ExecuteNonQuery();

            }
        }
        private void Delete(Book book)
        {
            string expression = "Delete from Book Where ID = @ID";
            using (SqlConnection connection = new SqlConnection(Utils.connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(expression, connection);
                command.Parameters.AddWithValue("@ID", book.ID);
                command.ExecuteNonQuery();
            }
        }
        public void SaveChanges()
        {
            
            using (TransactionScope transaction = new TransactionScope())
            {
                foreach (var book in books)
                {
                    
                    if (book.ID == null)
                        Insert(book);
                    else if (book.IsValueChanged)
                        Update(book);
                    else if(book.IsDeleted)
                        Delete(book);
                }
                transaction.Complete();
            }
        }
        public Book GetBookById(int? id)
        {
            if (id == null)
                return null;
            return books.SingleOrDefault(b => b.ID == id);
        }
    }
}
