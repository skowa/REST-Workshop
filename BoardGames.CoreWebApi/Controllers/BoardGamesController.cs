using System;
using System.Threading.Tasks;
using BoardGames.CoreWebApi.EF;
using BoardGames.CoreWebApi.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace BoardGames.CoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardGamesController : ControllerBase
    {
        private readonly IBoardGamesService _service;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _options;

        public BoardGamesController(IBoardGamesService service, IMemoryCache cache, MemoryCacheEntryOptions options)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _options = options;
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllBoardGames()
        {
            return this.Ok(await _service.GetBoardGamesAsync());
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetBoardGame(int id)
        {
            if (_cache.TryGetValue(id, out BoardGame boardGame))
            {
                return this.Ok(boardGame);
            }

            boardGame = await _service.GetBoardGameByIdAsync(id);
            if (boardGame == null)
            {
                return NotFound(id);
            }

            _cache.Set(id, boardGame, _options);
            return this.Ok(boardGame);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostBoardGame([FromBody] BoardGame boardGame)
        {
            await _service.CreateAsync(boardGame);

            return this.Ok();
        }
    }
}
