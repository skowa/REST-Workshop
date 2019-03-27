using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoardGames.CoreWebApi.EF;
using BoardGames.CoreWebApi.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BoardGames.CoreWebApi.Services
{
    public class BoardGamesService : IBoardGamesService
    {
        private readonly DbContext _context;

        public BoardGamesService(DbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<BoardGame> GetBoardGameByIdAsync(int id)
        {
            return await _context.Set<BoardGame>().SingleOrDefaultAsync(game => game.Id == id).ConfigureAwait(false);
        }

        public async Task<IEnumerable<BoardGame>> GetBoardGamesAsync()
        {
            return await _context.Set<BoardGame>().ToListAsync().ConfigureAwait(false);
        }

        public async Task CreateAsync(BoardGame boardGame)
        {
            if (boardGame == null)
            {
                throw new ArgumentNullException(nameof(boardGame));
            }

            await _context.Set<BoardGame>().AddAsync(boardGame).ConfigureAwait(false);
            await _context.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}