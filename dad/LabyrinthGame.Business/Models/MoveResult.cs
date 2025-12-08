namespace LabyrinthGame.Business.Models
{
    public class MoveResult
    {
        public bool IsSuccess { get; }
        public bool IsGameCompleted { get; }
        public string Message { get; }

        private MoveResult(bool isSuccess, bool isGameCompleted, string message)
        {
            IsSuccess = isSuccess;
            IsGameCompleted = isGameCompleted;
            Message = message;
        }

        public static MoveResult CreateSuccess(bool isGameCompleted, string message)
        {
            return new MoveResult(true, isGameCompleted, message);
        }

        public static MoveResult CreateFailed(string message)
        {
            return new MoveResult(false, false, message);
        }
    }
}