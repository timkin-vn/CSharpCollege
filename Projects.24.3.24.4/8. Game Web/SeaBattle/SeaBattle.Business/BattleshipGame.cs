namespace SeaBattle.Business
{
    public class BattleshipGame
    {
        public Board PlayerBoard { get; set; }
        public Board EnemyBoard { get; set; }
        public AiPlayer Ai { get; set; }
        public int MoveCount { get; set; }
        public bool Finished { get; set; }
        public bool Won { get; set; }

        public BattleshipGame()
        {
            PlayerBoard = new Board();
            EnemyBoard = new Board();
            Ai = new AiPlayer();
        }
    }
}
