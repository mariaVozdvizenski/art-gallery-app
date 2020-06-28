using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Comments
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CommentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly CommentMapper _commentMapper = new CommentMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public CommentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Comments
        /// <summary>
        /// Get all Comments
        /// </summary>
        /// <returns>A collection of Comments</returns>
        [AllowAnonymous]
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CommentView>))]
        public async Task<ActionResult<IEnumerable<CommentView>>> GetComments()
        {
            var bllCommentViews = await _bll.Comments.GetAllForViewAsync();
            var apiCommentViews = bllCommentViews.Select(e => _commentMapper.MapCommentView(e));
            return Ok(apiCommentViews.ToList());
        }

        // GET: api/Comments/5
        /// <summary>
        /// Get a single Comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <returns>Comment object</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);

            if (comment == null)
            {
                return NotFound(new MessageDTO("Comment not found"));
            }

            return Ok(_commentMapper.MapComment(comment));
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update a Comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        /// <param name="commentEditDTO">Comment object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutComment(Guid id, Comment commentEditDTO)
        {
            var oldComment = await _bll.Comments.FirstOrDefaultAsync(id, User.UserGuidId());
            commentEditDTO.AppUserId = User.UserGuidId();
            commentEditDTO.CreatedAt = oldComment.CreatedAt;
            
            if (id != commentEditDTO.Id)
            {
                return BadRequest(new MessageDTO("Id and comment.Id do not match"));
            }

            if (!await _bll.Comments.ExistsAsync(commentEditDTO.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Comment does not exist"));
            }

            var bllEntity = _commentMapper.Map(commentEditDTO);
            await _bll.Comments.UpdateAsync(bllEntity, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new Comment
        /// </summary>
        /// <param name="comment">Comment object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Comment>> PostComment(Comment comment)
        {
            comment.AppUserId = User.UserGuidId();
            comment.CreatedAt = DateTime.Now;
            
            var bllEntity = _commentMapper.Map(comment);
            
            _bll.Comments.Add(bllEntity);
            await _bll.SaveChangesAsync();
            comment.Id = bllEntity.Id;

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        /// <summary>
        /// Delete a Comment
        /// </summary>
        /// <param name="id">Comment Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Comment))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<Comment>> DeleteComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);
            
            if (comment == null)
            {
                return NotFound(new MessageDTO("Comment not found"));
            }

            await _bll.Comments.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(_commentMapper.MapComment(comment));
        }
    }
}
