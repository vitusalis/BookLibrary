using System;
using System.Collections.Generic;
using System.Linq;
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
            return await context.Books.Include(b => b.BookAuthors).ThenInclude(ba => ba.Author).FirstOrDefaultAsync(e=> e.Id == id);
        }
        
        [HttpPost]
        public async Task<Book> AddBook(BookDTO bookDto)
        {
            Book newBook = new Book();
            newBook.Title = bookDto.Title;
            newBook.ISBN = bookDto.ISBN;
            newBook.YearReleased = bookDto.YearReleased;

            var result = await context.Books.AddAsync(newBook);
            context.SaveChanges();
            newBook = result.Entity;
            if (newBook.Id != 0)
            {
                if (bookDto.AuthorIds != null && bookDto.AuthorIds.Length > 0)
                    foreach (int aid in bookDto.AuthorIds)
                    {
                        BookAuthor ba = new BookAuthor();
                        ba.AuthorId = aid;
                        ba.BookId = newBook.Id;
                        var res = await context.BookAuthors.AddAsync(ba);
                        await context.SaveChangesAsync();
                    }
                await context.SaveChangesAsync();
            }
            return newBook;
        }

        [HttpPut("{id}")]
        public async Task<Book> UpdateBook(BookDTO bookDto)
        {
            var book = await context.Books.FirstOrDefaultAsync(e => e.Id == bookDto.Id);
            if (book != null)
            {
                book.Title = bookDto.Title;
                book.ISBN = bookDto.ISBN;
                book.YearReleased = bookDto.YearReleased;

                var result = context.Books.Update(book);
                context.SaveChanges();

                book = result.Entity;
                if (book.Id != 0)
                {
                    if (bookDto.AuthorIds != null && bookDto.AuthorIds.Length > 0)
                        foreach (int aid in bookDto.AuthorIds)
                        {
                            // ja tem essx autorx
                            var ba = book.BookAuthors.FirstOrDefault(ba => ba.AuthorId == aid);
                            if (ba != null)
                                continue;
                            
                            // novo autor
                            ba = new BookAuthor();
                            ba.AuthorId = aid;
                            ba.BookId = book.Id;
                            var res = await context.BookAuthors.AddAsync(ba);
                        }
                    // apagar bookAuthors que nao estejam nesses ids
                    var outdatedAuthors = book.BookAuthors.Where(ba => bookDto.AuthorIds.Any(id => id != ba.AuthorId));
                    Console.WriteLine("OUTDATED");
                    foreach (var ba in outdatedAuthors)
                    {
                        Console.WriteLine(ba.Author.Name);
                        //context.BookAuthors.Remove(ba);
                    }
                        

                    await context.SaveChangesAsync();
                }
                return book;
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
        public async Task<bool> DeleteBook(int id)
        {
            Console.WriteLine("ID________" + id);
            if (id != 0)
            {
                var book = await context.Books.FindAsync(id);
                if (book != null)
                {
                    context.Books.Remove(book);
                    await context.SaveChangesAsync();
                    return true;
                }
            }
            return false;
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
