using BlackjackGame.Business.Models;
using System.Collections.Generic;
using System.Linq;

namespace BlackjackGame.Business.Services
{
    public class BlackjackService
    {
        public void StartNewGame(GameState state)
        {
            state.Reset();

            state.PlayerHand.Add(state.Deck.DrawCard());
            state.DealerHand.Add(state.Deck.DrawCard());
            state.PlayerHand.Add(state.Deck.DrawCard());
            state.DealerHand.Add(state.Deck.DrawCard());

            CheckForInitialBlackjack(state);
        }

        private void CheckForInitialBlackjack(GameState state)
        {
            int playerScore = CalculateScore(state.PlayerHand);
            if (playerScore == 21)
            {
                state.Status = GameStatus.PlayerWin;
                state.Message = "Блэкджек! Вы выиграли.";
                state.IsPlayerTurn = false;
            }
        }

        public void PlayerHit(GameState state)
        {
            if (!state.IsPlayerTurn || state.Status != GameStatus.Playing) return;

            state.PlayerHand.Add(state.Deck.DrawCard());
            int score = CalculateScore(state.PlayerHand);

            if (score > 21)
            {
                state.Status = GameStatus.Bust;
                state.Message = "Перебор! Вы проиграли.";
                state.IsPlayerTurn = false;
            }
            else if (score == 21)
            {
                PlayerStand(state);
            }
        }

        public void PlayerStand(GameState state)
        {
            if (!state.IsPlayerTurn || state.Status != GameStatus.Playing) return;

            state.IsPlayerTurn = false;
            state.Message = "Ход дилера...";

            while (CalculateScore(state.DealerHand) < 17)
            {
                state.DealerHand.Add(state.Deck.DrawCard());
            }

            DetermineWinner(state);
        }

        private void DetermineWinner(GameState state)
        {
            int playerScore = CalculateScore(state.PlayerHand);
            int dealerScore = CalculateScore(state.DealerHand);

            if (dealerScore > 21)
            {
                state.Status = GameStatus.PlayerWin;
                state.Message = "Дилер перебрал. Вы выиграли!";
            }
            else if (playerScore > dealerScore)
            {
                state.Status = GameStatus.PlayerWin;
                state.Message = "Вы выиграли!";
            }
            else if (playerScore < dealerScore)
            {
                state.Status = GameStatus.DealerWin;
                state.Message = "Дилер выиграл.";
            }
            else
            {
                state.Status = GameStatus.Draw;
                state.Message = "Ничья.";
            }
        }

        public int CalculateScore(List<Card> hand)
        {
            int score = 0;
            int aces = 0;

            foreach (var card in hand)
            {
                if (card.Rank == Rank.Ace)
                {
                    aces++;
                    score += 11;
                }
                else if (card.Rank >= Rank.Ten)
                {
                    score += 10;
                }
                else
                {
                    score += (int)card.Rank;
                }
            }

            while (score > 21 && aces > 0)
            {
                score -= 10;
                aces--;
            }

            return score;
        }
    }
}