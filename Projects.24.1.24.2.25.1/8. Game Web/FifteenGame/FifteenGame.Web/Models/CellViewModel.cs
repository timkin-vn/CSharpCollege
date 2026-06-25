using System;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public bool IsOpened { get; set; }

        public bool IsFlagged { get; set; }

        public bool IsMine { get; set; }

        public int MinesAround { get; set; }

        public bool IsGameFinished { get; set; }

        public string Text
        {
            get
            {
                if (IsOpened && IsMine)
                {
                    return "*";
                }

                if (IsFlagged && !IsOpened)
                {
                    return "F";
                }

                if (!IsOpened)
                {
                    return string.Empty;
                }

                return MinesAround == 0 ? string.Empty : MinesAround.ToString();
            }
        }

        public string CssClass
        {
            get
            {
                if (IsOpened && IsMine)
                {
                    return "mine-cell";
                }

                if (IsOpened)
                {
                    return "opened-cell";
                }

                if (IsFlagged)
                {
                    return "flagged-cell";
                }

                return "closed-cell";
            }
        }
    }
}
