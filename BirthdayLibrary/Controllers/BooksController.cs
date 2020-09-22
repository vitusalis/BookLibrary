using Microsoft.AspNetCore.Mvc;
using BirthdayLibrary.BLL.Data;

namespace BirthdayLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : MyDBController<BooksController, EfCoreBookRepository>
    {
        public BooksController(EfCoreMovieRepository repository) : base(repository)
        {

        }

        //private readonly ApplicationDbContext _context;

        //public BooksController(ApplicationDbContext context)
        //{
        //    _context = context;
        //}

        // // GET: api/Books
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        //{
        //    return await _context.Books.Include(b=>b.BookAuthors).ThenInclude(cs=>cs.Author).ToListAsync();
        //}

        //// GET: api/Books/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Book>> GetBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    return book;
        //}

        //// PUT: api/Books/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutBook(int id, Book book)
        //{
        //    if (id != book.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(book).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!BookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Books
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Book>> PostBook(Book book)
        //{
        //    _context.Books.Add(book);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetBook", new { id = book.Id }, book);
        //}

        //// DELETE: api/Books/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Book>> DeleteBook(int id)
        //{
        //    var book = await _context.Books.FindAsync(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Books.Remove(book);
        //    await _context.SaveChangesAsync();

        //    return book;
        //}

        //private bool BookExists(int id)
        //{
        //    return _context.Books.Any(e => e.Id == id);
        //}
    }
}
