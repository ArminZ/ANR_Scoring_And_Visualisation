using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Model
{
    class PointTemporaer : Point
    {
        public PointTemporaer(Point p)
        {
            this.Id = p.Id;
            this.latitude = p.latitude;
            this.longitude = p.longitude;
            this.altitude = p.altitude;
        }
        internal bool edited;
    }
}
