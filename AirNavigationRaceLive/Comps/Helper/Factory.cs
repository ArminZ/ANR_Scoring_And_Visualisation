using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Helper
{
    class Factory
    {
        public static Point newGPSPoint(double longitude, double latitude, double altitude)
        {
            Point result = new Point();
            result.longitude = longitude;
            result.latitude = latitude;
            result.altitude = altitude;
            return result;
        }
        public static Point newGPSPoint(Point point)
        {
            Point result = new Point();
            result.longitude = point.longitude;
            result.latitude = point.latitude;
            result.altitude = point.altitude;
            return result;
        }

        public static Line Line(Line l)
        {
            Line result = new Line();
            result.A = newGPSPoint(l.A);
            result.B = newGPSPoint(l.B);
            result.O = newGPSPoint(l.O);
            result.Id = l.Id;
            result.Type = l.Type;
            return result;
        }
    }
}
