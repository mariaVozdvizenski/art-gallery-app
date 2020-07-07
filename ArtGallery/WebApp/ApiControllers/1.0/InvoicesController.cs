using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Contracts.BLL.App;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;
using PublicApi.DTO.v1.Mappers;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Invoices
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class InvoicesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly InvoiceMapper _invoiceMapper = new InvoiceMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="bll"></param>
        public InvoicesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Invoices
        /// <summary>
        /// Get all Invoices for current Order
        /// </summary>
        /// <returns>A collection of Invoices</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Invoice>))]
        public async Task<ActionResult<IEnumerable<InvoiceView>>> GetInvoices(Guid orderId)
        {
            var invoices = await _bll.Invoices.GetInvoicesForCurrentOrderAsync(orderId);
            return Ok(invoices.Select(e => _invoiceMapper.MapForInvoiceView(e)));
        }

        // GET: api/Invoices/5
        /// <summary>
        /// Get a single Invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <returns>Invoice object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<InvoiceView>> GetInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);

            if (invoice == null)
            {
                return NotFound(new MessageDTO("Invoice not found"));
            }

            return Ok(_invoiceMapper.MapForInvoiceView(invoice));
        }
        
        /// <summary>
        ///  Get a generated Invoice Pdf file
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <param name="fileName">Pdf filename</param>
        /// <returns></returns>
        [HttpGet("{id}/{fileName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetInvoicePdf(Guid id, string fileName)
        {
            var filePath = $"../WebApp/ApiControllers/1.0/generated/{fileName}.pdf";

            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine(filePath);
                return NotFound();
            }
            var stream = new FileStream(filePath, FileMode.Open);
            return new  FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"{fileName}.pdf" 
            }; 
        }

        // PUT: api/Invoices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an Invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        /// <param name="invoice">Invoice object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutInvoice(Guid id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest(new MessageDTO("Id and invoice.id do not match"));
            }
            
            if (!await _bll.Invoices.ExistsAsync(invoice.Id))
            {
                return NotFound(new MessageDTO($"Invoice does not exist"));
            }
            
            var bllEntity = _invoiceMapper.Map(invoice);
            await _bll.Invoices.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();
            
            return NoContent();
        }

        // POST: api/Invoices
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create an Invoice
        /// </summary>
        /// <param name="invoice">Invoice object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Invoice>> PostInvoice(InvoiceCreate invoice)
        {
            var bllEntity = _invoiceMapper.MapFromInvoiceCreate(invoice);
            await _bll.Invoices.CalculateInvoiceDataAsync(bllEntity);
            _bll.Invoices.Add(bllEntity);
            await _bll.SaveChangesAsync();

            await _bll.Invoices.GenerateInvoiceAsync(bllEntity.Id, invoice);

            invoice.Id = bllEntity.Id;
            var updatedInvoice = _invoiceMapper.Map(bllEntity);

            return CreatedAtAction("GetInvoice", new { id = updatedInvoice.Id }, updatedInvoice);
        }

        // DELETE: api/Invoices/5
        /// <summary>
        /// Delete an Invoice
        /// </summary>
        /// <param name="id">Invoice Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Invoice))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
        public async Task<ActionResult<Invoice>> DeleteInvoice(Guid id)
        {
            var invoice = await _bll.Invoices.FirstOrDefaultAsync(id);
            if (invoice == null)
            {
                return NotFound(new MessageDTO("Invoice not found"));
            }

            await _bll.Invoices.RemoveAsync(invoice);
            await _bll.SaveChangesAsync();

            return Ok(_invoiceMapper.Map(invoice));
        }
    }
}
