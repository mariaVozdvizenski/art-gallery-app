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
    /// Addresses
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class AddressesController : ControllerBase
    {
        private readonly IAppBLL _bll;
        private readonly AddressMapper _addressMapper = new AddressMapper();

        /// <summary>
        /// Constructor
        /// </summary>
        public AddressesController(IAppBLL bll)
        {
            _bll = bll;
        }

        // GET: api/Addresses
        /// <summary>
        /// Get all Addresses
        /// </summary>
        /// <returns>Array of Addresses</returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Address>))]
        public async Task<ActionResult<IEnumerable<Address>>> GetAddresses()
        {
            var query = await _bll.Addresses.GetAllAsync(User.UserGuidId());
            return Ok(query.Select(e => _addressMapper.Map(e)));
        }

        // GET: api/Addresses/5
        /// <summary>
        /// Get a single Address
        /// </summary>
        /// <param name="id">Address Id</param>
        /// <returns>Address object</returns>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Address>> GetAddress(Guid id)
        {
            var address = await _bll.Addresses.FirstOrDefaultAsync(id, User.UserGuidId());

            if (address == null)
            {
                return NotFound(new MessageDTO($"Address with id {id} not found"));
            }

            return Ok(_addressMapper.Map(address));
        }

        // PUT: api/Addresses/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Update an Address object
        /// </summary>
        /// <param name="id">Address Id</param>
        /// <param name="address">Address object</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> PutAddress(Guid id, Address address)
        {
            if (id != address.Id)
            {
                return BadRequest(new MessageDTO("Id and address.Id do not match"));
            }

            if (!await _bll.Addresses.ExistsAsync(address.Id, User.UserGuidId()))
            {
                return NotFound(new MessageDTO($"Current user does not have an address with this id {id}"));
            }
            
            var bllEntity = _addressMapper.Map(address);
            await _bll.Addresses.UpdateAsync(bllEntity);
            await _bll.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Addresses
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        /// <summary>
        /// Create a new Address
        /// </summary>
        /// <param name="address">Address object</param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Address>> PostAddress(Address address)
        {
            address.AppUserId = User.UserGuidId();
            var bllEntity = _addressMapper.Map(address);

            if (await _bll.Addresses.NoMoreThanThreeAddresses(User.UserGuidId()))
            {
                _bll.Addresses.Add(bllEntity);
                await _bll.SaveChangesAsync();
                address.Id = bllEntity.Id;
                return CreatedAtAction("GetAddress", new { id = address.Id }, address);
            }
            return BadRequest("No more than three addresses allowed per account.");
        }

        // DELETE: api/Addresses/5
        /// <summary>
        /// Deletes an Address
        /// </summary>
        /// <param name="id">Address id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Address))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(MessageDTO))]
        public async Task<ActionResult<Address>> DeleteAddress(Guid id)
        {
            if (!User.IsInRole("admin")) return BadRequest("Only Admins are allowed to delete addresses.");
            
            var address = await _bll.Addresses.FirstOrDefaultAsync(id);
            
            if (address == null)
            {
                return NotFound(new MessageDTO("Address not found"));
            }

            await _bll.Addresses.RemoveAsync(address, User.UserGuidId());
            await _bll.SaveChangesAsync();

            return Ok(_addressMapper.Map(address));

        }
    }
}
