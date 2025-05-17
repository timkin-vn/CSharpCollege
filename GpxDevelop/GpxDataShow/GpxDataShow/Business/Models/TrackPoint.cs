using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpxDataShow.Business.Models
{
    public class TrackPoint
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Elevation { get; set; }

        public DateTime DateTime { get; set; }

        public bool IsDirectionUnreliable { get; set; }

        public double? Distance { get; set; }

        public double? NearestVelocity { get; set; }

        public double? NearestHeading { get; set; }

        public double? NearestVerticalSpeed { get; set; }

        public double? MeanVelocity { get; set; }

        public double? MeanHeading { get; set; }

        public double? MeanVerticalSpeed { get; set; }

        public double? MeanElevation { get; set; }
    }
}
