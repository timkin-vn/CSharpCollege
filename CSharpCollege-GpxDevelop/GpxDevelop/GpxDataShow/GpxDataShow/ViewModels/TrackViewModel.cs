using GpxDataShow.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.ViewModels
{
    public class TrackViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<TrackPointViewModel> Points { get; set; } = new ObservableCollection<TrackPointViewModel>();

        public double TotalWay { get; set; }

        private TrackService _trackService = new TrackService();

        public event PropertyChangedEventHandler PropertyChanged;

        public void LoadFromFile(string fileName)
        {
            _trackService.ReadFromFile(fileName);
            BuildViewModel();
        }

        private void BuildViewModel()
        {
            Points.Clear();
            DateTime lastDateTime = DateTime.MinValue;
            foreach (var segment in _trackService.Track.Segments)
            {
                foreach (var point in segment.Points)
                {
                    var newPoint = TrackPointViewModel.FromBusinessModel(point);
                    Points.Add(newPoint);
                }
            }

            TotalWay = Points.Sum(p => p.Distance ?? 0);
            OnPropertyChanged("TotalWay");
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
