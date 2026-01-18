using EdhWreck.Biz.Abstractions;
using EdhWreck.Biz.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EdhWreck.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IScryfallApiService _scryfallApiService;
        public CardController(IScryfallApiService scryfallApiService)
        {
            _scryfallApiService = scryfallApiService;
        }

        [HttpPost("search")]
        public async Task<IActionResult> GetCardSearchAsync(CardRequest request)
        {
            try
            {
                var response = await _scryfallApiService.CardSearchAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return new StatusCodeResult((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
