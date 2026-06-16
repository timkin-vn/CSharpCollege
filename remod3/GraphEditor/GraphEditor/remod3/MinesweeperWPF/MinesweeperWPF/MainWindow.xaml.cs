using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MinesweeperWPF.ViewModels;

namespace MinesweeperWPF;

public partial class MainWindow : Window {
    public MainWindow() {
        InitializeComponent();

        if (DataContext is MainViewModel vm) {
            vm.CustomDifficultyRequested += (_, _) => ShowCustomDifficulty(vm);
            vm.GameEnded += (_, e) => {
                var msg = e.HasWon ? $"Победа! Время: {e.Seconds} c" : "Вы подорвались на мине :(";
                var res = MessageBox.Show(this, msg + "\nСыграть ещё?", "Итог",
                    MessageBoxButton.YesNo, MessageBoxImage.Information);

                if (res == MessageBoxResult.Yes)
                    vm.NewGameCommand.Execute(null);
            };
        }
    }

    private void Cell_RightClick(object sender, MouseButtonEventArgs e) {
        if (DataContext is not MainViewModel vm) return;
        if (sender is not Button b) return;
        if (b.DataContext is not CellViewModel cell) return;

        vm.CellRightClickCommand.Execute(cell);
        e.Handled = true;
    }

    private void ShowCustomDifficulty(MainViewModel vm) {
        var previous = vm.DifficultyIndexFromCurrent();

        var dlg = new CustomDifficultyWindow(vm.Rows, vm.Columns, vm.MinesSetting) {
            Owner = this
        };

        if (dlg.ShowDialog() == true) {
            vm.SetCustomDifficulty(dlg.Rows, dlg.Cols, dlg.Mines);
        }
        else {
            vm.SetDifficultyIndex(previous);
        }
    }
}
