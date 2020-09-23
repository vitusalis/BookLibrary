using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BirthdayLibrary.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BirthdayLibrary.BLL.Models
{
    [Route("api/Books")]
    [ApiController]
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;
         
        public BookRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks()
        {
            return await context.Books.Include(b=>b.BookAuthors).ThenInclude(ba=>ba.Author).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id)
        {
            return await context.Books.FirstOrDefaultAsync(e=> e.Id == id);
        }
        
        [HttpPost]
        public async Task<Book> AddBook(Book book)
        {
            var result = await context.Books.AddAsync(book);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        
        [HttpPut("{id}")]
        public async Task<Book> UpdateBook(Book book)
        {
            var result = await context.Books.FirstOrDefaultAsync(e => e.Id == book.Id);
            if (result != null)
            {
                result.Title = book.Title;
                result.ISBN = book.ISBN;
                result.YearReleased = book.YearReleased;
                //result.BookAuthors = book.BookAuthors;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ;
            }

            return book;
        }
        
        [HttpDelete("{id}")]
        public async void DeleteBook(int bookId)
        {
            var book = await context.Books.FindAsync(bookId);
            if (book != null)
            {
                context.Books.Remove(book);
                await context.SaveChangesAsync();
            }
        }

        [HttpGet]
        [Route("api/BookByParam")]
        public async Task<Book> GetBookByName(string searchString)
        {
            var result = await context.Books.FirstOrDefaultAsync(e =>
            e.Title.ToLower().Contains(searchString.ToLower()) || e.ISBN == searchString);
            return result;
        }
    }
}
