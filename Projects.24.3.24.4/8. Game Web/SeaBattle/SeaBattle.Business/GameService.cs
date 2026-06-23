namespace SeaBattle.Business
{
    public class GameService
    {
        public BattleshipGame NewGame()
        {
            var game = new BattleshipGame();
            GameLogic.PlaceFleet(game.PlayerBoard);
            GameLogic.PlaceFleet(game.EnemyBoard);
            return game;
        }

        public void Fire(BattleshipGame game, int row, int col)
        {
            if (game == null || game.Finished) return;

            ShotResult result = game.EnemyBoard.Shoot(row, col);
            if (result == ShotResult.Invalid) return;

            game.MoveCount++;

            if (game.EnemyBoard.AllSunk())
            {
                game.Finished = true;
                game.Won = true;
                return;
            }

            if (result == ShotResult.Miss)
            {
                ShotResult aiResult;
                do
                {
                    int ar, ac;
                    aiResult = game.Ai.Fire(game.PlayerBoard, out ar, out ac);
                    if (game.PlayerBoard.AllSunk())
                    {
                        game.Finished = true;
                        game.Won = false;
                        return;
                    }
                }
                while (aiResult == ShotResult.Hit || aiResult == ShotResult.Sunk);
            }
        }
    }
}
