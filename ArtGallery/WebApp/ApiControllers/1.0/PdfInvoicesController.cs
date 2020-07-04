using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PublicApi.DTO.v1;

namespace WebApp.ApiControllers._1._0
{
    /// <summary>
    /// Pdf Invoices
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PdfInvoicesController : ControllerBase
    {
        [HttpGet("{fileName}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(MessageDTO))]
        public async Task<IActionResult> GetPdfInvoice(string fileName)
        {
            var path = "../WebApp/ApiControllers/1.0/generated/" + fileName;
            
            if (!System.IO.File.Exists(path))
            {
                return BadRequest(new MessageDTO("File does not exist"));
            }
            
            Byte[] b = await System.IO.File.ReadAllBytesAsync(path);           
            return File(b, "application/octet-stream");
        }
    }
}