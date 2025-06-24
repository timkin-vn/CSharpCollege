using GpxDataShow.Business.Models;
using GpxDataShow.Data.Models;
using GpxDataShow.Data.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.Business.Services
{
    public class TrackService
    {
        private DataService _dataService = new DataService();

        public Track Track { get; set; }

        public int TakeMeanBefore = 2;

        public int TakeMeanAfter = 2;

        public const double EarthRadius = 6372795;

        public const double DegreesPerHalfCircle = 180;

        public const double MPerSecToKmPerHour = 3.6;

        public void ReadFromFile(string fileName)
        {
            var inputData = _dataService.ReadFromFile(fileName);
            Track = CreateFromInput(inputData);
            CalculateMeanData();
        }

        private Track CreateFromInput(GpxData input)
        {
            var result = new Track { Segments = new List<TrackSegment>() };
            foreach (var segment in input.Track.Segments)
            {
                var segmentModel = CreateSegmentFromInput(segment);
                if (segmentModel.Points.Any())
                {
                    result.Segments.Add(segmentModel);
                }
            }

            return result;
        }

        private TrackSegment CreateSegmentFromInput(TrackSegmentDto input)
        {
            var result = new TrackSegment { Points = new List<TrackPoint>() };
            foreach (var point in input.Points)
            {
                var pointModel = CreatePointFromInput(point);
                if (pointModel != null)
                {
                    result.Points.Add(pointModel);
                }
            }

            return result;
        }

        private TrackPoint CreatePointFromInput(TrackPointDto input)
        {
            var result = new TrackPoint
            {
                Elevation = Math.Round((input.Elevation ?? 0) * 100) / 100,
            };

            if (double.TryParse(input.Latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lat))
            {
                result.Latitude = lat;
            }

            if (double.TryParse(input.Longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lon))
            {
                result.Longitude = lon;
            }

            if (DateTime.TryParse(input.Time, out DateTime dt))
            {
                result.DateTime = dt;
            }
            else
            {
                return null;
            }

            return result;
        }

        private void CalculateMeanData()
        {
            foreach (var segment in Track.Segments) 
            {
                for (int i = 1; i < segment.Points.Count; i++)
                {
                    double dt = (segment.Points[i - 1].DateTime != DateTime.MinValue && segment.Points[i].DateTime != DateTime.MinValue)
                        ? (segment.Points[i].DateTime - segment.Points[i - 1].DateTime).TotalSeconds
                        : 1;
                    double phi1 = segment.Points[i - 1].Latitude * Math.PI / DegreesPerHalfCircle;
                    double phi2 = segment.Points[i].Latitude * Math.PI / DegreesPerHalfCircle;
                    double dLambda = (segment.Points[i].Longitude - segment.Points[i - 1].Longitude) * Math.PI / DegreesPerHalfCircle;
                    double distance = CalculateDistance(phi1, phi2, dLambda);

                    segment.Points[i].Distance = distance;
                    segment.Points[i].NearestVelocity = distance / dt * MPerSecToKmPerHour;
                    if (distance > 0.1)
                    {
                        double heading = CalculateHeading(phi1, phi2, dLambda);
                        segment.Points[i].NearestHeading = heading;
                    }

                    segment.Points[i].NearestVerticalSpeed = (segment.Points[i].Elevation - segment.Points[i - 1].Elevation) / dt;
                }

                for (int i = TakeMeanBefore; i < segment.Points.Count - TakeMeanAfter; i++)
                {
                    double dt = (segment.Points[i - TakeMeanBefore].DateTime != DateTime.MinValue && segment.Points[i + TakeMeanAfter].DateTime != DateTime.MinValue)
                        ? (segment.Points[i + TakeMeanAfter].DateTime - segment.Points[i - TakeMeanBefore].DateTime).TotalSeconds
                        : TakeMeanBefore + TakeMeanAfter;
                    double sumElevation = 0;
                    foreach (int j in Enumerable.Range(i - TakeMeanBefore, TakeMeanBefore + TakeMeanAfter + 1))
                    {
                        sumElevation += segment.Points[j].Elevation;
                    }

                    segment.Points[i].MeanElevation = sumElevation / (TakeMeanBefore + TakeMeanAfter + 1);

                    double phi1 = segment.Points[i - TakeMeanBefore].Latitude * Math.PI / DegreesPerHalfCircle;
                    double phi2 = segment.Points[i + TakeMeanAfter].Latitude * Math.PI / DegreesPerHalfCircle;
                    double dLambda = (segment.Points[i + TakeMeanAfter].Longitude - segment.Points[i - TakeMeanBefore].Longitude) *
                        Math.PI / DegreesPerHalfCircle;
                    double distance = CalculateDistance(phi1, phi2, dLambda);

                    segment.Points[i].MeanVelocity = distance / dt * MPerSecToKmPerHour;

                    if (distance > 0.1)
                    {
                        double meanHeading = CalculateHeading(phi1, phi2, dLambda);
                        segment.Points[i].MeanHeading = meanHeading;
                    }

                    if (distance < 1)
                    {
                        segment.Points[i].IsDirectionUnreliable = true;
                    }

                    segment.Points[i].MeanVerticalSpeed = (segment.Points[i + TakeMeanAfter].Elevation - segment.Points[i - TakeMeanBefore].Elevation) / dt;
                }
            }
        }

        private static double CalculateDistance(double phi1, double phi2, double dLambda)
        {
            return EarthRadius * 2 * Math.Asin(
                Math.Sqrt(
                    Math.Sin((phi2 - phi1) / 2) * Math.Sin((phi2 - phi1) / 2) +
                    Math.Cos(phi1) * Math.Cos(phi2) * Math.Sin(dLambda / 2) * Math.Sin(dLambda / 2)
                )
            );
        }

        private static double CalculateHeading(double phi1, double phi2, double dLambda)
        {
            double x = Math.Cos(phi1) * Math.Sin(phi2) - Math.Sin(phi1) * Math.Cos(phi2) * Math.Cos(dLambda);
            double y = Math.Sin(dLambda) * Math.Cos(phi2);
            double z = -Math.Atan2(-y, x);

            if (z < 0)
            {
                z += 2 * Math.PI;
            }

            double heading = z * DegreesPerHalfCircle / Math.PI;
            return heading;
        }
    }
}
