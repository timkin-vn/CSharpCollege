using BlackjackMVC.Data;
using BlackjackMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;


namespace BlackjackMVC.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController(ApplicationDbContext context)
        {
            _context = context;
        }
        private const string SessionKey = "GAME";

        private GameState GetGame()
        {
            var json = HttpContext.Session.GetString(SessionKey);

            if (json == null)
            {
                return NewGame();
            }

            return JsonSerializer.Deserialize<GameState>(json);
        }

        private Player GetCurrentPlayer()
        {
            var player = _context.Players
                .FirstOrDefault(x => x.UserName == "Игрок");

            if (player == null)
            {
                player = new Player
                {
                    UserName = "Игрок",
                    Wins = 0,
                    Losses = 0
                };

                _context.Players.Add(player);
                _context.SaveChanges();
            }

            return player;
        }

        private void SaveGame(GameState game)
        {
            HttpContext.Session.SetString(
                SessionKey,
                JsonSerializer.Serialize(game));
        }

        private GameState NewGame()
        {
            Deck deck = new Deck();

            GameState game = new GameState();

            game.PlayerCards.Add(deck.Draw());
            game.PlayerCards.Add(deck.Draw());

            game.DealerCards.Add(deck.Draw());
            game.DealerCards.Add(deck.Draw());

            SaveGame(game);

            return game;
        }
        public IActionResult TestDb()
        {
            var tables = _context.Database
                .SqlQueryRaw<string>(
                    @"SELECT tablename
              FROM pg_tables
              WHERE schemaname='public'")
                .ToList();

            return Content(string.Join(", ", tables));
        }
        private int GetPoints(List<Card> cards)
        {
            int total = cards.Sum(c => c.Value);

            int aces = cards.Count(c => c.Rank == "A");

            while (total > 21 && aces > 0)
            {
                total -= 10;
                aces--;
            }

            return total;
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

            Deck deck = new Deck();

            game.PlayerCards.Add(deck.Draw());

            int playerScore = GetPoints(game.PlayerCards);

            if (playerScore > 21)
            {
                game.Message = "Перебор! Вы проиграли.";

                var playerDb = GetCurrentPlayer();

                playerDb.Losses++;

                _context.SaveChanges();
            }

            SaveGame(game);

            return RedirectToAction("Index");
        }

        public IActionResult Stand()
        {
            var game = GetGame();

            var playerDb = GetCurrentPlayer();

            Deck deck = new Deck();

            while (GetPoints(game.DealerCards) < 17)
            {
                game.DealerCards.Add(deck.Draw());
            }

            int dealer = GetPoints(game.DealerCards);
            int player = GetPoints(game.PlayerCards);

            if (dealer > 21)
            {
                game.Message = "Дилер проиграл. Вы победили!";
                playerDb.Wins++;
            }
            else if (player > dealer)
            {
                game.Message = "Вы победили!";
                playerDb.Wins++;
            }
            else if (player < dealer)
            {
                game.Message = "Вы проиграли!";
                playerDb.Losses++;
            }
            else
            {
                game.Message = "Ничья!";
            }

            _context.SaveChanges();

            SaveGame(game);

            return RedirectToAction("Index");
        }

        public int Score(List<Card> cards)
        {
            return GetPoints(cards);
        }
    }
}