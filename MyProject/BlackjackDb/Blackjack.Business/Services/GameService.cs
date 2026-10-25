using Blackjack.Common.Contracts.Repositories;
using Blackjack.Common.Contracts.Services;
using Blackjack.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack.Business.Services
{
    public class GameService : IGameService
    {
        private const int HistorySize = 10;

        private readonly IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public GameModel GetCurrentGame(int userId)
        {
            return _gameRepository.GetUnfinished(userId) ?? StartNewGame(userId);
        }

        public GameModel StartNewGame(int userId)
        {
            // Брошенная незаконченная партия засчитывается как поражение,
            // иначе можно пересдавать карты, пока не придёт хорошая рука
            var unfinished = _gameRepository.GetUnfinished(userId);
            if (unfinished != null)
            {
                unfinished.Status = GameStatus.DealerWin;
                unfinished.Message = "Партия брошена.";
                _gameRepository.Save(unfinished);
            }

            var game = new GameModel
            {
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Deck = DeckFactory.CreateShuffled(),
                Message = "Ваш ход",
            };

            game.PlayerCards.Add(Draw(game));
            game.DealerCards.Add(Draw(game));
            game.PlayerCards.Add(Draw(game));
            game.DealerCards.Add(Draw(game));

            CheckInitialBlackjack(game);

            game.Id = _gameRepository.Save(game);
            return game;
        }

        public GameModel Hit(int gameId)
        {
            var game = GetGame(gameId);
            if (game.IsFinished)
            {
                return game;
            }

            game.PlayerCards.Add(Draw(game));

            if (game.PlayerScore > 21)
            {
                Finish(game, GameStatus.DealerWin, "Перебор! Вы проиграли.");
            }
            else if (game.PlayerScore == 21)
            {
                DealerTurn(game);
            }

            _gameRepository.Save(game);
            return game;
        }

        public GameModel Stand(int gameId)
        {
            var game = GetGame(gameId);
            if (game.IsFinished)
            {
                return game;
            }

            DealerTurn(game);

            _gameRepository.Save(game);
            return game;
        }

        public UserStatsModel GetStats(int userId)
        {
            var games = _gameRepository.GetFinished(userId);
            return new UserStatsModel
            {
                Wins = games.Count(g => g.Status == GameStatus.PlayerWin),
                Losses = games.Count(g => g.Status == GameStatus.DealerWin),
                Draws = games.Count(g => g.Status == GameStatus.Draw),
            };
        }

        public List<GameHistoryItem> GetHistory(int userId)
        {
            return _gameRepository.GetFinished(userId)
                .OrderByDescending(g => g.CreatedAt)
                .ThenByDescending(g => g.Id)
                .Take(HistorySize)
                .Select(g => new GameHistoryItem
                {
                    Id = g.Id,
                    CreatedAt = g.CreatedAt,
                    Status = g.Status,
                    PlayerScore = g.PlayerScore,
                    DealerScore = g.DealerScore,
                })
                .ToList();
        }

        private GameModel GetGame(int gameId)
        {
            var game = _gameRepository.GetById(gameId);
            if (game == null)
            {
                throw new KeyNotFoundException($"Игра с номером {gameId} не найдена.");
            }
            return game;
        }

        private static Card Draw(GameModel game)
        {
            if (game.Deck.Count == 0)
            {
                throw new InvalidOperationException("Колода пуста!");
            }

            var card = game.Deck[0];
            game.Deck.RemoveAt(0);
            return card;
        }

        private static void CheckInitialBlackjack(GameModel game)
        {
            int player = game.PlayerScore;
            int dealer = game.DealerScore;

            if (player == 21 && dealer == 21)
            {
                Finish(game, GameStatus.Draw, "У обоих блэкджек. Ничья.");
            }
            else if (player == 21)
            {
                Finish(game, GameStatus.PlayerWin, "Блэкджек! Вы выиграли.");
            }
            else if (dealer == 21)
            {
                Finish(game, GameStatus.DealerWin, "У дилера блэкджек. Вы проиграли.");
            }
        }

        // Дилер берёт карты, пока у него меньше 17, затем определяется победитель
        private static void DealerTurn(GameModel game)
        {
            while (game.DealerScore < 17)
            {
                game.DealerCards.Add(Draw(game));
            }

            int player = game.PlayerScore;
            int dealer = game.DealerScore;

            if (dealer > 21)
            {
                Finish(game, GameStatus.PlayerWin, "Дилер перебрал. Вы выиграли!");
            }
            else if (player > dealer)
            {
                Finish(game, GameStatus.PlayerWin, "Вы выиграли!");
            }
            else if (player < dealer)
            {
                Finish(game, GameStatus.DealerWin, "Дилер выиграл.");
            }
            else
            {
                Finish(game, GameStatus.Draw, "Ничья.");
            }
        }

        private static void Finish(GameModel game, GameStatus status, string message)
        {
            game.Status = status;
            game.Message = message;
        }
    }
}
