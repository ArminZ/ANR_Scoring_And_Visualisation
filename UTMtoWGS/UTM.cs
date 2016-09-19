using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UTMtoWGS
{
    public static class UTMConverter
    {
        private const double PI = Math.PI;
        private const double deg2rad = PI / 180;
        private const double rad2deg = 180.0 / PI;
        private const double radius = 6378137;
        private const double eccSquared = 0.00669438;
        
        /// <summary>
        /// converts UTM coords to lat/long.  Equations from USGS Bulletin 1532 
        ///	East Longitudes are positive, West longitudes are negative. 
        ///	North latitudes are positive, South latitudes are negative
        ///	Lat and Long are in decimal degrees. 
        ///	Written by Chuck Gantz- chuck.gantz@globalstar.com
        /// </summary>
        private static void UTMtoLL(double UTMNorthing, double UTMEasting, int ZoneNumber, bool southhemi, out double Lat, out double Long)
        {
            double k0 = 0.9996;
            double eccPrimeSquared;
            double e1 = (1 - Math.Sqrt(1 - eccSquared)) / (1 + Math.Sqrt(1 - eccSquared));
            double N1, T1, C1, R1, D, M;
            double LongOrigin;
            double mu, phi1, phi1Rad;
            double x, y;

            x = UTMEasting - 500000.0; //remove 500,000 meter offset for longitude
            y = UTMNorthing;

            if (!southhemi)
            {
                //NorthernHemisphere = 1;//point is in northern hemisphere
            }
            else
            {
                //NorthernHemisphere = 0;//point is in southern hemisphere
                y -= 10000000.0;//remove 10,000,000 meter offset used for southern hemisphere
            }

            LongOrigin = (ZoneNumber - 1) * 6 - 180 + 3;  //+3 puts origin in middle of zone

            eccPrimeSquared = (eccSquared) / (1 - eccSquared);

            M = y / k0;
            mu = M / (radius * (1 - eccSquared / 4 - 3 * eccSquared * eccSquared / 64 - 5 * eccSquared * eccSquared * eccSquared / 256));

            phi1Rad = mu + (3 * e1 / 2 - 27 * e1 * e1 * e1 / 32) * Math.Sin(2 * mu)
                            + (21 * e1 * e1 / 16 - 55 * e1 * e1 * e1 * e1 / 32) * Math.Sin(4 * mu)
                            + (151 * e1 * e1 * e1 / 96) * Math.Sin(6 * mu);
            phi1 = phi1Rad * rad2deg;

            N1 = radius / Math.Sqrt(1 - eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad));
            T1 = Math.Tan(phi1Rad) * Math.Tan(phi1Rad);
            C1 = eccPrimeSquared * Math.Cos(phi1Rad) * Math.Cos(phi1Rad);
            R1 = radius * (1 - eccSquared) / Math.Pow(1 - eccSquared * Math.Sin(phi1Rad) * Math.Sin(phi1Rad), 1.5);
            D = x / (N1 * k0);

            Lat = phi1Rad - (N1 * Math.Tan(phi1Rad) / R1) * (D * D / 2 - (5 + 3 * T1 + 10 * C1 - 4 * C1 * C1 - 9 * eccPrimeSquared) * D * D * D * D / 24
                            + (61 + 90 * T1 + 298 * C1 + 45 * T1 * T1 - 252 * eccPrimeSquared - 3 * C1 * C1) * D * D * D * D * D * D / 720);
            Lat = Lat * rad2deg;

            Long = (D - (1 + 2 * T1 + C1) * D * D * D / 6 + (5 - 2 * C1 + 28 * T1 - 3 * C1 * C1 + 8 * eccPrimeSquared + 24 * T1 * T1)
                            * D * D * D * D * D / 120) / Math.Cos(phi1Rad);
            Long = LongOrigin + Long * rad2deg;
        }

        internal static double[] getLatLon(int zone, double UTMNorthing, double UTMEasting, bool southhemi)
        {
            double lat = 0f;
            double lng = 0f;
            UTMtoLL(UTMNorthing, UTMEasting, zone, southhemi, out lat, out lng);
            return new double[]{lat,lng};
        }
    }
}
