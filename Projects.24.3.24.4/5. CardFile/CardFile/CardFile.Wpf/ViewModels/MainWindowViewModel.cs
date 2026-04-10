        using CardFile.Business.Models;
        using CardFile.Business.Services;
        using CardFile.Common.Infrastructure;
        using CardFile.Wpf.Infrastructure;
        using System;
        using System.Collections.Generic;
        using System.Collections.ObjectModel;
        using System.IO;
        using System.Linq;

        namespace CardFile.Wpf.ViewModels
        {
            public class MainWindowViewModel : ViewModelBase
            {
                private readonly CardFileService _service = new CardFileService();
                private List<CardViewModel> _allCards = new List<CardViewModel>();
                private CardViewModel _selectedCard;

                public ObservableCollection<CardViewModel> Cards { get; } = new ObservableCollection<CardViewModel>();

                public CardViewModel SelectedCard
                {
                    get => _selectedCard;
                    set
                    {
                        _selectedCard = value;
                        OnPropertyChanged(nameof(SelectedCard));
                        OnPropertyChanged(nameof(IsEditButtonEnabled));
                        OnPropertyChanged(nameof(IsDeleteButtonEnabled));
                    }
                }

                public bool IsEditButtonEnabled => SelectedCard != null;
                public bool IsDeleteButtonEnabled => SelectedCard != null;
                public string FileName { get; private set; }
                public string WindowTitle => string.IsNullOrEmpty(FileName) ? "Картотека альбомов" : $"Картотека альбомов: {Path.GetFileName(FileName)}";

public bool IsReleaseDateAscending { get; private set; } = true;
public string SortButtonText => IsReleaseDateAscending
    ? "Сортировка по дате релиза: сначала старые"
    : "Сортировка по дате релиза: сначала новые";


                public MainWindowViewModel()
                {
                    MapperInitialization.PreRegister();
                }

                public void WindowLoaded()
                {
                    Mapping.Initialize();
                    LoadAllData();
                }

                public CardViewModel GetSelectedCard()
                {
                    return SelectedCard;
                }

                public CardViewModel GetNewCard()
                {
                    return new CardViewModel
                    {
                        ReleaseDate = new DateTime(2021, 1, 1),
                        PurchaseDate = DateTime.Today,
                    };
                }

                public void SaveEditedCard(CardViewModel card)
                {
                    var index = _allCards.FindIndex(c => c.Id == card.Id);
                    if (index < 0)
                        throw new Exception("Карточка с таким Id не существует");

                    var id = _service.Save(ToBusiness(card));
                    if (id < 0)
                        return;

                    _allCards[index].LoadViewModel(card);
                    ApplyView();
                }

                public void SaveNewCard(CardViewModel card)
                {
                    var newCard = new CardViewModel();
                    newCard.LoadViewModel(card);

                    var id = _service.Save(ToBusiness(card));
                    if (id < 0)
                        return;

                    newCard.Id = id;
                    _allCards.Add(newCard);
                    ApplyView();
                }

                public void DeleteSelectedCard()
                {
                    if (SelectedCard == null)
                        return;

                    _service.Delete(SelectedCard.Id);
                    var index = _allCards.FindIndex(c => c.Id == SelectedCard.Id);
                    if (index < 0)
                        throw new Exception("Карточка с таким Id не существует");

                    _allCards.RemoveAt(index);
                    SelectedCard = null;
                    ApplyView();
                }

                public void SelectionChanged()
                {
                }

                public void SaveToFile(string fileName)
                {
                    _service.SaveToFile(fileName);
                    FileName = fileName;
                    OnPropertyChanged(nameof(WindowTitle));
                }

                public void SaveToFile()
                {
                    try
                    {
                        _service.SaveToFile(FileName);
                    }
                    catch (Exception)
                    {
                        FileName = null;
                        OnPropertyChanged(nameof(WindowTitle));
                        throw;
                    }
                }

                public void OpenFromFile(string fileName)
                {
                    _service.OpenFromFile(fileName);
                    LoadAllData();
                    FileName = fileName;
                    OnPropertyChanged(nameof(WindowTitle));
                }
public void ToggleReleaseDateSort()
{
    IsReleaseDateAscending = !IsReleaseDateAscending;
    OnPropertyChanged(nameof(IsReleaseDateAscending));
    OnPropertyChanged(nameof(SortButtonText));
    ApplyView();
}


                private void LoadAllData()
                {
                    _allCards = _service.GetAll().Select(ToViewModel).ToList();
                    ApplyView();
                }

                private void ApplyView()
                {
                    var items = _allCards.AsEnumerable();
            items = IsReleaseDateAscending ? items.OrderBy(card => card.ReleaseDate) : items.OrderByDescending(card => card.ReleaseDate);

                    var itemsList = items.ToList();
                    var selectedId = SelectedCard?.Id;

                    Cards.Clear();
                    foreach (var card in itemsList)
                        Cards.Add(card);

                    if (selectedId.HasValue)
                        SelectedCard = Cards.FirstOrDefault(c => c.Id == selectedId.Value);
                }

                private CardViewModel ToViewModel(Card card)
                {
                    return Mapping.Mapper.Map<CardViewModel>(card);
                }

                private Card ToBusiness(CardViewModel card)
                {
                    return Mapping.Mapper.Map<Card>(card);
                }
            }
        }
