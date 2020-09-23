using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayLibrary.BLL.Models
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetBooks();
        Task<Book> GetBook(int bookId);
        Task<Book> AddBook(Book book);
        Task<Book> UpdateBook(Book book);
        void DeleteBook(int bookId);
        
    }
}
