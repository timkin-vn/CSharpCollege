using System.ComponentModel;

namespace CardFile.Wpf.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged {
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName) {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}