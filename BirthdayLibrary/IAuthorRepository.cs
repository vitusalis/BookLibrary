using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayLibrary.BLL.Models
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthor(int authorId);
        Task<Author> AddAuthor(Author author);
        Task<Author> UpdateAuthor(Author author);
        Task<bool> DeleteAuthor(int authorId);
        
    }
}
