using BuissnesLayaer.Interfaces;
using DataLayer;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuissnesLayaer.Implementations
{
    public class EFGame : IGameRep
    {
        private EFDBContext context;

        public EFGame(EFDBContext context)
        {
            this.context = context;
        }

        public void SaveGame(Game game)
        {
            if (game.GameId == 0)
            {
                context.Game.Add(game);
            }
            else
            {
                context.Entry(game).State = Microsoft.EntityFrameworkCore.EntityState.Modified;             
            }

			context.SaveChanges();
		}

        public void DeleteGame(Game game)
        {
            context.Game.Remove(game);
            context.SaveChanges();
        }

        public IEnumerable<Game> GetAllGames()
        {
            return context.Game.ToList();
        }

        public Game GetGameById(int gameId)
        {
            return context.Game.FirstOrDefault(x => x.GameId == gameId);
        }
    }
}
