using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Storage.Tables;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Game>> CreateGame([FromBody] GameCreateRequest request)
        {
            int.TryParse(((ClaimsIdentity) User.Identity).Claims.FirstOrDefault()?.Value, out int userId);
            var game = await _gameService.CreateGame(request, userId);
            return game;
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteGame(int id)
        {
            await _gameService.DisbandGame(id);
            return Ok();
        }
    }
}
