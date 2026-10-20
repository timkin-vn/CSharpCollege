using System;
using System.Windows;
using System.Windows.Controls;
using System.Net.Http;
using System.Linq;
using TicTacToeGame.Common; // Убедитесь, что ссылка на проект Common добавлена

namespace TicTacToeClient
{
    public partial class MainWindow : Window
    {
        private HttpClient _client = new HttpClient();
        // ВАЖНО: Замените порт 44318 на тот, который отображается у вас в браузере
        private const string ApiUrl = "https://localhost:44318/api/game/move/";

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void CellButton_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null) return;

            int position = int.Parse(btn.Tag.ToString());

            try
            {
                // Отправляем ход на сервер
                var response = await _client.PostAsync($"{ApiUrl}{position}", null);

                if (response.IsSuccessStatusCode)
                {
                    // Получаем состояние игры
                    var newState = await response.Content.ReadAsAsync<GameState>();
                    UpdateBoard(newState);
                }
                else
                {
                    MessageBox.Show("Ошибка сервера: " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Не удалось подключиться: " + ex.Message);
            }
        }

        private void UpdateBoard(GameState state)
        {
            // Находим сетку по имени из XAML
            foreach (var element in MyGrid.Children)
            {
                if (element is Button btn && btn.Tag != null)
                {
                    int pos = int.Parse(btn.Tag.ToString());
                    char cellValue = state.Board[pos];
                    btn.Content = (cellValue == '\0') ? "" : cellValue.ToString();
                }
            }

            if (state.Status != "In Progress")
            {
                MessageBox.Show("Игра окончена: " + state.Status);
            }
        }
    }
}