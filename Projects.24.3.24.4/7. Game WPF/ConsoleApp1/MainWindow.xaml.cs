// Файл: LightsOutGame.Wpf/MainWindow.xaml.cs
using LightsOutGame.Business; // Подключаем бизнес-логику!
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LightsOutGame.Wpf
{
    public partial class MainWindow : Window
    {
        private Game _game;
        private Button[,] _buttons;

        public MainWindow()
        {
            InitializeComponent();
            _game = new Game();
            _buttons = new Button[_game.Size, _game.Size];

            CreateGrid();
            UpdateGrid();
        }

        // Динамически генерируем кнопки в сетке WPF
        private void CreateGrid()
        {
            GameGrid.Children.Clear();
            for (int r = 0; r < _game.Size; r++)
            {
                for (int c = 0; c < _game.Size; c++)
                {
                    Button btn = new Button
                    {
                        Margin = new Thickness(4),
                        Tag = new Point(r, c) // Сохраняем координаты кнопки прямо в неё
                    };

                    // Вешаем событие клика
                    btn.Click += Button_Click;

                    _buttons[r, c] = btn;
                    GameGrid.Children.Add(btn); // Добавляем кнопку на экран
                }
            }
        }

        // Обновляем цвета кнопок на основе массива из Бизнес-логики
        private void UpdateGrid()
        {
            for (int r = 0; r < _game.Size; r++)
            {
                for (int c = 0; c < _game.Size; c++)
                {
                    if (_game.Board[r, c])
                    {
                        _buttons[r, c].Background = new SolidColorBrush(Color.FromRgb(255, 208, 102)); // Желтый (горит)
                    }
                    else
                    {
                        _buttons[r, c].Background = new SolidColorBrush(Color.FromRgb(61, 61, 74));    // Серый (погас)
                    }
                }
            }

            // Если победа — показываем сообщение
            if (_game.IsWon)
            {
                MessageBox.Show("🎉 Поздравляем! Вы потушили все светильники!", "Победа!");
            }
        }

        // Обработка нажатия на лампочку
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is Point point)
            {
                int row = (int)point.X;
                int col = (int)point.Y;

                _game.MakeMove(row, col); // Вызываем расчет ходов из Бизнес-логики
                UpdateGrid();             // Перерисовываем цвета кнопок
            }
        }

        // Клик по кнопке "Начать заново"
        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            _game.Reset();
            UpdateGrid();
        }
    }
}