using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using MemoryGame.Controller;
using MemoryGame.Model;

namespace MemoryGame
{
    public partial class MainWindow : Window
    {
        private readonly GameController _controller;
        private Dictionary<int, Button> _cardButtons = new Dictionary<int, Button>();
        private DispatcherTimer _flipBackTimer;
        public MainWindow()
        {
            InitializeComponent();

            _controller = new GameController(new GameModel(2, 3));
            _controller.GameModel.CardsUpdated += OnCardsUpdated;
            _controller.GameModel.GameWon += OnGameWon;
            CreateCardButtons();
        }

        private void CreateCardButtons()
        {
            int rowCount = _controller.GameModel.Rows;
            int colCount = _controller.GameModel.Cols;

            for (int i = 0; i < rowCount; i++)
            {
                MainGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int j = 0; j < colCount; j++)
            {
                MainGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach (var card in _controller.GameModel.Cards)
            {
                Button button = new Button()
                {
                    Tag = card.Id,
                    Content = " ",
                    Margin = new Thickness(5),

                };

                button.Click += CardButton_Click;

                int row = card.Id / colCount;
                int col = card.Id % colCount;

                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                MainGrid.Children.Add(button);
                _cardButtons[card.Id] = button;
            }
        }

        private void CardButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int cardId = (int)button.Tag;
            _controller.CardClicked(cardId);
        }

        private void OnCardsUpdated(object sender, EventArgs e)
        {
            UpdateCardButtons();

            if (_controller.GameModel.FirstFlippedCard == null)
            {
                _flipBackTimer = new DispatcherTimer()
                {
                    Interval = TimeSpan.FromSeconds(2)
                };
                _flipBackTimer.Tick += FlipBackTimer_Tick;
                _flipBackTimer.Start();
            }

        }

        private void FlipBackTimer_Tick(object sender, EventArgs e)
        {
            _controller.FlipBackUnmatchedCards();
            _flipBackTimer.Stop();

        }

        private void UpdateCardButtons()
        {
            foreach (var card in _controller.GameModel.Cards)
            {
                Button button = _cardButtons[card.Id];

                if (card.IsMatched)
                {
                    button.IsEnabled = false;
                    SetImage(button, card.ImagePath);

                }
                else if (card.IsFlipped)
                    SetImage(button, card.ImagePath);
                else
                    button.Content = " ";

            }
        }

        private void SetImage(Button button, string imagePath)
        {
            var image = new Image
            {
                Source = new BitmapImage(new Uri(imagePath, UriKind.Relative))
            };
            button.Content = image;
        }

        private void OnGameWon(object sender, EventArgs e)
        {
            MessageBox.Show("ты победил!");
        }
    }
}

