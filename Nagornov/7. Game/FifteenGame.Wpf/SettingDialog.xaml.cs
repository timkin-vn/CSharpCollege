using System.Windows;

namespace FifteenGame.Wpf
{
    public partial class SettingsDialog : Window
    {
        public int SelectedMinesCount { get; private set; }

        public SettingsDialog()
        {
            InitializeComponent();
            SelectedMinesCount = 15; 
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedMinesCount = (int)MinesSlider.Value;
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}