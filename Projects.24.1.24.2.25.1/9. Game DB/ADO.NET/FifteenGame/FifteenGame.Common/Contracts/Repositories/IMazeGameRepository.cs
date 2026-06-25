using FifteenGame.Common.BusinessModels;
using System.Collections.Generic;

namespace FifteenGame.Common.Contracts.Repositories
{
    public interface IMazeGameRepository
    {
        MazeGameModel GetByUserId(int userId);
        MazeGameModel Save(MazeGameModel mazeGameModel);
        void Remove(int gameId);
    }
}
