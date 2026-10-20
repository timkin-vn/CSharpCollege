using FifteenGame.Core.BusinessLogic.Models;

namespace FifteenGame.Common.Contracts.Services
{
    /// <summary>Игра «Ключи и монстры» с сохранением состояния для каждого пользователя.</summary>
    public interface IGameService
    {
        /// <summary>Текущая игра пользователя. Если её нет или она закончена — создаётся новая.</summary>
        GameSnapshot GetByUserId(int userId);

        /// <summary>Начинает новую игру (старая игра пользователя заменяется).</summary>
        GameSnapshot NewGame(int userId);

        /// <summary>Делает ход игрока. Ход на стену или после конца игры ничего не меняет.</summary>
        GameSnapshot MakeMove(int userId, int dRow, int dColumn);
    }
}
