using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Business.Models
{
    public class Board
    {
        public const int Size = 8;
        private Checker[,] _cells;
        public Board()
        {
            _cells = new Checker[Size, Size];
            InitializeBoard();
        }

        private void InitializeBoard()
        {
            for (int i = 0; i < Size; i++)
                for (int j = 0; j < Size; j++)
                    _cells[i, j] = null;
            for (int i = 0; i < 3; i++)
            {
                for(int j = 0;j < Size; j++)
                {
                    if((i + j) % 2 == 1)
                    {
                        _cells[i, j] = new Checker(CheckerColor.White, i, j);
                    }
                }
            }
            for(int i = 5; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    if ((i + j) % 2 == 1)
                    {
                        _cells[i, j] = new Checker(CheckerColor.Black, i, j);
                    }
                }
            }
        }
        public Checker GetChecker(int row, int col)
        {
            if(row >= 0 && row < Size && col >= 0 && col < Size) return _cells[row, col];
            return null;
        }
        public void SetChecker(int row, int col, Checker checker)
        {
            _cells[row, col] = checker;
            if(checker != null)
            {
                checker.Row = row;
                checker.Col = col;
            }
        }
        public void RemoveChecker(int row, int col)
        {
            _cells[row, col] = null;
        }
        public bool IsValidCell(int row, int col) => row >= 0 && row < Size && col >= 0 && col < Size;
        public bool IsDarkSquare(int row, int col) => (row +  col) % 2 == 1;
        public Board Clone()
        {
            Board newBoard = new Board();
            for(int i = 0; i < Size; i++)
            {
                for(int j = 0; j < Size; j++)
                {
                    if(_cells[i, j] != null) newBoard.SetChecker(i, j, _cells[i, j].Clone());
                }
            }
            return newBoard;
        }
        public List<Checker> GetAllCheckers()
        {
            var checkers = new List<Checker>();
            for(int i = 0;i < Size;i++)
                for(int j = 0;j < Size;j++)
                    if(_cells[i,j] != null) checkers.Add(_cells[i, j]);
            return checkers;
        }
    }
}
