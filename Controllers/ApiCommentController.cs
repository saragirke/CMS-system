using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmsSystem.Data;
using cmsSystem.Models;

namespace cmsSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiCommentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiCommentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ApiComment
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetComment()
        {
          if (_context.Comment == null)
          {
              return NotFound();
          }
            return await _context.Comment.ToListAsync();
        }

// GET: api/ApiComment/ByNews/{newsId}
[HttpGet("{newsId}")]
public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByNews(int newsId)
{
    //Hämtar kommentarer som tillhör nyhetsinlägget
    var comments = await _context.Comment.Where(c => c.NewsId == newsId).ToListAsync();

    if (comments == null || comments.Count == 0)
    {
        return NotFound();
    }

    return comments;
}
/*
        // PUT: api/ApiComment/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(int id, Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest();
            }

            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        } */

        // POST: api/ApiComment
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
          if (_context.Comment == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Comment'  is null.");
          }
            _context.Comment.Add(comment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }
    }}
/*
        // DELETE: api/ApiComment/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (_context.Comment == null)
            {
                return NotFound();
            }
            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentExists(int id)
        {
            return (_context.Comment?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
*/