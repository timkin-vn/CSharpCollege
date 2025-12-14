using PacmanGame.DataAccess;
using PacmanGame.DataAccess.Entities;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Data.Entity;
using StepByStepPacman.Business.Models; // Используется для GameStateData

namespace Pacman.Business.Services
{
    public class GameDataService
    {
        
        // 1. АУТЕНТИФИКАЦИЯ (Возвращает Id)
        
        public int Authenticate(string username, string password)
        {
            Debug.WriteLine($"=== AUTHENTICATE ===");
            Debug.WriteLine($"Username: {username}");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return 0;

            try
            {
                using (var context = new PacmanDbContext())
                {
                    var user = context.GameUsers
                                     .FirstOrDefault(u => u.Username.ToLower() == username.ToLower());

                    if (user == null)
                    {
                        Debug.WriteLine("❌ Пользователь не найден.");
                        return 0;
                    }

                    if (user.Password == password)
                    {
                        Debug.WriteLine($"✅ Аутентификация успешна. ID: {user.Id}");
                        return user.Id;
                    }
                    else
                    {
                        Debug.WriteLine("❌ Неверный пароль.");
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при авторизации: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetails += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
                }

                Debug.WriteLine(errorDetails);
                MessageBox.Show(errorDetails, "КРИТИЧЕСКАЯ ОШИБКА БАЗЫ ДАННЫХ", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
        }

        
        // 2. РЕГИСТРАЦИЯ
        
        public bool Register(string username, string password)
        {
            Debug.WriteLine($"=== REGISTER ===");

            if (string.IsNullOrEmpty(username) || username.Length < 3 || string.IsNullOrEmpty(password) || password.Length < 3)
            {
                Debug.WriteLine("❌ Слишком короткое имя пользователя или пароль");
                return false;
            }

            try
            {
                using (var context = new PacmanDbContext())
                {
                    if (context.GameUsers.Any(u => u.Username.ToLower() == username.ToLower()))
                    {
                        Debug.WriteLine("❌ Пользователь с таким логином уже существует в БД.");
                        return false;
                    }

                    var newUser = new GameUserEntity
                    {
                        Username = username,
                        Password = password,
                        CreatedAt = DateTime.Now
                    };

                    context.GameUsers.Add(newUser);
                    context.SaveChanges();

                    Debug.WriteLine("✅ Регистрация прошла успешно (в БД)");
                    return true;
                }
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при регистрации: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetails += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
                }

                Debug.WriteLine(errorDetails);
                MessageBox.Show(errorDetails, "КРИТИЧЕСКАЯ ОШИБКА БАЗЫ ДАННЫХ", MessageBoxButton.OK, MessageBoxImage.Error);

                return false;
            }
        }


        
        // 3. СОХРАНЕНИЕ ПРОГРЕССА
        
        public void SaveGameState(int userId, GameStateData currentGameState)
        {
            Debug.WriteLine($"=== SAVE GAME STATE for UserID: {userId} ===");
            try
            {
                using (var context = new PacmanDbContext())
                {
                    var newSave = new GameStateEntity
                    {
                        GameUserId = userId,
                        Score = currentGameState.Score,
                        Level = currentGameState.Level,
                        Lives = currentGameState.Lives,
                        PlayerX = currentGameState.PlayerX,
                        PlayerY = currentGameState.PlayerY,
                        BoardState = currentGameState.BoardState,
                        GhostsPositions = currentGameState.GhostsPositions,

                        
                        CreatedAt = DateTime.Now
                    };

                    context.GameStates.Add(newSave);
                    context.SaveChanges();

                    Debug.WriteLine($"✅ Игра сохранена. Счет: {currentGameState.Score}");
                }
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при сохранении: {ex.Message}";
                Debug.WriteLine(errorDetails);
                MessageBox.Show(errorDetails, "КРИТИЧЕСКАЯ ОШИБКА СОХРАНЕНИЯ", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        
        // 4. ЗАГРУЗКА ПОСЛЕДНЕГО ПРОГРЕССА
        
        public GameStateData LoadGameState(int userId)
        {
            try
            {
                using (var context = new PacmanDbContext())
                {
                    var latestSave = context.GameStates
                                             .Where(g => g.GameUserId == userId)
                                             .OrderByDescending(g => g.CreatedAt)
                                             .FirstOrDefault();

                    if (latestSave == null)
                    {
                        Debug.WriteLine("❌ Нет сохраненных игр для этого пользователя.");
                        return null;
                    }

                    var state = new GameStateData
                    {
                        Score = latestSave.Score,
                        Level = latestSave.Level,
                        Lives = latestSave.Lives,
                        PlayerX = latestSave.PlayerX,
                        PlayerY = latestSave.PlayerY,
                        BoardState = latestSave.BoardState,
                        GhostsPositions = latestSave.GhostsPositions,

                        
                        CreatedAt = latestSave.CreatedAt
                    };

                    Debug.WriteLine($"✅ Загружен прогресс: Score={state.Score}, Level={state.Level}");
                    return state;
                }
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при загрузке игры: {ex.Message}";
                Debug.WriteLine(errorDetails);
                MessageBox.Show(errorDetails, "КРИТИЧЕСКАЯ ОШИБКА ЗАГРУЗКИ", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }
        }
    }
}