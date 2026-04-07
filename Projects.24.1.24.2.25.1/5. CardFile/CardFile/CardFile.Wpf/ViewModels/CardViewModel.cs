using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        private IEnumerable<string> _availableHeroes;
        private IEnumerable<string> _availableItems;
        private IEnumerable<string> _availableNeutrals;
        private INotifyCollectionChanged _heroesNotify;
        private INotifyCollectionChanged _itemsNotify;
        private INotifyCollectionChanged _neutralsNotify;
        private string _heroSearchText;
        private string _neutralSearchText;
        private string _slot1SearchText;
        private string _slot2SearchText;
        private string _slot3SearchText;
        private string _slot4SearchText;
        private string _slot5SearchText;
        private string _slot6SearchText;

        public int Id { get; set; }

        public string Hero { get; set; }

        public string Slot1 { get; set; }

        public string Slot2 { get; set; }

        public string Slot3 { get; set; }

        public string Slot4 { get; set; }

        public string Slot5 { get; set; }

        public string Slot6 { get; set; }

        public string Neutral { get; set; }

        public IEnumerable<string> AvailableHeroes
        {
            get => _availableHeroes;
            set
            {
                if (_heroesNotify != null)
                {
                    _heroesNotify.CollectionChanged -= HeroesCollectionChanged;
                    _heroesNotify = null;
                }

                _availableHeroes = value;
                OnPropertyChanged(nameof(AvailableHeroes));

                _heroesNotify = value as INotifyCollectionChanged;
                if (_heroesNotify != null)
                {
                    _heroesNotify.CollectionChanged += HeroesCollectionChanged;
                }

                RefreshHeroes();
            }
        }

        public IEnumerable<string> AvailableItems
        {
            get => _availableItems;
            set
            {
                if (_itemsNotify != null)
                {
                    _itemsNotify.CollectionChanged -= ItemsCollectionChanged;
                    _itemsNotify = null;
                }

                _availableItems = value;
                OnPropertyChanged(nameof(AvailableItems));

                _itemsNotify = value as INotifyCollectionChanged;
                if (_itemsNotify != null)
                {
                    _itemsNotify.CollectionChanged += ItemsCollectionChanged;
                }

                RefreshAllItemFilters();
            }
        }

        public IEnumerable<string> AvailableNeutrals
        {
            get => _availableNeutrals;
            set
            {
                if (_neutralsNotify != null)
                {
                    _neutralsNotify.CollectionChanged -= NeutralsCollectionChanged;
                    _neutralsNotify = null;
                }

                _availableNeutrals = value;
                OnPropertyChanged(nameof(AvailableNeutrals));

                _neutralsNotify = value as INotifyCollectionChanged;
                if (_neutralsNotify != null)
                {
                    _neutralsNotify.CollectionChanged += NeutralsCollectionChanged;
                }

                RefreshNeutrals();
            }
        }

        public ObservableCollection<string> FilteredHeroes { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot1 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot2 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot3 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot4 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot5 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredItemsSlot6 { get; } = new ObservableCollection<string>();

        public ObservableCollection<string> FilteredNeutrals { get; } = new ObservableCollection<string>();

        public string HeroSearchText
        {
            get => _heroSearchText;
            set
            {
                _heroSearchText = value;
                OnPropertyChanged(nameof(HeroSearchText));
                RefreshHeroes();
            }
        }

        public string Slot1SearchText
        {
            get => _slot1SearchText;
            set
            {
                _slot1SearchText = value;
                OnPropertyChanged(nameof(Slot1SearchText));
                RefreshItemsForSlot(FilteredItemsSlot1, _slot1SearchText, Slot1);
            }
        }

        public string Slot2SearchText
        {
            get => _slot2SearchText;
            set
            {
                _slot2SearchText = value;
                OnPropertyChanged(nameof(Slot2SearchText));
                RefreshItemsForSlot(FilteredItemsSlot2, _slot2SearchText, Slot2);
            }
        }

        public string Slot3SearchText
        {
            get => _slot3SearchText;
            set
            {
                _slot3SearchText = value;
                OnPropertyChanged(nameof(Slot3SearchText));
                RefreshItemsForSlot(FilteredItemsSlot3, _slot3SearchText, Slot3);
            }
        }

        public string Slot4SearchText
        {
            get => _slot4SearchText;
            set
            {
                _slot4SearchText = value;
                OnPropertyChanged(nameof(Slot4SearchText));
                RefreshItemsForSlot(FilteredItemsSlot4, _slot4SearchText, Slot4);
            }
        }

        public string Slot5SearchText
        {
            get => _slot5SearchText;
            set
            {
                _slot5SearchText = value;
                OnPropertyChanged(nameof(Slot5SearchText));
                RefreshItemsForSlot(FilteredItemsSlot5, _slot5SearchText, Slot5);
            }
        }

        public string Slot6SearchText
        {
            get => _slot6SearchText;
            set
            {
                _slot6SearchText = value;
                OnPropertyChanged(nameof(Slot6SearchText));
                RefreshItemsForSlot(FilteredItemsSlot6, _slot6SearchText, Slot6);
            }
        }

        public string NeutralSearchText
        {
            get => _neutralSearchText;
            set
            {
                _neutralSearchText = value;
                OnPropertyChanged(nameof(NeutralSearchText));
                RefreshNeutrals();
            }
        }

        public string NewHeroName { get; set; }

        public string NewItemName { get; set; }

        public string NewNeutralName { get; set; }

        public System.Action<string> AddHeroToDatabase { get; set; }

        public System.Action<string> AddItemToDatabase { get; set; }

        public System.Action<string> AddNeutralToDatabase { get; set; }

        public string MarkdownRow => $"| {Hero} | {Slot1} | {Slot2} | {Slot3} | {Slot4} | {Slot5} | {Slot6} | {Neutral} |";

        public void AddHeroClicked()
        {
            var value = (NewHeroName ?? "").Trim();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            AddHeroToDatabase?.Invoke(value);
            NewHeroName = "";
            OnPropertyChanged(nameof(NewHeroName));
            RefreshHeroes();
        }

        public void AddItemClicked()
        {
            var value = (NewItemName ?? "").Trim();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            AddItemToDatabase?.Invoke(value);
            NewItemName = "";
            OnPropertyChanged(nameof(NewItemName));
            RefreshAllItemFilters();
        }

        public void AddNeutralClicked()
        {
            var value = (NewNeutralName ?? "").Trim();
            if (string.IsNullOrEmpty(value))
            {
                return;
            }

            AddNeutralToDatabase?.Invoke(value);
            NewNeutralName = "";
            OnPropertyChanged(nameof(NewNeutralName));
            RefreshNeutrals();
        }

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);

            RefreshHeroes();
            RefreshAllItemFilters();
            RefreshNeutrals();

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Hero));
            OnPropertyChanged(nameof(Slot1));
            OnPropertyChanged(nameof(Slot2));
            OnPropertyChanged(nameof(Slot3));
            OnPropertyChanged(nameof(Slot4));
            OnPropertyChanged(nameof(Slot5));
            OnPropertyChanged(nameof(Slot6));
            OnPropertyChanged(nameof(Neutral));
            OnPropertyChanged(nameof(MarkdownRow));
        }

        private void HeroesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshHeroes();
        }

        private void ItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshAllItemFilters();
        }

        private void NeutralsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            RefreshNeutrals();
        }

        private void RefreshHeroes()
        {
            RefreshList(FilteredHeroes, AvailableHeroes, HeroSearchText, Hero);
        }

        private void RefreshNeutrals()
        {
            RefreshList(FilteredNeutrals, AvailableNeutrals, NeutralSearchText, Neutral);
        }

        private void RefreshAllItemFilters()
        {
            RefreshItemsForSlot(FilteredItemsSlot1, Slot1SearchText, Slot1);
            RefreshItemsForSlot(FilteredItemsSlot2, Slot2SearchText, Slot2);
            RefreshItemsForSlot(FilteredItemsSlot3, Slot3SearchText, Slot3);
            RefreshItemsForSlot(FilteredItemsSlot4, Slot4SearchText, Slot4);
            RefreshItemsForSlot(FilteredItemsSlot5, Slot5SearchText, Slot5);
            RefreshItemsForSlot(FilteredItemsSlot6, Slot6SearchText, Slot6);
        }

        private void RefreshItemsForSlot(ObservableCollection<string> target, string query, string selected)
        {
            RefreshList(target, AvailableItems, query, selected);
        }

        private static void RefreshList(ObservableCollection<string> target, IEnumerable<string> source, string query, string ensureSelected)
        {
            var values = (source ?? Enumerable.Empty<string>()).Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => v.Trim()).Distinct().ToList();

            query = (query ?? "").Trim();
            if (!string.IsNullOrEmpty(query))
            {
                values = values.Where(v => v.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }

            ensureSelected = (ensureSelected ?? "").Trim();
            if (!string.IsNullOrEmpty(ensureSelected) && !values.Any(v => v.Equals(ensureSelected, StringComparison.OrdinalIgnoreCase)))
            {
                values.Insert(0, ensureSelected);
            }

            target.Clear();
            foreach (var v in values)
            {
                target.Add(v);
            }
        }
    }
}
