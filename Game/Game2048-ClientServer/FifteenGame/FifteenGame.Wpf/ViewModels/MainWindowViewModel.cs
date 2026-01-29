using FifteenGame.Common.BusinessModels;
using FifteenGame.Common.BusinessDtos;
using FifteenGame.Common.Dtos;
using FifteenGame.Common.Definitions;
using Ninject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace FifteenGame.Wpf.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private readonly HttpClient _httpClient;
        private GameDto _currentGame;
        private UserModel _user;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<CellViewModel> Cells { get; set; } = new ObservableCollection<CellViewModel>();

        public string UserName => _user?.Name ?? "<нет>";

        public int Score => _currentGame?.Score ?? 0;

        public bool IsGameOver => _currentGame?.IsGameOver ?? false;

        public bool HasWon => _currentGame?.HasWon ?? false;

        public MainWindowViewModel()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:61979/");
        }

        public void SetUser(UserModel user)
        {
            _user = user;
            OnPropertyChanged(nameof(UserName));
            LoadOrCreateGame();
        }

        public async void NewGame()
        {
            try
            {
                
                if (_user == null || _user.Id == 0)
                {
                    System.Windows.MessageBox.Show("Пользователь не инициализирован. Пожалуйста, войдите в систему заново.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var request = new NewGameRequest { UserId = _user.Id };
                var json = System.Text.Json.JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/game/new", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Windows.MessageBox.Show($"Ошибка создания игры: {response.StatusCode}\n{errorContent}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var responseJson = await response.Content.ReadAsStringAsync();
                
                try
                {
                    _currentGame = System.Text.Json.JsonSerializer.Deserialize<GameDto>(responseJson);
                    UpdateUI();
                }
                catch (JsonException jsonEx)
                {
                    System.Windows.MessageBox.Show($"Ошибка десериализации ответа: {jsonEx.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка создания новой игры: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public async void LoadOrCreateGame()
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("LoadOrCreateGame called");
                
                // Try to load existing game
                var request = new UserIdRequest { UserId = _user.Id };
                var json = System.Text.Json.JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/game/get-by-user-id", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    // Если не удалось загрузить, создаем новую игру
                    NewGame();
                    return;
                }
                
                var responseJson = await response.Content.ReadAsStringAsync();
                
                try
                {
                    var games = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<GameDto>>(responseJson);
                    var existingGame = games?.OrderByDescending(g => g.Id).FirstOrDefault();
                    
                    if (existingGame != null)
                    {
                        _currentGame = existingGame;
                    }
                    else
                    {
                        NewGame();
                    }
                }
                catch
                {
                    NewGame();
                }
                
                UpdateUI();
            }
            catch (Exception ex)
            {
                // Если произошла ошибка, создаем новую игру
                System.Windows.MessageBox.Show($"Ошибка загрузки игры: {ex.Message}\nСоздается новая игра.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                NewGame();
            }
        }

        public async void MakeMove(string direction)
        {
            if (_currentGame == null || _currentGame.IsGameOver || _currentGame.HasWon)
                return;

            try
            {
                if (_currentGame.Id == 0)
                {
                    System.Windows.MessageBox.Show("Игра не инициализирована. Пожалуйста, начните новую игру.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var request = new MoveRequest 
                { 
                    GameId = _currentGame.Id, 
                    Direction = direction.ToLower() 
                };
                
                var json = System.Text.Json.JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                var response = await _httpClient.PostAsync("api/game/move", content);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    System.Windows.MessageBox.Show($"Ошибка хода: {response.StatusCode}\n{errorContent}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                
                var responseJson = await response.Content.ReadAsStringAsync();
                
                try
                {
                    _currentGame = System.Text.Json.JsonSerializer.Deserialize<GameDto>(responseJson);
                    UpdateUI();
                }
                catch (JsonException jsonEx)
                {
                    System.Windows.MessageBox.Show($"Ошибка десериализации ответа: {jsonEx.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Ошибка хода: {ex.Message}");
            }
        }

        private void UpdateUI()
        {
            Cells.Clear();
            
            if (_currentGame == null)
            {
                // Создаем пустое поле если игра не инициализирована
                for (int row = 0; row < Constants.RowCount; row++)
                {
                    for (int col = 0; col < Constants.ColumnCount; col++)
                    {
                        Cells.Add(new CellViewModel
                        {
                            Row = row,
                            Column = col,
                            Num = Constants.FreeCellValue,
                        });
                    }
                }
                return;
            }
            
            for (int row = 0; row < Constants.RowCount; row++)
            {
                for (int col = 0; col < Constants.ColumnCount; col++)
                {
                    int index = row * Constants.ColumnCount + col;
                    int value = _currentGame.Cells[index];
                    
                    Cells.Add(new CellViewModel
                    {
                        Row = row,
                        Column = col,
                        Num = value
                    });
                }
            }

            OnPropertyChanged(nameof(Score));
            OnPropertyChanged(nameof(IsGameOver));
            OnPropertyChanged(nameof(HasWon));
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
