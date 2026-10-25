using BlackjackMVC.Data;
using BlackjackMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BlackjackMVC.Controllers
{
    public class GameController : Controller
    {
        private const string SessionKey = "GAME";
        private const string PlayerName = "Игрок";

        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Player = GetCurrentPlayer();
            return View(GetGame());
        }

        public IActionResult New()
        {
            NewGame();
            return RedirectToAction("Index");
        }

        public IActionResult Hit()
        {
            var game = GetGame();

            // После окончания партии брать карты нельзя
            if (game.IsOver)
            {
                return RedirectToAction("Index");
            }

            game.PlayerCards.Add(Deck.Draw(game.DeckCards));

            int playerScore = game.PlayerPoints;
            if (playerScore > 21)
            {
                Finish(game, "Перебор! Вы проиграли.", isWin: false);
            }
            else if (playerScore == 21)
            {
                // 21 очко — дальше брать нет смысла, ход переходит к дилеру
                DealerTurn(game);
            }

            SaveGame(game);
            return RedirectToAction("Index");
        }

        public IActionResult Stand()
        {
            var game = GetGame();

            // Повторное нажатие "Хватит" не должно второй раз менять статистику
            if (game.IsOver)
            {
                return RedirectToAction("Index");
            }

            DealerTurn(game);

            SaveGame(game);
            return RedirectToAction("Index");
        }

        private void DealerTurn(GameState game)
        {
            while (game.DealerPoints < 17)
            {
                game.DealerCards.Add(Deck.Draw(game.DeckCards));
            }

            int dealer = game.DealerPoints;
            int player = game.PlayerPoints;

            if (dealer > 21)
            {
                Finish(game, "Дилер перебрал. Вы победили!", isWin: true);
            }
            else if (player > dealer)
            {
                Finish(game, "Вы победили!", isWin: true);
            }
            else if (player < dealer)
            {
                Finish(game, "Вы проиграли!", isWin: false);
            }
            else
            {
                Finish(game, "Ничья!", isWin: null);
            }
        }

        // isWin: true — победа, false — поражение, null — ничья
        private void Finish(GameState game, string message, bool? isWin)
        {
            game.Message = message;
            game.IsOver = true;

            if (isWin.HasValue)
            {
                var playerDb = GetCurrentPlayer();
                if (isWin.Value)
                {
                    playerDb.Wins++;
                }
                else
                {
                    playerDb.Losses++;
                }
                _context.SaveChanges();
            }
        }

        private GameState GetGame()
        {
            var json = HttpContext.Session.GetString(SessionKey);
            if (json == null)
            {
                return NewGame();
            }

            return JsonSerializer.Deserialize<GameState>(json) ?? NewGame();
        }

        private GameState NewGame()
        {
            var game = new GameState
            {
                DeckCards = Deck.CreateCards(),
                Message = "Ваш ход",
            };

            game.PlayerCards.Add(Deck.Draw(game.DeckCards));
            game.DealerCards.Add(Deck.Draw(game.DeckCards));
            game.PlayerCards.Add(Deck.Draw(game.DeckCards));
            game.DealerCards.Add(Deck.Draw(game.DeckCards));

            // Блэкджек с раздачи
            int player = game.PlayerPoints;
            int dealer = game.DealerPoints;
            if (player == 21 && dealer == 21)
            {
                Finish(game, "У обоих блэкджек. Ничья!", isWin: null);
            }
            else if (player == 21)
            {
                Finish(game, "Блэкджек! Вы победили!", isWin: true);
            }
            else if (dealer == 21)
            {
                Finish(game, "У дилера блэкджек. Вы проиграли!", isWin: false);
            }

            SaveGame(game);
            return game;
        }

        private void SaveGame(GameState game)
        {
            HttpContext.Session.SetString(SessionKey, JsonSerializer.Serialize(game));
        }

        private Player GetCurrentPlayer()
        {
            var player = _context.Players.FirstOrDefault(x => x.UserName == PlayerName);

            if (player == null)
            {
                player = new Player
                {
                    UserName = PlayerName,
                    Wins = 0,
                    Losses = 0
                };

                _context.Players.Add(player);
                _context.SaveChanges();
            }

            return player;
        }
    }
}
