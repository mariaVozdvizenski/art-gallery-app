using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App.Mappers;
using Contracts.BLL.App;
using Contracts.DAL.App;
using Domain;
using Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CommentsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private CommentMapper _commentMapper = new CommentMapper();

        public CommentsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Comments
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentView>>> GetComments()
        {
            var bllCommentViews = await _bll.Comments.GetAllForViewAsync();
            var apiCommentViews = bllCommentViews.Select(e => _commentMapper.MapCommentView(e));
            return apiCommentViews.ToList();
        }

        // GET: api/Comments/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            return _commentMapper.MapComment(comment);
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(Guid id, Comment commentEditDTO)
        {
            commentEditDTO.AppUserId = User.UserGuidId();
            
            if (id != commentEditDTO.Id)
            {
                return BadRequest();
            }

            var bllEntity = _commentMapper.Map(commentEditDTO);
            await _bll.Comments.UpdateAsync(bllEntity, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Comments
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Domain.App.Comment>> PostComment(Comment comment)
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
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Domain.App.Comment>> DeleteComment(Guid id)
        {
            var comment = await _bll.Comments.FirstOrDefaultAsync(id);
            
            if (comment == null)
            {
                return NotFound();
            }

            await _bll.Comments.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(comment);
        }

    }
}
