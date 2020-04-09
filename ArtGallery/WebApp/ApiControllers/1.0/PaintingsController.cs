using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contracts.DAL.App;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    
    public class PaintingsController : ControllerBase
    {
        private readonly IAppUnitOfWork _uow;

        public PaintingsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }

        // GET: api/Paintings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaintingDTO>>> GetPaintings()
        {
            return Ok(await _uow.Paintings.DTOAllAsync());
        }

        // GET: api/Paintings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PaintingDTO>> GetPainting(Guid id)
        {
            var painting = await _uow.Paintings.DTOFirstOrDefaultAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return painting;
        }

        // PUT: api/Paintings/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPainting(Guid id, PaintingEditDTO paintingEditDTO)
        {
            if (id != paintingEditDTO.Id)
            {
                return BadRequest();
            }
            
            var painting = await _uow.Paintings.FirstOrDefaultAsync(paintingEditDTO.Id);
            
            if (painting == null)
            {
                return BadRequest();
            }
            
            painting.Title = paintingEditDTO.Title;
            painting.Price = paintingEditDTO.Price;
            painting.Size = paintingEditDTO.Size;
            painting.ArtistId = paintingEditDTO.ArtistId;
            painting.Artist = await _uow.Artists.FirstOrDefaultAsync(paintingEditDTO.ArtistId);
            
            _uow.Paintings.Update(painting);

            try
            {
                await _uow.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _uow.Paintings.ExistsAsync(id))
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

        // POST: api/Paintings
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Painting>> PostPainting(PaintingCreateDTO paintingCreateDTO)
        {
            var painting = new Painting
            {
                Description = paintingCreateDTO.Description,
                Price = paintingCreateDTO.Price,
                ArtistId = paintingCreateDTO.ArtistId,
                Size = paintingCreateDTO.Size,
                Title = paintingCreateDTO.Title
            };
            
            _uow.Paintings.Add(painting);
            await _uow.SaveChangesAsync();

            return CreatedAtAction("GetPainting", new { id = painting.Id }, painting);
        }

        // DELETE: api/Paintings/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Painting>> DeletePainting(Guid id)
        {
            var painting = await _uow.Paintings.FirstOrDefaultAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            _uow.Paintings.Remove(painting);
            await _uow.SaveChangesAsync();

            return painting;
        }
        
    }
}
