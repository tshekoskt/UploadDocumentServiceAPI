using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace UploadDocumentServiceAPI.Controllers
{

    [ApiController]
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class DocumentController : ControllerBase
    {

        private readonly ILogger<DocumentController> _logger;
        private readonly IBlobService _blobService;

        public DocumentController(IBlobService blobService, ILogger<DocumentController> logger)
        {
            _logger = logger;
            _blobService = blobService;
        }

        [HttpPost("Upload"), DisableRequestSizeLimit]
        public async Task<ActionResult> UploadDocument()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }

            var result = await _blobService.UploadFileBlobAsync(
                    "gdestorage",
                    file.OpenReadStream(),
                    file.ContentType,
                    file.FileName);

            var toReturn = result.AbsoluteUri;

            return Ok(new { path = toReturn });
        }

        //[HttpGet]
        //[Route("download/{fileName}")]
        //public async Task<IActionResult> DownloadAsync(string fileName)
        //{
        //    var fileDto = await _blobService.GetBlobAsync(new GetBlobRequestDto { Name = fileName });

        //    return File(fileDto.Content, "application/octet-stream", fileDto.Name);
        //}

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}
    }
}
