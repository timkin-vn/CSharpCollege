
using CardFile.Common.Infrastructure;
using System;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : ViewModelBase
    {
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string CarName =>
            $"{Brand} {Model}".Trim();

        public int Year { get; set; }

        public string Color { get; set; }

        public int Mileage { get; set; }

        public decimal Price { get; set; }

        public string FuelType { get; set; }

        public decimal EngineVolume { get; set; }

        public string TransmissionType { get; set; }

        public bool HasAirConditioning { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? SoldDate { get; set; }

        public string Vin { get; set; }

        public string AddedDateText =>
            AddedDate.ToShortDateString();

        public string SoldDateText =>
            SoldDate?.ToShortDateString() ?? "Не продан";

        public string PriceText =>
            Price.ToString("N2") + " ₽";

        public string MileageText =>
            Mileage.ToString("N0") + " км";

        public string EngineVolumeText =>
            EngineVolume.ToString("0.0") + " л";

        public string AirConditioningText =>
            HasAirConditioning ? "Есть" : "Нет";

        public void LoadViewModel(CardViewModel model)
        {
            Mapping.Mapper.Map(model, this);
            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Brand));
            OnPropertyChanged(nameof(Model));
            OnPropertyChanged(nameof(CarName));
            OnPropertyChanged(nameof(Year));
            OnPropertyChanged(nameof(Color));
            OnPropertyChanged(nameof(Mileage));
            OnPropertyChanged(nameof(Price));
            OnPropertyChanged(nameof(FuelType));
            OnPropertyChanged(nameof(EngineVolume));
            OnPropertyChanged(nameof(TransmissionType));
            OnPropertyChanged(nameof(HasAirConditioning));
            OnPropertyChanged(nameof(AddedDate));
            OnPropertyChanged(nameof(SoldDate));
            OnPropertyChanged(nameof(Vin));
            OnPropertyChanged(nameof(AddedDateText));
            OnPropertyChanged(nameof(SoldDateText));
            OnPropertyChanged(nameof(PriceText));
            OnPropertyChanged(nameof(MileageText));
            OnPropertyChanged(nameof(EngineVolumeText));
            OnPropertyChanged(nameof(AirConditioningText));
        }
    }
}
