/*
 TrackViewModel.cs

 Назначение:
 ViewModel для всего маршрута (трека). Представляет собой коллекцию точек маршрута
 и логику загрузки, преобразования и отображения данных из GPX-файла в интерфейсе пользователя.

 Использует:
 - TrackService для чтения и преобразования данных
 - TrackPointViewModel для представления каждой точки
 - INotifyPropertyChanged для обновления UI при изменении свойств
*/

using GpxDataShow.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace GpxDataShow.ViewModels
{
    /// <summary>
    /// ViewModel для всего трека (маршрута), включает список точек и общую длину маршрута.
    /// </summary>
    public class TrackViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Коллекция точек маршрута, используется для отображения в UI (например, в таблице или на графике).
        /// ObservableCollection позволяет автоматически обновлять UI при изменении коллекции.
        /// </summary>
        public ObservableCollection<TrackPointViewModel> Points { get; set; } = new ObservableCollection<TrackPointViewModel>();

        /// <summary>
        /// Общая длина маршрута (сумма расстояний между точками, рассчитывается при загрузке).
        /// </summary>
        public double TotalWay { get; set; }

        /// <summary>
        /// Сервис бизнес-логики, отвечающий за загрузку и обработку данных трека.
        /// </summary>
        private TrackService _trackService = new TrackService();

        /// <summary>
        /// Событие, уведомляющее об изменении свойств ViewModel (необходимо для корректной работы привязки данных в UI).
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Загружает данные из файла и преобразует их в ViewModel.
        /// </summary>
        public void LoadFromFile(string fileName)
        {
            _trackService.ReadFromFile(fileName); // Загружаем трек с помощью бизнес-сервиса
            BuildViewModel(); // Заполняем ViewModel данными из бизнес-модели
        }

        /// <summary>
        /// Преобразует данные трека из бизнес-модели в ViewModel.
        /// </summary>
        private void BuildViewModel()
        {
            Points.Clear(); // Очищаем текущие точки
            foreach (var segment in _trackService.Track.Segments)
            {
                foreach (var point in segment.Points)
                {
                    // Конвертируем каждую точку в ViewModel и добавляем в коллекцию
                    var newPoint = TrackPointViewModel.FromBusinessModel(point);
                    Points.Add(newPoint);
                }
            }

            // Вычисляем общую длину маршрута (сумма расстояний между точками)
            TotalWay = Points.Sum(p => p.Distance ?? 0);

            // Уведомляем интерфейс об изменении свойства
            OnPropertyChanged(nameof(TotalWay));
        }

        /// <summary>
        /// Метод для вызова события PropertyChanged — уведомление об изменении свойства.
        /// </summary>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
