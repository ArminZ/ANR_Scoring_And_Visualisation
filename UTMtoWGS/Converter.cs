using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using swisstopo.geodesy.gpsref;

namespace UTM
{
    public class Converter
    {
        public static double Distance(double long1, double lat1, double long2, double lat2)
        {
            //The haversine formula
            //a = sin²(Δlat/2) + cos(lat1)*cos(lat2)*sin²(Δlong/2)
            //c = 2.atan2(√a, √(1−a))
            //d = R*c
            double deltaLong = DegreeToRadian(long2 - long1);
            double deltaLat = DegreeToRadian(lat2 - lat1);
            double lat1Rad = DegreeToRadian(lat1);
            double lat2Rad = DegreeToRadian(lat2);
            double a_1 = Math.Sin(deltaLat / 2);
            double a_2 = a_1 * a_1;
            double a_3 = Math.Cos(lat1Rad) * Math.Cos(lat2Rad);
            double a_4 = Math.Sin(deltaLong / 2);
            double a_5 = a_4 * a_4;
            double a = a_2 + a_3 * a_5;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double R = 6371; //Earth Radius
            double distance = R * c;
            return distance;
        }

        public static double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public static double MtoNM(double meter)
        {
            return meter/1.852;
        }
        public static double NMtoM(double nauticMiles)
        {
            return nauticMiles * 1.852;
        }
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
    }
}
