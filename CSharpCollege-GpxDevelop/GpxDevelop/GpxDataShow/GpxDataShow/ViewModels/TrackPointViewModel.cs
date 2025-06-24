using GpxDataShow.Business.Models;
using System;
using System.Windows.Media;

namespace GpxDataShow.ViewModels
{
    public class TrackPointViewModel
    {
        public Brush NorthColor { get; set; } = new SolidColorBrush(Color.FromRgb(81, 109, 237));
        public Brush EastColor { get; set; } = new SolidColorBrush(Color.FromRgb(104, 237, 81));
        public Brush SouthColor { get; set; } = new SolidColorBrush(Color.FromRgb(237, 81, 81));
        public Brush WestColor { get; set; } = new SolidColorBrush(Color.FromRgb(45, 184, 35));




        private const double SpeedMin = 2;
        private const double SpeedMax = 100;

        private const double LatitudeMin = -90; // Ну, экватор не пересекался, тут уж градиента не видно, но он есть
        private const double LatitudeMax = 90;

        private const double LongitudeMin = -180; //та же ситуация
        private const double LongitudeMax = 180;

        private const double ElevationMin = 0;   
        private const double ElevationMax = 191;  



        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsDirectionUnreliable { get; set; }
        public double? Elevation { get; set; }
        public double? Distance { get; set; }
        public double? NearestVelocity { get; set; }
        public double? NearestHeading { get; set; }
        public double? NearestVerticalSpeed { get; set; }
        public double? MeanVelocity { get; set; }
        public double? MeanHeading { get; set; }
        public double? MeanVerticalSpeed { get; set; }
        public double? MeanElevation { get; set; }

        private Brush GradientBrush(double value, double min, double max)
        {
            double t = (value - min) / (max - min);
            t = Math.Max(0, Math.Min(1, t));

            byte baseR = 227;
            byte baseG = 77;

            byte r = (byte)Math.Min(255, baseR * (1 - t) + 60);
            byte g = (byte)Math.Min(255, baseG * t + 20);

            return new SolidColorBrush(Color.FromRgb(r, g, 0));
        }




        public Brush LongitudeBrush => GradientBrush(Longitude, LongitudeMin, LongitudeMax);
        public Brush LatitudeBrush => GradientBrush(Latitude, LatitudeMin, LatitudeMax);

        public Brush MeanVelocityBrush =>
            MeanVelocity.HasValue && MeanVelocity.Value >= SpeedMin
                ? GradientBrush(MeanVelocity.Value, SpeedMin, SpeedMax)
                : Brushes.Gray;

        public Brush MeanElevationBrush =>
            MeanElevation.HasValue
                ? GradientBrush(MeanElevation.Value, ElevationMin, ElevationMax)
                : Brushes.Gray;

        public Brush MeanHeadingBrush
        {
            get
            {
                if (!MeanHeading.HasValue)
                    return Brushes.Transparent;

                double heading = MeanHeading.Value % 360;
                if (heading >= 315 || heading < 45)     // Север
                    return NorthColor;
                if (heading >= 45 && heading < 135)     // Восток
                    return EastColor;
                if (heading >= 135 && heading < 225)    // Юг
                    return SouthColor;
                if (heading >= 225 && heading < 315)    // Запад
                    return WestColor;

                return Brushes.Transparent;
            }
        }

        public string ElevationText => Elevation.HasValue ? Elevation.Value.ToString("F2") : "-";
        public string DistanceText => Distance.HasValue ? Distance.Value.ToString("F2") : "-";
        public string NearestVelocityText => NearestVelocity.HasValue ? NearestVelocity.Value.ToString("F2") : "-";
        public string NearestHeadingText => NearestHeading.HasValue ? NearestHeading.Value.ToString("F2") : "-";
        public string NearestVerticalSpeedText => NearestVerticalSpeed.HasValue ? NearestVerticalSpeed.Value.ToString("F2") : "-";
        public string MeanVelocityText => MeanVelocity.HasValue ? MeanVelocity.Value.ToString("F2") : "-";
        public string MeanHeadingText => (MeanHeading.HasValue ? MeanHeading.Value.ToString("F2") : "-") + (IsDirectionUnreliable ? " (?)" : "");
        public string MeanVerticalSpeedText => MeanVerticalSpeed.HasValue ? MeanVerticalSpeed.Value.ToString("F2") : "-";
        public string MeanElevationText => MeanElevation.HasValue ? MeanElevation.Value.ToString("F2") : "-";

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
            };
        }
    }
}
