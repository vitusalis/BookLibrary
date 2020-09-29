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
    [Route("api/BookAuthor")]
    [ApiController]
    public class BookAuthorRepository : IBookAuthorRepository
    {
        private readonly ApplicationDbContext context;
         
        public BookAuthorRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<BookAuthor>> GetBookAuthors()
        {
            return await context.BookAuthors.Include(ba=>ba.Book).Include(ba=>ba.Author).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<BookAuthor> GetBookAuthor(int id)
        {
            return await context.BookAuthors.FirstOrDefaultAsync(e=> e.Book.Id == id);
        }
        
        [HttpPost]
        public async Task<BookAuthor> AddBookAuthor(BookAuthor bookAuthor)
        {
            //if (bookAuthor.BookId != 0)
            //{
            //    bookAuthor.Book = await context.Books.FirstOrDefaultAsync(e => e.Id == bookAuthor.BookId);
            //}
            //if (bookAuthor.AuthorId != 0)
            //{
            //    bookAuthor.Author = await context.Authors.FirstOrDefaultAsync(e => e.Id == bookAuthor.AuthorId);
            //}
            Console.WriteLine("_____________"+bookAuthor.ToString());
            var result = await context.BookAuthors.AddAsync(bookAuthor);
            await context.SaveChangesAsync();
            return result.Entity;
        }

        //[HttpPut("{id}")]
        //public async Task<BookAuthor> UpdateBook(BookAuthor bookAuthor)
        //{
        //    var result = await context.BookAuthors.FirstOrDefaultAsync(e => e.Book.Id == bookAuthor.Book.Id && e.Author.Id == bookAuthor.Author.Id);
        //    if (result != null)
        //    {
        //        result.Author = bookAuthor.Author;
        //        result.Book = bookAuthor.Book;
        //    }

        //    try
        //    {
        //        await context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        ;
        //    }

        //    return bookAuthor;
        //}

        [HttpDelete("{id}")]
        public async void DeleteBookAuthor(int id)
        {
            var ba = await context.BookAuthors.FindAsync(id);
            if (ba != null)
            {
                context.BookAuthors.Remove(ba);
                await context.SaveChangesAsync();
            }
        }

    }
}
