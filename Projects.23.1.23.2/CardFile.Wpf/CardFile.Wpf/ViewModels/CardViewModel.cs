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

        /// <summary>
        /// Название техники (например, "Т-34")
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Производитель/страна (например, "СССР")
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// Конструктор (КБ или личность)
        /// </summary>
        public string Designer { get; set; }

        /// <summary>
        /// Год начала производства
        /// </summary>
        public int ProductionYear { get; set; }

        /// <summary>
        /// Тип техники (танк, самолёт и т.д.)
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Максимальная скорость (км/ч)
        /// </summary>
        public double MaxSpeed { get; set; }

        /// <summary>
        /// Характеристики орудия
        /// </summary>
        public string Gun { get; set; }

        /// <summary>
        /// Вес в тоннах
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// Краткая информация для списка
        /// </summary>
        public string ShortInfo => $"{Name} ({Type}, {ProductionYear})";

        /// <summary>
        /// Полная техническая сводка
        /// </summary>
        public string FullTechSpecs =>
            $"Производитель: {Manufacturer}\n" +
            $"Конструктор: {Designer}\n" +
            $"Скорость: {MaxSpeed} км/ч\n" +
            $"Орудие: {Gun}\n" +
            $"Вес: {Weight} т";

        public void LoadFromViewModel(CardViewModel viewModel)
        {
            Id = viewModel.Id;
            Name = viewModel.Name;
            Manufacturer = viewModel.Manufacturer;
            Designer = viewModel.Designer;
            ProductionYear = viewModel.ProductionYear;
            Type = viewModel.Type;
            MaxSpeed = viewModel.MaxSpeed;
            Gun = viewModel.Gun;
            Weight = viewModel.Weight;

            UpdateAll();
        }

        private void UpdateAll()
        {
            OnPropertyChanged(nameof(Id));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(Designer));
            OnPropertyChanged(nameof(ProductionYear));
            OnPropertyChanged(nameof(Type));
            OnPropertyChanged(nameof(MaxSpeed));
            OnPropertyChanged(nameof(Gun));
            OnPropertyChanged(nameof(Weight));
            OnPropertyChanged(nameof(ShortInfo));
            OnPropertyChanged(nameof(FullTechSpecs));
        }
    }
}