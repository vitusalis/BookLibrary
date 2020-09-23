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
    [Route("api/Authors")]
    [ApiController]
    public class AuthorRepository : IAuthorRepository
    {
        private readonly ApplicationDbContext context;
         
        public AuthorRepository(ApplicationDbContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public async Task<IEnumerable<Author>> GetAuthors()
        {
            return await context.Authors.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<Author> GetAuthor(int authorId)
        {
            return await context.Authors.FindAsync(authorId);
        }
        
        [HttpPost]
        public async Task<Author> AddAuthor(Author author)
        {
            var result = await context.Authors.AddAsync(author);
            await context.SaveChangesAsync();
            return result.Entity;
        }
        
        [HttpPut("{id}")]
        public async Task<Author> UpdateAuthor(Author author)
        {
            var result = await context.Authors.FirstOrDefaultAsync(e => e.Id == author.Id);
            if (result != null)
            {
                result.Name = author.Name;
                result.LastName = author.LastName;
                result.Email = author.Email;
                result.Birthday = author.Birthday;
                //result.BookAuthors = author.BookAuthors;
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                ;
            }

            return author;
        }
        
        [HttpDelete("{id}")]
        public async void DeleteAuthor(int authorId)
        {
            var author = await context.Authors.FindAsync(authorId);
            if (author != null)
            {
                context.Authors.Remove(author);
                await context.SaveChangesAsync();
            }
        }

    }
}
