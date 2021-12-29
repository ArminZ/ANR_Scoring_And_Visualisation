using System;
using swisstopo.geodesy.gpsref;
using AirNavigationRaceLive.Model;
using System.Collections.Generic;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class Converter
    {
        private double topLeftLongitude;
        private double topLeftLatitude;
        double sizeLongitude;
        double sizeLatitude;
        public Converter(MapSet map)
        {
            topLeftLongitude = map.XTopLeft;
            topLeftLatitude = map.YTopLeft;
            sizeLongitude = map.XSize;
            sizeLatitude = map.YSize;
        }

        #region Map coordinate + sizing functionality
        public double TopLeftLongitudeX
        {
            get
            {
                return topLeftLongitude;
            }
        }
        public double TopLeftLatitudeY
        {
            get
            {
                return topLeftLatitude;
            }
        }
        public double SizeLongitudeX
        {
            get
            {
                return sizeLongitude;
            }
        }
        public double SizeLatitudeY
        {
            get
            {
                return sizeLatitude;
            }
        }
        public double XtoLongitude(double x)
        {
            return topLeftLongitude + x * sizeLongitude;
        }
        public int LongitudeToX(double longitude)
        {
            return (int)((longitude - topLeftLongitude) / sizeLongitude);
        }
        public double YtoLatitude(double y)
        {
            return topLeftLatitude + y * sizeLatitude;
        }
        public int LatitudeToY(double latitude)
        {
            return (int)((latitude - topLeftLatitude) / sizeLatitude);
        }
        public int getStartX(Line l)
        {
            return LongitudeToX(l.A.longitude);
        }
        public int getStartY(Line l)
        {
            return LatitudeToY(l.A.latitude);
        }
        public int getEndX(Line l)
        {
            return LongitudeToX(l.B.longitude);
        }
        public int getEndY(Line l)
        {
            return LatitudeToY(l.B.latitude);
        }
        public int getOrientationX(Line l)
        {
            return LongitudeToX(l.O.longitude);
        }
        public int getOrientationY(Line l)
        {
            return LatitudeToY(l.O.latitude);
        } 
        #endregion

        #region Distance and Bearing calculation (Haversine, Great Circle)
        public static double Distance(Point point1, Point point2)
        {
            return Distance(point1.longitude, point1.latitude, point2.longitude, point2.latitude);
        }

        public static double Distance(double long1, double lat1, double long2, double lat2)
        {
            // somewhat re-written 2021-09-14
            // see https://danielsaidi.com/blog/2011/02/04/calculate-distance-and-bearing-between-two-positions

            //The Haversine formula
            //a = sin²(Δlat/2) + cos(lat1)*cos(lat2)*sin²(Δlong/2)
            //c = 2.atan2(√a, √(1−a))
            //d = R*c
            // returns: distance in km
            // NOTE regardig earth radius: there are different options what to use: Mean Equatorial, Mean Polar, Authalic/Volumetric, Meridional. And others
            // Also an implementation of an ellipse based on Mean Equatorial and Mean Polar would be possible
            // Implemented here: Authalic/Volumetric Earth Radius in km
            const double R = 6371.0; // Authalic/Volumetric Earth Radius in km
            double dLon = DegreeToRadians(long2)  - DegreeToRadians(long1);
            double dLat = DegreeToRadians(lat2) - DegreeToRadians(lat1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(DegreeToRadians(lat1)) * Math.Cos(DegreeToRadians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return distance;
        }

        public static double Bearing(Point position1, Point position2)
        {
            return Bearing(position1.longitude, position1.latitude, position2.longitude, position2.latitude);
        }

        public static double Bearing(double long10, double lat10, double long20, double lat20)
        {
            // see https://danielsaidi.com/blog/2011/02/04/calculate-distance-and-bearing-between-two-positions
            // Bearing for a Great circle = Orthodrome
            // returns: angle in degrees

            var lat1 = DegreeToRadians(lat10);
            var lat2 = DegreeToRadians(lat20);
            var long2 = DegreeToRadians(long20);
            var long1 = DegreeToRadians(long10);
            var dLon = long1 - long2;

            var y = Math.Sin(dLon) * Math.Cos(lat2);
            var x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);
            var brng = Math.Atan2(y, x);

            return (RadiansToDegree(brng) + 360) % 360;
        }

        #endregion

        #region Common conversion functions (Radians<->Degrees, km<->NM)
        public static double DegreeToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public static double RadiansToDegree(double angle)
        {
            return 180.0 * angle / Math.PI;
        }
        public static double KmToNM(double kilometer)
        {
            return kilometer / 1.852;
        }
        public static double NMToKm(double nauticalMile)
        {
            return nauticalMile * 1.852;
        }

        #endregion

        #region Bearing and Length for each segment of a parcour (TEST)

        public static void myTEST()
        {
            // Kuhmo N out inverted. Route A

            double[] LstLat = new double[7] {
                64.22404117082263,
                64.24837141385511,
                64.2408356421904,
                64.20991020112726,
                64.1825644288248,
                64.18579212670649,
                64.12153469598418
                };

            double[] LstLon = new double[7] {
                29.28607196162505,
                29.36181028089516,
                29.4594823361775,
                29.49993082091178,
                29.59370697671723,
                29.67134469883853,
                29.67097243977683
                };


            double totalDistance = 0.0;
            List<double> LstDistance = new List<double>();
            List<double> LstBearing = new List<double>();

            for (int i = 0; i < LstLon.Length - 1; i++)
            {
                double lat1 = LstLat[i];
                double lon1 = LstLon[i];
                double lat2 = LstLat[i + 1];
                double lon2 = LstLon[i + 1];

                double dist = KmToNM(Distance(lon1, lat1, lon2, lat2));
                LstDistance.Add(dist);
                LstBearing.Add(Bearing(lon1, lat1, lon2, lat2));
                totalDistance += dist;
            }
            double dd = totalDistance;
        }


        #endregion

        #region Correction factors used in approximative calculations

        public double LongitudeCorrFactor(Line l)
        {
            return Math.Cos(Math.PI * (l.A.latitude + l.B.latitude) / 2 / 180.0);
        }
        public double LongitudeCorrFactor(Point midPoint)
        {
            return Math.Cos(Math.PI * midPoint.latitude / 180.0);
        }

        #endregion
        public Point PointForRadius(Point CenterPoint)
        {
            // fixed line width for start and end line is 0.6 NM
            // radius should be 0.3 NM
            // 60.0 NM equals to 1.00 degree in latitude difference
            // 0.3 NM equals to 0.3 * 1/60 degree in latitude difference
            const double diff = 1.0 / 60.0 * 0.3;
            Point x = new Point();
            x.latitude = CenterPoint.latitude - diff;
            x.longitude = CenterPoint.longitude;
            return x;
        }

        #region UNUSED (disabled) / Conversion Swiss coordinates

        // Convert CH y/x to WGS lat
        public static double CHtoWGSlat(double y, double x)
        {
            // Converts militar to civil and  to unit = 1000km
            // Axiliary values (% Bern)
            double y_aux = (y - 600000) / 1000000;
            double x_aux = (x - 200000) / 1000000;

            // Process lat
            double lat = 16.9023892
                + 3.238272 * x_aux
                - 0.270978 * Math.Pow(y_aux, 2)
                - 0.002528 * Math.Pow(x_aux, 2)
                - 0.0447 * Math.Pow(y_aux, 2) * x_aux
                - 0.0140 * Math.Pow(x_aux, 3);

            // Unit 10000" to 1 " and converts seconds to degrees (dec)
            lat = lat * 100 / 36;

            return lat;
        }
        // Convert CH y/x to WGS long
        public static double CHtoWGSlng(double y, double x)
        {
            // Converts militar to civil and  to unit = 1000km
            // Axiliary values (% Bern)
            double y_aux = (y - 600000) / 1000000;
            double x_aux = (x - 200000) / 1000000;

            // Process long
            double lng = 2.6779094
                + 4.728982 * y_aux
                + 0.791484 * y_aux * x_aux
                + 0.1306 * y_aux * Math.Pow(x_aux, 2)
                - 0.0436 * Math.Pow(y_aux, 3);

            // Unit 10000" to 1 " and converts seconds to degrees (dec)
            lng = lng * 100 / 36;

            return lng;
        }

        public static double WGStoChEastY(double longitude, double latitude)
        {
            return ApproxSwissProj.WGStoCHy(latitude, longitude);
        }
        public static double WGStoChNorthX(double longitude, double latitude)
        {
            return ApproxSwissProj.WGStoCHx(latitude, longitude);
        } 
        #endregion
    }
}
