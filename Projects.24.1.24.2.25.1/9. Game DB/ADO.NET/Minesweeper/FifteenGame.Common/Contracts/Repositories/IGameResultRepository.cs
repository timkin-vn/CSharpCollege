using FifteenGame.Common.Dtos;

namespace FifteenGame.Common.Contracts.Repositories;

public interface IGameResultRepository
{
    void Initialize();
    void Save(GameResultDto result);
    IReadOnlyList<GameResultDto> GetTopResults();
}
