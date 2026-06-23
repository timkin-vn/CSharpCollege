using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Business.Models
{
    public class Checker
    {
        public Checker(CheckerColor color, int row, int col, bool isQueen = false)
        {
            Color = color;
            IsQueen = isQueen;
            Row = row;
            Col = col;
        }

        public CheckerColor Color { get; set; }
        public bool IsQueen { get; set; }
        public int Row { get; set; }
        public int Col { get; set; }

        public Checker Clone()
        {
            return new Checker(Color, Row, Col, IsQueen);
        }

    }
}
