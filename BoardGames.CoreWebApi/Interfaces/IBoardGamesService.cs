using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGames.CoreWebApi.EF;

namespace BoardGames.CoreWebApi.Interfaces
{
    public interface IBoardGamesService
    {
        Task<BoardGame> GetBoardGameByIdAsync(int id);
        Task<IEnumerable<BoardGame>> GetBoardGamesAsync();
        Task CreateAsync(BoardGame boardGame);
    }
}