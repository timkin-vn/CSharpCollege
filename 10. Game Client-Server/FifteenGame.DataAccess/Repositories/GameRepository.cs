using FifteenGame.Common.Dto;
using FifteenGame.Common.Enums;
using FifteenGame.DataAccess.DataContext;
using FifteenGame.DataAccess.Entities;
using Newtonsoft.Json;
using System.Linq;

namespace FifteenGame.DataAccess.Repositories
{
    public class GameRepository
    {
        public GameDto Get(int id)
        {
            using (var db = new GameDbContext())
            {
                var entity = db.Games.FirstOrDefault(g => g.Id == id);
                if (entity == null) return null;
                return MapToDto(entity);
            }
        }

        public GameDto Save(GameDto dto)
        {
            using (var db = new GameDbContext())
            {
                GameEntity entity;
                if (dto.Id == 0)
                {
                    entity = new GameEntity();
                    db.Games.Add(entity);
                }
                else
                {
                    entity = db.Games.FirstOrDefault(g => g.Id == dto.Id);
                }

                entity.UserId = dto.UserId;
                entity.Score = dto.Score;
                entity.MovesLeft = dto.MovesLeft;
                entity.Mode = (int)dto.Mode;
                entity.State = (int)dto.State;

                var state = new
                {
                    Gems = dto.Gems,
                    Matched = dto.Matched,
                    SelR = dto.SelectedRow,
                    SelC = dto.SelectedColumn
                };
                entity.BoardJson = JsonConvert.SerializeObject(state);

                db.SaveChanges();
                dto.Id = entity.Id;
                return dto;
            }
        }

        private GameDto MapToDto(GameEntity e)
        {
            var state = JsonConvert.DeserializeObject<dynamic>(e.BoardJson);
            return new GameDto
            {
                Id = e.Id,
                UserId = e.UserId,
                Score = e.Score,
                MovesLeft = e.MovesLeft,
                Mode = (GameMode)e.Mode,
                State = (GameState)e.State,
                Gems = state.Gems.ToObject<int[]>(),
                Matched = state.Matched.ToObject<bool[]>(),
                SelectedRow = state.SelR,
                SelectedColumn = state.SelC
            };
        }
    }
}