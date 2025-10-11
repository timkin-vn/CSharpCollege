using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;
using FifteenGame.Wpf.ViewModels;
using System;

namespace FifteenGame.Wpf
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseRightButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonUp(e);

            if (e.OriginalSource is FrameworkElement element && element.DataContext is CellViewModel cell)
            {
                var viewModel = (MainWindowViewModel)DataContext;
                viewModel.ToggleFlag(cell);
                e.Handled = true;
            }
        }

        private void MinesTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        // Метод для запуска анимаций при проигрыше
        public void PlayGameOverAnimations()
        {
            // Анимация мигания фона
            var backgroundAnimation = (Storyboard)FindResource("GameOverAnimation");
            Storyboard.SetTarget(backgroundAnimation, MainGrid);
            backgroundAnimation.Begin();

            // Анимация изменения заголовка
            AnimateTitleText();
        }

        private void AnimateTitleText()
        {
            var titleAnimation = new DoubleAnimation
            {
                From = 24,
                To = 28,
                Duration = TimeSpan.FromSeconds(0.2),
                AutoReverse = true,
                RepeatBehavior = new RepeatBehavior(3)
            };
            TitleText.BeginAnimation(TextBlock.FontSizeProperty, titleAnimation);
        }
    }
}