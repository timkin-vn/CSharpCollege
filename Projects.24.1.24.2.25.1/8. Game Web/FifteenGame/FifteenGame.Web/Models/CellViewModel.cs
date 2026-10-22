using FifteenGame.Business.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FifteenGame.Web.Models
{
    public class CellViewModel
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public MoveDirection Direction { get; set; }

        public string Symbol
        {
            get
            {
                switch (Direction)
                {
                    case MoveDirection.Miss: return "•";
                    case MoveDirection.Hit: return "✕";
                    case MoveDirection.Destroyed: return "☠";
                    default: return string.Empty;
                }
            }
        }

        public string CssClass
        {
            get
            {
                switch (Direction)
                {
                    case MoveDirection.Ship: return "cell-water";
                    case MoveDirection.Miss: return "cell-miss";
                    case MoveDirection.Hit: return "cell-hit";
                    case MoveDirection.Destroyed: return "cell-destroyed";
                    default: return "cell-water";
                }
            }
        }
    }
}