using CardFile.Common.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }
        public string DishName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int PortionSize { get; set; }
        public double Price { get; set; }
        public bool IsAvaliableNow { get; set; }
        public bool SeasonDish { get; set; }
        public bool IsVegan { get; set; }
        public bool IsSpicy { get; set; }

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Mapping.Mapper.Map(viewModel, this);

            UpdateAll();
        }

        public void IsNowAvaliable()
        {
            IsAvaliableNow = true;
        }

        public void IsntAvaliable()
            { IsAvaliableNow = false; }

        public void IsSeasonDish()
        {
            SeasonDish=true;
        }

        public void IsntSeasonal()
        {
            SeasonDish = false;
        }

        public void IsNowVegan()
        {
            IsVegan = true;
        }

        public void IsntVegan()
        {
            IsVegan=false;
        }

        public void IsNowSpicy()
            { IsSpicy = true; }

        public void IsntSpicy()
            { IsSpicy = false; }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(DishName));
            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Description));
            OnPropertyChanged(nameof(PortionSize));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(IsAvaliableNow));
            OnPropertyChanged(nameof(SeasonDish));
            OnPropertyChanged(nameof(IsVegan));
            OnPropertyChanged(nameof(IsSpicy));
        }
    }
}


