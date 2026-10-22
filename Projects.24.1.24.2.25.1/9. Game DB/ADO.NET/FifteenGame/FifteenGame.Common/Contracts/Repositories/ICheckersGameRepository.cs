using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FifteenGame.Common.Dtos;


namespace FifteenGame.Common.Contracts.Repositories
{
    public interface ICheckersGameRepository
    {
        CheckersGameDto GetById(int gameId);
        IEnumerable<CheckersGameDto> GetByUserId(int userId);
        int Save(CheckersGameDto gameDto);
        void Delete(int gameId);
        void AddMove(CheckersMoveDto move);
    }
}