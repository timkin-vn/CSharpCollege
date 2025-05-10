using System;
using FifteenGame.Business.Models;

namespace FifteenGame.Business.Services
{
    public class TicTacToeService
    {
        private readonly TicTacToeModel _model;

        public TicTacToeService()
        {
            _model = new TicTacToeModel();
        }

        public TicTacToeModel Model => _model;

        public bool MakeMove(int position)
        {
            return _model.MakeMove(position);
        }

        public void Reset()
        {
            _model.Reset();
        }
    }
} 