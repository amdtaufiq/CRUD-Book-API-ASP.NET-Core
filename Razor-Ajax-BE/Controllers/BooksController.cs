using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Razor_Ajax_BE.Models;

namespace Razor_Ajax_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        public BooksController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetAllBook()
        {
            return await _db.Books.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _db.Entry(book).State = EntityState.Added;
            await _db.SaveChangesAsync();
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if(id != book.Id)
            {
                return BadRequest();
            }
            _db.Entry(book).State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Book>> DeleteBook(int id)
        {
            var book = await _db.Books.FindAsync(id);
            if(book == null)
            {
                return NoContent();
            }
            _db.Entry(book).State = EntityState.Deleted;
            await _db.SaveChangesAsync();
            return book;
        }
    }
}
