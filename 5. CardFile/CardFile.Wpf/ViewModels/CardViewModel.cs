using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardFile.Wpf.ViewModels
{
    public class CardViewModel : INotifyPropertyChanged
    {

        private string _laptopModel;
        private decimal _laptopPrice;
        private string _videoCard;
        private string _processor;
        private string _storage;
        private string _ram;
        private string _warranty;
        private string _selectedVideoCardVendor;
        private string _selectedProcessorVendor;
        public int Id { get; set; }

        public string LaptopModel
        {
            get => _laptopModel;
            set
            {
                _laptopModel = value;
                OnPropertyChanged(nameof(LaptopModel));
            }
        }

        public decimal LaptopPrice
        {
            get => _laptopPrice;
            set
            {
                _laptopPrice = value;
                OnPropertyChanged(nameof(LaptopPrice));
            }
        }

        public string VideoCard
        {
            get => _videoCard;
            set
            {
                _videoCard = value;
                OnPropertyChanged(nameof(VideoCard));
            }
        }

        public string Processor
        {
            get => _processor;
            set
            {
                _processor = value;
                OnPropertyChanged(nameof(Processor));
            }
        }

        public string Storage
        {
            get => _storage;
            set
            {
                _storage = value;
                OnPropertyChanged(nameof(Storage));
            }
        }

        public string Ram
        {
            get => _ram;
            set
            {
                _ram = value;
                OnPropertyChanged(nameof(Ram));
            }
        }

        public string Warranty
        {
            get => _warranty;
            set
            {
                _warranty = value;
                OnPropertyChanged(nameof(Warranty));
            }
        }
        public List<string> VideoCardVendors { get; } = new List<string> { "NVIDIA", "AMD" };
        public string SelectedVideoCardVendor
        {
            get => _selectedVideoCardVendor;
            set
            {
                _selectedVideoCardVendor = value;
                OnPropertyChanged(nameof(SelectedVideoCardVendor));
            }
        }

        public List<string> ProcessorVendors { get; } = new List<string> { "Intel", "AMD", "Apple", "Другое" };
        public string SelectedProcessorVendor
        {
            get => _selectedProcessorVendor;
            set
            {
                _selectedProcessorVendor = value;
                OnPropertyChanged(nameof(SelectedProcessorVendor));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void CopyFrom(CardViewModel model)
        {

            Id = model.Id;
            LaptopModel = model.LaptopModel;
            LaptopPrice = model.LaptopPrice;
            VideoCard = model.VideoCard;
            Processor = model.Processor;
            Storage = model.Storage;
            Ram = model.Ram;
            Warranty = model.Warranty;

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
