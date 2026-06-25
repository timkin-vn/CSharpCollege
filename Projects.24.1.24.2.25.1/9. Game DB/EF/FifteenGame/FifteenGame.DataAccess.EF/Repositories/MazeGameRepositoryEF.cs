using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.Contracts.Repositories;
using FifteenGame.DataAccess.EF.DataContext;
using FifteenGame.DataAccess.EF.Entities;
using System.Linq;

namespace FifteenGame.DataAccess.EF.Repositories
{
    public class MazeGameRepositoryEF : IMazeGameRepository
    {
        public MazeGameModel GetByUserId(int userId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.MazeGames.FirstOrDefault(g => g.UserId == userId);
                if (game == null) return null;

                return new MazeGameModel
                {
                    Id = game.Id,
                    UserId = game.UserId,
                    SerializedGameManager = game.SerializedGameManager
                };
            }
        }

        public MazeGameModel Save(MazeGameModel mazeGameModel)
        {
            if (mazeGameModel.Id == 0)
            {
                return Create(mazeGameModel);
            }
            return Update(mazeGameModel);
        }

        private MazeGameModel Create(MazeGameModel mazeGameModel)
        {
            using (var context = new FifteenGameDataContext())
            {
                var newGame = new MazeGame
                {
                    UserId = mazeGameModel.UserId,
                    SerializedGameManager = mazeGameModel.SerializedGameManager
                };

                context.MazeGames.Add(newGame);
                context.SaveChanges();

                mazeGameModel.Id = newGame.Id;
                return mazeGameModel;
            }
        }

        private MazeGameModel Update(MazeGameModel mazeGameModel)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.MazeGames.FirstOrDefault(g => g.Id == mazeGameModel.Id);
                if (game != null)
                {
                    game.SerializedGameManager = mazeGameModel.SerializedGameManager;
                    context.SaveChanges();
                }
                return mazeGameModel;
            }
        }

        public void Remove(int gameId)
        {
            using (var context = new FifteenGameDataContext())
            {
                var game = context.MazeGames.FirstOrDefault(g => g.Id == gameId);
                if (game != null)
                {
                    context.MazeGames.Remove(game);
                    context.SaveChanges();
                }
            }
        }
    }
}
