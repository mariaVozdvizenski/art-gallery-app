using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;
using InvoiceStatusCode = PublicApi.DTO.v1.InvoiceStatusCode;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Invoice Status Codes
    /// </summary>
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class InvoiceStatusCodesController : ControllerBase
    {
        private readonly AppBLL _bll;
        private readonly InvoiceStatusCodeMapper _invoiceStatusCodeMapper = new InvoiceStatusCodeMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public InvoiceStatusCodesController(AppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/InvoiceStatusCodes
        /// <summary>
        /// Get All InvoiceStatusCodes
        /// </summary>
        /// <returns>A collection of InvoiceStatusCodes</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<InvoiceStatusCode>))]
        public async Task<ActionResult<IEnumerable<InvoiceStatusCode>>> GetInvoiceStatusCodes()
        {
            var query = await _bll.InvoiceStatusCodes.GetAllAsync();
            return Ok(query.Select(e => _invoiceStatusCodeMapper.Map(e)));
        }

        // GET: api/InvoiceStatusCodes/5
        /// <summary>
        /// Get a single InvoiceStatusCode
        /// </summary>
        /// <param name="id">InvoiceStatusCode Id</param>
        /// <returns>InvoiceStatusCode object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceStatusCode))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<InvoiceStatusCode>> GetInvoiceStatusCode(Guid id)
        {
            var invoiceStatusCode = await _bll.InvoiceStatusCodes.FirstOrDefaultAsync(id);

            if (invoiceStatusCode == null)
            {
                return NotFound(new MessageDTO("InvoiceStatusCode not found"));
            }

            return Ok(_invoiceStatusCodeMapper.Map(invoiceStatusCode));
        }

        // PUT: api/InvoiceStatusCodes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an InvoiceStatusCode
        /// </summary>
        /// <param name="id">InvoiceStatusCode Id</param>
        /// <param name="invoiceStatusCode">InvoiceStatusCode object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutInvoiceStatusCode(Guid id, InvoiceStatusCode invoiceStatusCode)
        {
            if (id != invoiceStatusCode.Id)
            {
                return BadRequest(new MessageDTO("Id and invoiceStatusCode.Id do not match"));
            }

            if (!await _bll.InvoiceStatusCodes.ExistsAsync(invoiceStatusCode.Id))
            {
                return NotFound(new MessageDTO($"InvoiceStatusCode does not exist"));
            }

            var bllEntity = _invoiceStatusCodeMapper.Map(invoiceStatusCode);
            await _bll.InvoiceStatusCodes.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/InvoiceStatusCodes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new InvoiceStatusCode
        /// </summary>
        /// <param name="invoiceStatusCode">InvoiceStatusCode object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(InvoiceStatusCode))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<InvoiceStatusCode>> PostInvoiceStatusCode(InvoiceStatusCode invoiceStatusCode)
        {
            var bllEntity = _invoiceStatusCodeMapper.Map(invoiceStatusCode);
            _bll.InvoiceStatusCodes.Add(bllEntity);
            await _bll.SaveChangesAsync();

            invoiceStatusCode.Id = bllEntity.Id;
            
            return CreatedAtAction("GetInvoiceStatusCode", new { id = invoiceStatusCode.Id }, invoiceStatusCode);
        }

        // DELETE: api/InvoiceStatusCodes/5
        /// <summary>
        /// Delete InvoiceStatusCode
        /// </summary>
        /// <param name="id">InvoiceStatusCode Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InvoiceStatusCode))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<InvoiceStatusCode>> DeleteInvoiceStatusCode(Guid id)
        {
            var invoiceStatusCode = await _bll.InvoiceStatusCodes.FirstOrDefaultAsync(id);
            
            if (invoiceStatusCode == null)
            {
                return NotFound(new MessageDTO("InvoiceStatusCode not found"));
            }

            await _bll.InvoiceStatusCodes.RemoveAsync(invoiceStatusCode);
            await _bll.SaveChangesAsync();

            return Ok(_invoiceStatusCodeMapper.Map(invoiceStatusCode));
        }
    }
}
