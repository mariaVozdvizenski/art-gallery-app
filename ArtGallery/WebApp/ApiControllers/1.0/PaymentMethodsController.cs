using System;
using System.Collections.Generic;
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
    /// Payment Methods
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    public class PaymentMethodsController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly PaymentMethodMapper _paymentMethodMapper = new PaymentMethodMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public PaymentMethodsController(IAppBLL bll)
        {
            _bll = bll;
        }
        
        // GET: api/PaymentMethods
        /// <summary>
        /// Get all PaymentMethods
        /// </summary>
        /// <returns>A collection of PaymentMethods</returns>
        [HttpGet]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PaymentMethod>))]
        public async Task<ActionResult<IEnumerable<PaymentMethod>>> GetPaymentMethods()
        {
            var query = await _bll.PaymentMethods.GetAllAsync();
            return Ok(query.Select(e => _paymentMethodMapper.Map(e)));
        }

        // GET: api/PaymentMethods/5
        /// <summary>
        /// Get a single PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod Id</param>
        /// <returns>PaymentMethod object</returns>
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaymentMethod))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<PaymentMethod>> GetPaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);

            if (paymentMethod == null)
            {
                return NotFound(new MessageDTO("PaymentMethod was not found"));
            }

            return Ok(_paymentMethodMapper.Map(paymentMethod));
        }

        // PUT: api/PaymentMethods/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update a PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod Id</param>
        /// <param name="paymentMethod">PaymentMethod object</param>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PutPaymentMethod(Guid id, PaymentMethod paymentMethod)
        {
            if (id != paymentMethod.Id)
            {
                return BadRequest(new MessageDTO("Id and paymentMethod.Id do not match"));
            }

            if (!await _bll.PaymentMethods.ExistsAsync(paymentMethod.Id))
            {
                return NotFound(new MessageDTO($"PaymentMethod does not exist"));
            }

            var bllEntity = _paymentMethodMapper.Map(paymentMethod);
            await _bll.PaymentMethods.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/PaymentMethods
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new PaymentMethod
        /// </summary>
        /// <param name="paymentMethod">PaymentMethod object</param>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PaymentMethod))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaymentMethod>> PostPaymentMethod(PaymentMethod paymentMethod)
        {
            var bllEntity = _paymentMethodMapper.Map(paymentMethod);
            
            _bll.PaymentMethods.Add(bllEntity);
            await _bll.SaveChangesAsync();

            paymentMethod.Id = bllEntity.Id;

            return CreatedAtAction("GetPaymentMethod", new { id = paymentMethod.Id }, paymentMethod);
        }

        // DELETE: api/PaymentMethods/5
        /// <summary>
        /// Delete a PaymentMethod
        /// </summary>
        /// <param name="id">PaymentMethod Id</param>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaintingCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PaymentMethod>> DeletePaymentMethod(Guid id)
        {
            var paymentMethod = await _bll.PaymentMethods.FirstOrDefaultAsync(id);
            if (paymentMethod == null)
            {
                return NotFound(new MessageDTO("PaymentMethod not found"));
            }

            await _bll.PaymentMethods.RemoveAsync(paymentMethod);
            await _bll.SaveChangesAsync();

            return _paymentMethodMapper.Map(paymentMethod);
        }
    }
}
