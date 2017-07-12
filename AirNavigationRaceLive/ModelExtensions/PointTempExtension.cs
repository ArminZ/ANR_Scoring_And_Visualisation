using AirNavigationRaceLive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.ModelExtensions
{
    [NotMapped]
    class PointTempExtension : Point
    {
        public PointTempExtension(Point p)
        {
            this.Id = p.Id;
            this.latitude = p.latitude;
            this.longitude = p.longitude;
            this.altitude = p.altitude;
        }
        internal bool edited;
    }
}
