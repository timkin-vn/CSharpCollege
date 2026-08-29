using CardFile.Common.Infrastructure;

namespace CardFile.Wpf.ViewModels;
public class CardViewModel : ViewModelBase {
    public int Id { get; set; }
    public string Title { get; set; }
    public string OriginalTitle { get; set; }
    public string Genre { get; set; }
    public string FullTitle => $"{Title} ({OriginalTitle})";
    public DateTime ReleaseDate { get; set; }

    public string ReleaseDateText => ReleaseDate.ToShortDateString();
    public string Studio { get; set; }
    public string Director { get; set; }
    public DateTime? EndDate { get; set; }

    public string EndDateText => EndDate?.ToShortDateString();
    public decimal Rating { get; set; }

    public bool WorksTillNow { get; set; }

    public bool IsDismissalDateEnabled => !WorksTillNow;

    public void LoadFromViewModel(CardViewModel viewModel) {
        Mapping.Mapper.Map(viewModel, this);
        WorksTillNow = !viewModel.EndDate.HasValue;

        UpdateAll();
    }

    public void WorksTillNowChecked() {
        EndDate = null;

        OnPropertyChanged(nameof(EndDate));
        OnPropertyChanged(nameof(IsDismissalDateEnabled));
    }

    public void WorksTillNowUnchecked() {
        EndDate = DateTime.Today;

        OnPropertyChanged(nameof(EndDate));
        OnPropertyChanged(nameof(IsDismissalDateEnabled));
    }

    private void UpdateAll() {
        OnPropertyChanged(nameof(Id));
        OnPropertyChanged(nameof(Title));
        OnPropertyChanged(nameof(OriginalTitle));
        OnPropertyChanged(nameof(Genre));
        OnPropertyChanged(nameof(FullTitle));
        OnPropertyChanged(nameof(ReleaseDate));
        OnPropertyChanged(nameof(ReleaseDateText));
        OnPropertyChanged(nameof(Studio));
        OnPropertyChanged(nameof(Director));
        OnPropertyChanged(nameof(EndDate));
        OnPropertyChanged(nameof(EndDateText));
        OnPropertyChanged(nameof(Rating));
        OnPropertyChanged(nameof(WorksTillNow));
        OnPropertyChanged(nameof(IsDismissalDateEnabled));
    }
}