namespace FifteenGame.Common.BusinessDtos
{
    /// <summary>Состояние партии в шашки, передаваемое с сервера клиенту.</summary>
    public class CheckersGameReply
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int MoveCount { get; set; }

        /// <summary>0 - белые, 1 - чёрные (как в CheckersGame.Business.Models.Player).</summary>
        public int CurrentPlayer { get; set; }

        public bool IsCaptureInProgress { get; set; }

        public int CaptureRow { get; set; }

        public int CaptureCol { get; set; }

        public bool IsGameOver { get; set; }

        /// <summary>Доска 8x8, построчно (64 значения), значение - номер Piece.</summary>
        public int[] Cells { get; set; }

        /// <summary>Уже побитые в текущей серии клетки: row * 8 + col.</summary>
        public int[] CapturedCells { get; set; }
    }
}
