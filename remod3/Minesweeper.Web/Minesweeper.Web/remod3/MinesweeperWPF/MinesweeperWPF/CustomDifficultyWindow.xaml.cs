using System.Globalization;
using System.Windows;

namespace MinesweeperWPF;

public partial class CustomDifficultyWindow : Window {
    public int Rows { get; private set; }
    public int Cols { get; private set; }
    public int Mines { get; private set; }

    public CustomDifficultyWindow(int rows, int cols, int mines) {
        InitializeComponent();
        RowsBox.Text = rows.ToString(CultureInfo.InvariantCulture);
        ColsBox.Text = cols.ToString(CultureInfo.InvariantCulture);
        MinesBox.Text = mines.ToString(CultureInfo.InvariantCulture);
    }

    private void Ok_Click(object sender, RoutedEventArgs e) {
        ErrorText.Text = string.Empty;

        if (!int.TryParse(RowsBox.Text, out var r) || r < 5 || r > 60) {
            ErrorText.Text = "Строки: введите число от 5 до 60.";
            return;
        }

        if (!int.TryParse(ColsBox.Text, out var c) || c < 5 || c > 60) {
            ErrorText.Text = "Столбцы: введите число от 5 до 60.";
            return;
        }

        if (!int.TryParse(MinesBox.Text, out var m) || m < 1 || m >= r * c) {
            ErrorText.Text = "Мины: от 1 до (строки×столбцы−1).";
            return;
        }

        Rows = r;
        Cols = c;
        Mines = m;

        DialogResult = true;
    }
}
