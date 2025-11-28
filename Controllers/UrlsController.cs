using Microsoft.AspNetCore.Mvc;
using System;
using TestShortUrl.Abstarcts;
using TestShortUrl.Mappers;
using TestShortUrl.Models;

namespace TestShortUrl.Controllers
{
    [ApiController]
    [Route("api/Urls")]
    public class UrlsController : ControllerBase
    {
        private readonly IWorker _worker;
        public UrlsController(IWorker worker)
        {
            _worker = worker;
        }
        [HttpPost("create-url")]
        public async Task<ActionResult<CreateShortUrlDTO>> CreateUrl(string oldUrl)
        {
            var url =await _worker.CreateShortUrlAsync(oldUrl);
            return Ok(url);
        }
        [HttpGet("redirect")]
        [ProducesResponseType(StatusCodes.Status302Found)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateUrlResponse>> RedirectToUrl(string url)
        {
            var oldUrl=await _worker.GetOriginalUrl(url);

            return Redirect(oldUrl);
        }
    }
}
