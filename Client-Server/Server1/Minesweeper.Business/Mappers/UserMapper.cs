using Minesweeper.Common.BusinessDtos;
using Minesweeper.Common.BusinessModels;
using Minesweeper.Common.Dto;

namespace Minesweeper.Business.Mappers
{
    public static class UserMapper
    {
        public static UserResponse ToResponse(UserModel model)
        {
            if (model == null) return null;

            return new UserResponse
            {
                Id = model.Id,
                Username = model.Username,
                CreatedAt = model.CreatedAt,
                TotalGamesPlayed = model.TotalGamesPlayed,
                GamesWon = model.GamesWon,
                WinRate = model.WinRate,
                LastPlayedAt = model.CreatedAt 
            };
        }

        public static UserModel ToModel(UserDto dto)
        {
            if (dto == null) return null;

            return new UserModel
            {
                Id = dto.Id,
                Username = dto.Username,
                CreatedAt = dto.CreatedAt,
                TotalGamesPlayed = dto.TotalGamesPlayed,
                GamesWon = dto.GamesWon
            };
        }

        public static UserDto ToDto(UserModel model)
        {
            if (model == null) return null;

            return new UserDto
            {
                Id = model.Id,
                Username = model.Username,
                CreatedAt = model.CreatedAt,
                TotalGamesPlayed = model.TotalGamesPlayed,
                GamesWon = model.GamesWon
            };
        }
    }
}