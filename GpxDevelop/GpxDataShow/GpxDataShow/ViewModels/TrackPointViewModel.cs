/*
 TrackPointViewModel.cs

 Назначение:
 Представляет собой модель представления (ViewModel) для одной точки маршрута (TrackPoint).
 Используется в слое представления (UI), чтобы связать данные бизнес-модели с визуальным отображением.

 Содержит не только исходные данные точки (широта, долгота, время и пр.), но и рассчитанные поля для отображения:
 - округлённые значения
 - текстовые представления с форматированием
 - флаги и признаки достоверности

 Связывается с бизнес-моделью TrackPoint через метод FromBusinessModel.
*/

using GpxDataShow.Business.Models;
using System;

namespace GpxDataShow.ViewModels
{
    /// <summary>
    /// ViewModel для отображения информации о точке трека на интерфейсе.
    /// </summary>
    public class TrackPointViewModel
    {
        // Географические координаты и временная метка
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateTime { get; set; }

        // Признак недостоверного направления (определяется при расчётах)
        public bool IsDirectionUnreliable { get; set; }

        // Основные измерения
        public double? Elevation { get; set; }
        public double? Distance { get; set; }

        // Показатели, рассчитанные по ближайшим точкам
        public double? NearestVelocity { get; set; }
        public double? NearestHeading { get; set; }
        public double? NearestVerticalSpeed { get; set; }

        // Показатели, рассчитанные по окну усреднения
        public double? MeanVelocity { get; set; }
        public double? MeanHeading { get; set; }
        public double? MeanVerticalSpeed { get; set; }
        public double? MeanElevation { get; set; }
        // Показатели с округлением
        public double? MeanVelocityRounded { get; set; }
        public double? MeanElevationRounded { get; set; }
        public double? MeanHeadingRounded { get; set; }


        // Текстовые версии для отображения в UI (с форматированием и проверкой на null)
        public string ElevationText => Elevation.HasValue ? Elevation.ToString() : "-";
        public string DistanceText => Distance.HasValue ? Distance.Value.ToString("F2") : "-";
        public string NearestVelocityText => NearestVelocity.HasValue ? NearestVelocity.Value.ToString("F2") : "-";
        public string NearestHeadingText => NearestHeading.HasValue ? NearestHeading.Value.ToString("F2") : "-";
        public string NearestVerticalSpeedText => NearestVerticalSpeed.HasValue ? NearestVerticalSpeed.Value.ToString("F2") : "-";
        public string MeanVelocityText => MeanVelocity.HasValue ? MeanVelocity.Value.ToString("F2") : "-";

        // Отображение угла с пометкой, если данные недостоверны
        public string MeanHeadingText => (MeanHeading.HasValue ? MeanHeading.Value.ToString("F2") : "-") +
            (IsDirectionUnreliable ? " (?)" : "");

        public string MeanVerticalSpeedText => MeanVerticalSpeed.HasValue ? MeanVerticalSpeed.Value.ToString("F2") : "-";
        public string MeanElevationText => MeanElevation.HasValue ? MeanElevation.Value.ToString("F2") : "-";

        // Округлённые значения для удобства визуального анализа (например, для построения графиков или группировки)
        public string MeanVelocityRoundedText => MeanVelocityRounded?.ToString("F0") ?? "-";
        public string MeanElevationRoundedText => MeanElevationRounded?.ToString("F0") ?? "-";
        public string MeanHeadingRoundedText => MeanHeadingRounded?.ToString("F0") ?? "-";


        /// <summary>
        /// Создаёт ViewModel из бизнес-модели TrackPoint.
        /// </summary>
        public static TrackPointViewModel FromBusinessModel(TrackPoint point)
        {
            return new TrackPointViewModel
            {
                Longitude = point.Longitude,
                Latitude = point.Latitude,
                DateTime = point.DateTime,
                Elevation = point.Elevation,
                Distance = point.Distance,
                NearestVelocity = point.NearestVelocity,
                NearestHeading = point.NearestHeading,
                NearestVerticalSpeed = point.NearestVerticalSpeed,
                MeanVelocity = point.MeanVelocity,
                MeanHeading = point.MeanHeading,
                MeanVerticalSpeed = point.MeanVerticalSpeed,
                MeanElevation = point.MeanElevation,
                IsDirectionUnreliable = point.IsDirectionUnreliable,
                MeanVelocityRounded = point.MeanVelocityRounded,
                MeanElevationRounded = point.MeanElevationRounded,
                MeanHeadingRounded = point.MeanHeadingRounded,
            };
        }
    }
}
