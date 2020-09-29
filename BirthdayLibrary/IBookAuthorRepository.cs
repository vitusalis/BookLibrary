using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayLibrary.BLL.Models
{
    public interface IBookAuthorRepository
    {
        Task<IEnumerable<BookAuthor>> GetBookAuthors();
        Task<BookAuthor> GetBookAuthor(int id);
        Task<BookAuthor> AddBookAuthor(BookAuthor bookAuthor);
        //Task<BookAuthor> UpdateBookAuthor(BookAuthor book);
        void DeleteBookAuthor(int id);
        
    }
}
