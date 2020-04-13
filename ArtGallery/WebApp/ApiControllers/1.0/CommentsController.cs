using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    
    public class CommentsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public CommentsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetComments()
        {
            return Ok(await _uow.Comments.DTOAllAsync());
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetComment(Guid id)
        {
            var comment = await _uow.Comments.DTOFirstOrDefaultAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return comment;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<IActionResult> PutComment(Guid id, CommentEditDTO commentEditDTO)
        {
            if (id != commentEditDTO.Id)
            {
                return BadRequest();
            }

            var comment = await _uow.Comments.FirstOrDefaultAsync(commentEditDTO.Id, User.UserGuidId());

            if (comment == null)
            {
                return BadRequest();
            }

            comment.CommentBody = commentEditDTO.CommentBody;
            _uow.Comments.Update(comment);
            
            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Comments.ExistsAsync(id, User.UserGuidId()))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            _uow.Comments.Add(comment);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Comment>> DeleteComment(Guid id)
        {
            await _uow.Comments.DeleteAsync(id, User.UserGuidId());
            await _uow.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
