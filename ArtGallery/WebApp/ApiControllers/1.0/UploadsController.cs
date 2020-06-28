using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO.Enumeration;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.ApiControllers._1._0
{
    
    /// <summary>
    /// Uploads
    /// </summary>
    [ApiController]
    [ApiVersion( "1.0" )]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "admin")]
    
    public class UploadsController : ControllerBase
    {
        /// <summary>
        /// Create a new Upload
        /// </summary>
        /// <param name="file">An image to be uploaded</param>
        [HttpPost]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PostUpload(IFormFile file)
        {
            long size = file.Length;
            
            var fileName = $@"{DateTime.Now.Ticks}.";

            if (file.Length > 0)
            {
                var filePath = Path.GetTempFileName();

                using (var stream = System.IO.File.Create(filePath))
                {
                    
                    await file.CopyToAsync(stream);

                    var img = Image.FromStream(stream);
                    
                    ImageFormat frmt;
                    if (ImageFormat.Png.Equals(img.RawFormat))
                    {
                        fileName += "png";
                        frmt = ImageFormat.Png;
                    }
                    else
                    {
                        fileName += "jpg";
                        frmt = ImageFormat.Jpeg;
                    }
                    string path = "C:/Users/maria/OneDrive/Documents/images/" + fileName;
                    img.Save(path, frmt);
                }
            }
            return Ok(new {size, fileName});
        }
        
        /// <summary>
        /// Get an uploaded Image
        /// </summary>
        /// <param name="fileName">Image name</param>
        /// <returns>File</returns>
        [HttpGet("{fileName}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetUpload(string fileName)
        {
            var path = "C:/Users/maria/OneDrive/Documents/images/" + fileName;
            
            if (!System.IO.File.Exists(path))
            {
                return BadRequest();
            }
            
            Byte[] b = await System.IO.File.ReadAllBytesAsync(path);           
            return File(b, "image/jpeg");
        }
        
        /// <summary>
        /// Delete an image
        /// </summary>
        /// <param name="fileName">Image name</param>
        [HttpDelete("{fileName}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUpload(string fileName)
        {
            var path = "C:/Users/maria/OneDrive/Documents/images/" + fileName;
            
            if (!System.IO.File.Exists(path))
            {
                return BadRequest();
            }
            System.IO.File.Delete(path);
            return Ok(new {fileName});
        }
    }
}