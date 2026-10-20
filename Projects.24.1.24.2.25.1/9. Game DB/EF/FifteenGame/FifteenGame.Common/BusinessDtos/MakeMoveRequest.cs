namespace FifteenGame.Common.BusinessDtos
{
    public class MakeMoveRequest
    {
        public int UserId { get; set; }

        public int RowDelta { get; set; }

        public int ColumnDelta { get; set; }
    }
}
