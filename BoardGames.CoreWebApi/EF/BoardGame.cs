using System;
using System.Collections.Generic;

namespace BoardGames.CoreWebApi.EF
{
    public partial class BoardGame
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int MinPlayersNumber { get; set; }
        public int MaxPlayersNumber { get; set; }
        public string Description { get; set; }
    }
}
