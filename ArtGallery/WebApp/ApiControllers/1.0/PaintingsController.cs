using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    public class PaintingsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private PaintingMapper _paintingMapper = new PaintingMapper();

        public PaintingsController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Paintings
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaintingView>>> GetPaintings()
        {
            var query = await _bll.Paintings.GetAllForViewAsync();
            return Ok(query.Select(e => _paintingMapper.MapPaintingView(e)).ToList());
        }

        // GET: api/Paintings/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<PaintingView>> GetPainting(Guid id)
        {
            var painting = await _bll.Paintings.GetFirstOrDefaultForViewAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return Ok(_paintingMapper.MapPaintingView(painting));
        }

        // PUT: api/Paintings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<IActionResult> PutPainting(Guid id, Painting paintingDTO)
        {
            if (id != paintingDTO.Id)
            {
                return BadRequest();
            }

            var bllEntity = _paintingMapper.Map(paintingDTO);
            await _bll.Paintings.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Paintings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Domain.App.Painting>> PostPainting(Painting paintingCreateDTO)
        {
            var bllEntity = _paintingMapper.Map(paintingCreateDTO);
            _bll.Paintings.Add(bllEntity);
            await _bll.SaveChangesAsync();

            paintingCreateDTO.Id = bllEntity.Id;

            return CreatedAtAction("GetPainting", new { id = paintingCreateDTO.Id }, paintingCreateDTO);
        }

        // DELETE: api/Paintings/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Domain.App.Painting>> DeletePainting(Guid id)
        {
            var painting = await _bll.Paintings.FirstOrDefaultAsync(id, User.UserGuidId());
            
            if (painting == null)
            {
                return NotFound();
            }

            await _bll.Paintings.RemoveAsync(id);
            await _bll.SaveChangesAsync();

            return Ok(painting);
        }
    }
}
