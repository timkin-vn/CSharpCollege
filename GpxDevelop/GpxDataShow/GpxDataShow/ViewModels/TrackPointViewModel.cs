using GpxDataShow.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.ViewModels
{
    public class TrackPointViewModel
    {
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

        public string ElevationText => Elevation.HasValue ? Elevation.ToString() : "-";

        public string DistanceText => Distance.HasValue ? Distance.Value.ToString("F2") : "-";

        public string NearestVelocityText => NearestVelocity.HasValue ? NearestVelocity.Value.ToString("F2") : "-";

        public string NearestHeadingText => NearestHeading.HasValue ? NearestHeading.Value.ToString("F2") : "-";

        public string NearestVerticalSpeedText => NearestVerticalSpeed.HasValue ? NearestVerticalSpeed.Value.ToString("F2") : "-";

        public string MeanVelocityText => MeanVelocity.HasValue ? MeanVelocity.Value.ToString("F2") : "-";

        public string MeanHeadingText => (MeanHeading.HasValue ? MeanHeading.Value.ToString("F2") : "-") + 
            (IsDirectionUnreliable ? " (?)" : "");

        public string MeanVerticalSpeedText => MeanVerticalSpeed.HasValue ? MeanVerticalSpeed.Value.ToString("F2") : "-";

        public string MeanElevationText => MeanElevation.HasValue ? MeanElevation.Value.ToString("F2") : "-";

        public string MeanVelocityRoundedText => RoundToNearest(MeanVelocity, 5)?.ToString("F0") ?? "-";
        public string MeanElevationRoundedText => RoundToNearest(MeanElevation, 5)?.ToString("F0") ?? "-";
        public string MeanHeadingRoundedText => RoundToNearest(MeanHeading, 10)?.ToString("F0") ?? "-";

        // Вспомогательный метод
        private double? RoundToNearest(double? value, double step)
        {
            if (!value.HasValue) return null;
            return Math.Round(value.Value / step) * step;
        }

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
