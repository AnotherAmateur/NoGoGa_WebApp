using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayaer.Interfaces
{
    public interface IGameRep
    {
        IEnumerable<Game> GetAllGames();
        Game GetGameById(int gameId);
        void SaveGame(Game game);
        void DeleteGame(Game game);
    }
}
