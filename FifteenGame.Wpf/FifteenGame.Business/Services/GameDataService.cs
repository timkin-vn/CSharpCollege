using System;
using System.Diagnostics;
using System.Linq;
using Pacman.Business.Models; // Используется для GameStateData
using PacmanGame.DataAccess.Entities;
using PacmanGame.DataAccess.UnitOfWork;
namespace Pacman.Business.Services
{
    public class GameDataService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameDataService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // 1. АУТЕНТИФИКАЦИЯ (Возвращает Id)
        
        public int Authenticate(string username, string password)
        {
            Debug.WriteLine($"=== AUTHENTICATE ===");
            Debug.WriteLine($"Username: {username}");

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return 0;

            try
            {
                var user = _unitOfWork.Users
                                 .GetByUsername(username);

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
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при авторизации: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetails += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
                }

                Debug.WriteLine(errorDetails);
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
                if (_unitOfWork.Users.UsernameExists(username))
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

                _unitOfWork.Users.Add(newUser);
                _unitOfWork.SaveChanges();

                Debug.WriteLine("✅ Регистрация прошла успешно (в БД)");
                return true;
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при регистрации: {ex.Message}";
                if (ex.InnerException != null)
                {
                    errorDetails += $"\nВнутренняя ошибка: {ex.InnerException.Message}";
                }

                Debug.WriteLine(errorDetails);

                return false;
            }
        }


        
        // 3. СОХРАНЕНИЕ ПРОГРЕССА
        
        public void SaveGameState(int userId, GameStateData currentGameState)
        {
            Debug.WriteLine($"=== SAVE GAME STATE for UserID: {userId} ===");
            try
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

                _unitOfWork.GameStates.Add(newSave);
                _unitOfWork.SaveChanges();

                    Debug.WriteLine($"✅ Игра сохранена. Счет: {currentGameState.Score}");
            }
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при сохранении: {ex.Message}";
                Debug.WriteLine(errorDetails);
            }
        }

        
        // 4. ЗАГРУЗКА ПОСЛЕДНЕГО ПРОГРЕССА
        
        public GameStateData LoadGameState(int userId)
        {
            try
            {
                var latestSave = _unitOfWork.GameStates
                                         .GetLastGameStateByUserId(userId);

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
            catch (Exception ex)
            {
                string errorDetails = $"Критическая ошибка БД при загрузке игры: {ex.Message}";
                Debug.WriteLine(errorDetails);
                return null;
            }
        }
    }
}