using CardFile.Wpf.ViewModels;
using System.Windows;

namespace CardFile.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CardEditWindow.xaml
    /// </summary>
    public partial class CardEditWindow : Window
    {
        // Конструктор, который принимает ViewModel — исправляет ошибку CS1729
        public CardEditWindow(CardViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        // Пустой конструктор оставляем только для поддержки дизайнера XAML (если он пытается открыть окно)
        public CardEditWindow() : this(new CardViewModel()) { }

        public CardViewModel ViewModel => (CardViewModel)DataContext;

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        // Эти методы удалены, так как они вызывали несуществующие методы в CardViewModel
        // и были нужны только для логики "работает/уволен" сотрудников.
    }
}
