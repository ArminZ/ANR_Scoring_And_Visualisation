using SharpKml.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AirNavigationRaceLive.Comps.ANRRouteGenerator
{
    public class GeoData
    {

        private string TurnDirection(double Phi1, double Phi2)
        {
            string turnDirection = string.Empty;

            if (Phi2 > Phi1)
            {
                turnDirection = "R";
            }

            if (Phi2 < Phi1)
            {
                turnDirection = "L";
            }

            if (Phi2 - Phi1 > 180.00)
            {
                turnDirection = "L";
            }

            if (Phi1 - Phi2 > 180.00)
            {
                turnDirection = "R";
            }

            return turnDirection;
        }

        public static double Corr(double Y1, double Y2)
        {
            // latitude scale correction factor, based on averaged latitude
            return Math.Cos((Y1 + Y2) / 2.00 * Math.PI / 180.00);
        }

        public double CalcHeading(Vector point, Vector vectorPrev)
        {
            double calcHeading;
            double X = point.Longitude;
            double Y = point.Latitude;

            double Xprev = vectorPrev.Longitude;
            double Yprev = vectorPrev.Latitude;

            if (Y == Yprev)
            {

                if (X == Xprev)
                {
                    calcHeading = 90.00;
                }
                else
                {
                    calcHeading = 270.00;
                }
            }
            else
            {
                if (Y > Yprev)
                {
                    calcHeading = Math.Atan((X - Xprev) * Corr(Y, Yprev) / (Y - Yprev)) * 180.00 / Math.PI;
                }
                else
                {
                    calcHeading = Math.Atan((X - Xprev) * Corr(Y, Yprev) / (Y - Yprev)) * 180.00 / Math.PI + 180.00;
                }

            }
            if (calcHeading < 0)
            {
                calcHeading = calcHeading + 360.00;
            }
            return calcHeading;
        }

        public List<double> CalculateHeadings(List<Vector> trackPoints)
        {
            List<double> lstHeadings = new List<double>();


            for (int i = 1; i < trackPoints.Count; i++)
            {

                double calcHeading;
                double X = trackPoints[i].Longitude;
                double Y = trackPoints[i].Latitude;

                double Xprev = trackPoints[i - 1].Longitude;
                double Yprev = trackPoints[i - 1].Latitude;

                if (Y == Yprev)
                {

                    if (X == Xprev)
                    {
                        calcHeading = 90.00;
                    }
                    else
                    {
                        calcHeading = 270.00;
                    }
                }
                else
                {
                    if (Y > Yprev)
                    {
                        calcHeading = Math.Atan((X - Xprev) * Corr(Y, Yprev) / (Y - Yprev)) * 180.00 / Math.PI;
                    }
                    else
                    {
                        calcHeading = Math.Atan((X - Xprev) * Corr(Y, Yprev) / (Y - Yprev)) * 180.00 / Math.PI + 180.00;
                    }

                }
                if (calcHeading < 0)
                {
                    calcHeading = calcHeading + 360.00;
                }

                if (i == 1)
                {
                    // heading before the first point is the same as for the first point 
                    lstHeadings.Add(calcHeading);
                }

                lstHeadings.Add(calcHeading);
            }
            // heading after the last point is the same as for the last point
            lstHeadings.Add(lstHeadings[trackPoints.Count - 1]);
            return lstHeadings;
        }

        public Vector CalculateInnerCurvePoint(Vector point, double Phi1, double Phi2, double R)
        {
            /***
             * 
             * Calculate the intersection point on the "inner side" of a curve for a channel
             * R: Radius of circle/channel width
             * X, Y: coordinate of center line point. X is the longitudinal East/West direction, Y is the latitudinal North/South direction
             * TH1, TH2: angle of actual and following leg. TH North = 0.
             * 
             * calculate the intersection point of the two border lines
             * calculate first R1 (using Pythagoras), then calculate the circle point with given angle (phi2-phi1)/2
            ***/
            double r1, d1;
            double xVal, yVal;
            double X = point.Longitude;
            double Y = point.Latitude;


            Vector vr = new Vector();

            d1 = R * Math.Tan(Math.PI / 180.00 * (Phi2 - Phi1) / 2.00);
            r1 = Math.Sqrt(R * R + d1 * d1);

            if (Phi2 > Phi1)
            {
                // right turn, calculate right border point
                xVal = X + r1 / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
                yVal = Y - r1 / 60.00 * Math.Sin(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
            }
            else
            {
                // left turn, calculate left border point
                xVal = X - r1 / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
                yVal = Y + r1 / 60.00 * Math.Sin(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
            }

            vr.Latitude = yVal;
            vr.Longitude = xVal;
            return vr;

        }
        public Vector CalculateOuterCurvePoint(Vector point, double Phi1, double Phi2, double R)
        {
            /***
             * 
             * Calculate the intersection point on the "inner side" of a curve for a channel
             * R: Radius of circle/channel width
             * X, Y: coordinate of center line point. X is the longitudinal East/West direction, Y is the latitudinal North/South direction
             * TH1, TH2: angle of actual and following leg. TH North = 0.
             * 
             * calculate the intersection point of the two border lines
             * calculate first R1 (using Pythagoras), then calculate the circle point with given angle (phi2-phi1)/2
            ***/
            double r1, d1;
            double xVal, yVal;
            double X = point.Longitude;
            double Y = point.Latitude;


            Vector vr = new Vector();

            d1 = R * Math.Tan(Math.PI / 180.00 * (Phi2 - Phi1) / 2.00);
            r1 = Math.Sqrt(R * R + d1 * d1);

            if (Phi2 > Phi1)
            {
                // right turn, calculate left border point
                xVal = X - r1 / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
                yVal = Y + r1 / 60.00 * Math.Sin(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
            }
            else
            {
                // left turn, calculate right border point
                xVal = X + r1 / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
                yVal = Y - r1 / 60.00 * Math.Sin(Math.PI / 180.00 * (Phi1 + Phi2) / 2.00);
            }

            vr.Latitude = yVal;
            vr.Longitude = xVal;
            return vr;

        }

        public List<Vector> CalculateOuterCurveArc(Vector point, double Phi1, double Phi2, double R, int DeltaArc)
        {
            double X = point.Longitude;
            double Y = point.Latitude;
            double xVal, yVal;
            List<Vector> vrs = new List<Vector>();

            if (Phi2 - Phi1 > 0)
            {
                for (int i = Convert.ToInt32(Phi1) + DeltaArc; i < Convert.ToInt32(Phi2); i += Math.Sign(Convert.ToInt32(Phi2 - Phi1)) * DeltaArc)
                {
                    xVal = X - R / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * i);
                    yVal = Y + R / 60.00 * Math.Sin(Math.PI / 180.00 * i);
                    Vector vr = new Vector(yVal, xVal);
                    vrs.Add(vr);
                }
            }
            else if (Phi2 - Phi1 < 0)
            {
                for (int i = Convert.ToInt32(Phi1) - DeltaArc; i > Convert.ToInt32(Phi2); i -= DeltaArc)
                {
                    xVal = X + R / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * i);
                    yVal = Y - R / 60.00 * Math.Sin(Math.PI / 180.00 * i);
                    Vector vr = new Vector(yVal, xVal);
                    vrs.Add(vr);
                }
            }
            else
            {
                xVal = X + R / 60.00 / Corr(Y, Y) * Math.Cos(Math.PI / 180.00 * Phi1);
                yVal = Y - R / 60.00 * Math.Sin(Math.PI / 180.00 * Phi1);
                Vector vr = new Vector(yVal, xVal);
                vrs.Add(vr);
            }
            return vrs;

        }

        public List<Vector> CalculateCurvePoint(List<Vector> points, List<double> headings, double R, bool isRightHandBorderCalculation, int DeltaArc)
        {
            List<Vector> lstVector = new List<Vector>();

            for (int i = 0; i < points.Count; i++)
            {
                Vector point = points[i];

                double X = point.Longitude;
                double Y = point.Latitude;
                double Phi1, Phi2;

                Phi1 = headings[i];
                Phi2 = headings[i + 1];

                double PhiA, PhiB;
                PhiA = Phi1;
                PhiB = Phi2;


                // Handle cases where we cross the 360-degrees during the turn


                if (TurnDirection(Phi1, Phi2) == "R" && Phi1 - Phi2 > 180.00)
                {
                    //old heading 355, new heading 030
                    PhiA = Phi1;
                    PhiB = Phi2 + 360.00;
                }

                if (TurnDirection(Phi1, Phi2) == "L" && Phi2 - Phi1 > 180.00)
                {
                    // old heading 040, new heading 340
                    PhiA = Phi1 + 360.00;
                    PhiB = Phi2;
                }

                if (isRightHandBorderCalculation)
                {
                    //Right border
                    if (TurnDirection(Phi1, Phi2) == "R")
                    {
                        //Right turn
                        lstVector.Add(CalculateInnerCurvePoint(point, PhiA, PhiB, R));
                    }
                    else
                    {
                        // left fturn
                        lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));
                    }
                }
                else
                {
                    //left border
                    if (TurnDirection(Phi1, Phi2) == "R")
                    {
                        // Right turn
                        lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));
                    }
                    else
                    {
                        // Left turn
                        lstVector.Add(CalculateInnerCurvePoint(point, PhiA, PhiB, R));
                    }
                }
            }
            return lstVector;
        }

        private int orientation(Vector p, Vector q, Vector r)
        {
            // see http://www.geeksforgeeks.org/check-if-two-given-line-segments-intersect/
            // See 10th slides from following link for derivation of the formula
            // http://www.dcs.gla.ac.uk/~pat/52233/slides/Geometry1x1.pdf
            double val = (q.Latitude - p.Latitude) * (r.Longitude - q.Longitude) -
                      (q.Longitude - p.Longitude) * (r.Latitude - q.Latitude);

            if (val == 0) return 0;  // colinear

            return (val > 0) ? 1 : 2; // clock or counterclock wise
        }

        // Given three colinear points p, q, r, the function checks if
        // point q lies on line segment 'pr'
        private bool onSegment(Vector p, Vector q, Vector r)
        {
            if (q.Longitude <= Math.Min(p.Longitude, r.Longitude) && q.Longitude >= Math.Min(p.Longitude, r.Longitude) &&
                q.Latitude <= Math.Max(p.Latitude, r.Latitude) && q.Latitude >= Math.Min(p.Latitude, r.Latitude))
                return true;

            return false;
        }

        // The main function that returns true if line segment 'p1q1'
        // and 'p2q2' intersect.
        public bool DoIntersect(Vector p1, Vector q1, Vector p2, Vector q2)
        {
            // Find the four orientations needed for general and
            // special cases
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and p2 are colinear and q2 lies on segment p1q1
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases
        }

        public bool DoIntersect(List<Vector> p1q1, List<Vector> p2q2)
        {
            Vector p1 = p1q1[0];
            Vector q1 = p1q1[1];
            Vector p2 = p2q2[0];
            Vector q2 = p2q2[1];
            return DoIntersect(p1, q1, p2, q2);
        }

        // TODO:  
        // add the intersection point
        // remove all points back to the first non-intersecting point (start point of the intersecting line)

        public Vector IntersectionPoint(List<Vector> p1q1, List<Vector> p2q2)
        {

            // http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=geometry2#line_line_intersection
            // note: must be checked

            Vector p1 = p1q1[0];
            Vector q1 = p1q1[1];
            Vector p2 = p2q2[0];
            Vector q2 = p2q2[1];

            Vector retPt = new Vector();

            double A1 = q1.Latitude - p1.Latitude;
            double B1 = p1.Longitude - q1.Longitude;
            double C1 = A1 * p1.Longitude + B1 * p1.Latitude;
            double A2 = q2.Latitude - p2.Latitude;
            double B2 = p2.Longitude - q2.Longitude;
            double C2 = A2 * p2.Longitude + B2 * p2.Latitude;

            double det = A1 * B2 - A2 * B1;
            if (det == 0)
            {
                //Lines are parallel
            }
            else
            {
                retPt.Longitude = (B2 * C1 - B1 * C2) / det;
                retPt.Latitude = (A1 * C2 - A2 * C1) / det;
            }

            return retPt;
        }


        private static readonly Regex CoordinateRegex = new Regex(@"^\d{6}[N]\d{7}[E]");
        public Vector ConvertToDecimalCoord(string str)
        { // convert from format "603245N0251432E" to decimal format 60.yyyyy 25.xxxxxx
            double lat = double.Parse(str.Substring(0, 2)) + double.Parse(str.Substring(2, 2)) / 60.0 + double.Parse(str.Substring(4, 2)) / 3600.0;
            double lon = double.Parse(str.Substring(7, 3)) + double.Parse(str.Substring(10, 2)) / 60.0 + double.Parse(str.Substring(12, 2)) / 3600.0;
            return new Vector(lat, lon);
        }


        public bool isCoordinate(string str)
        {
            return CoordinateRegex.IsMatch(str);
        }


        public List<string> getAltitudeLimits(string str, bool forOpenAIP)
        {
            string altitudeText = string.Empty;
            string strCleaned = string.Empty;

            //string str = " FL 195 1300 FT AMSL ";
            //str = " FL 195 FL 60 ";
            //str = " FL 195 SFC ";
            //str = " UNL FL 95 ";
            //str = " UNL 1300 FT AMSL ";
            //str = " 1300 FT AMSL SFC ";
            //str = " 1000 FT AGL SFC ";
            //str = " 2500 FT AMSL 1300 FT AMSL ";
            //str = " FL 195    2500 FT AMSL ";


            strCleaned = str.Trim();
            altitudeText = strCleaned;
            strCleaned = strCleaned.Replace("  ", " ");
            strCleaned = strCleaned.Replace("FT AGL", "FTAGL");
            strCleaned = strCleaned.Replace("FT MSL", "FTMSL");
            strCleaned = strCleaned.Replace("FTAGL ", "FTAGL|");
            strCleaned = strCleaned.Replace("FTMSL ", "FTMSL|");
            strCleaned = strCleaned.Replace("UNL ", "UNL|");
            strCleaned = strCleaned.Replace(" SFC", "|SFC");
            strCleaned = strCleaned.Replace(" FL", "|FL");
            strCleaned = strCleaned.Replace("||", "|");

            if (!strCleaned.Contains('|'))
            {
                // format is 'FL 95 2500 FTAMSL' or 'FL 95 2500 FTAGL' 

                if (strCleaned.StartsWith("FL "))
                {
                    StringBuilder sb = new StringBuilder(strCleaned);
                    strCleaned = sb.Remove(0, 3).ToString();
                    int idx = strCleaned.IndexOf(" ");
                    sb = new StringBuilder(strCleaned);
                    sb.Remove(idx, 1);
                    strCleaned = sb.Insert(idx, "|").ToString();
                    strCleaned = "FL " + strCleaned;
                }
            }

            //str = " FL 195|FL 60";
            //str = " FL 195|SFC";
            //str = " UNL|FL 95";
            //str = " UNL|1300 FTAMSL";
            //str = " 1300 FTAMSL|SFC";
            //str = " 1300 FTAGL|SFC";
            //str = " 2500 FTAMSL|1300 FTAMSL";
            //str = " FL 195|2500 FTAMSL";


            string[] arr = strCleaned.Split('|');

            double upper = getAltitude(arr[0]);
            double lower = getAltitude(arr[1]);
            int upperType = getAltitudeType(arr[0]);
            int lowerType = upperType;

            if (forOpenAIP)
            {
                string strRU, strRL, strUU, strUL, strVU, strVL;
                altitudeText = "<ALTLIMIT_TOP REFERENCE='{0}'><ALT UNIT='{1}'>{2}</ALT></ALTLIMIT_TOP><ALTLIMIT_BOTTOM REFERENCE='{3}'><ALT UNIT='{4}'>{5}</ALT></ALTLIMIT_BOTTOM>";
                // make a text that again can be parsed later on used as XML for the OpenAIP format

                if (arr[0].StartsWith("FL") || arr[0].StartsWith("UNL"))
                {
                    strRU = "STD";
                    strUU = "FL";
                    strVU = arr[0].Replace("FL", "").Trim();
                    if (arr[0].StartsWith("UNL"))
                    {
                        strVU = "9999";
                    }
                }
                else
                {
                    strRU = "MSL";
                    strUU = "F";
                    strVU = arr[0].Split(' ')[0];
                }
                if (arr[1].StartsWith("FL") || arr[1].StartsWith("SFC"))
                {
                    if (arr[1].StartsWith("SFC"))
                    {
                        strRL = "GND";
                        strUL = "F";
                        strVL = "0";
                    }
                    else
                    {
                        strRL = "STD";
                        strUL = "FL";
                        strVL = arr[1].Replace("FL", "").Trim();
                    }

                }
                else
                {
                    strRL = "MSL";
                    strUL = "F";
                    strVL = arr[1].Split(' ')[0];
                }

                altitudeText = String.Format(altitudeText, strRU, strUU, strVU, strRL, strUL, strVL);

            }

            List<string> lst = new List<string>();
            lst.Add(altitudeText);
            lst.Add(upper.ToString());
            lst.Add(lower.ToString());
            lst.Add(upperType.ToString());
            lst.Add(lowerType.ToString());

            return lst;

        }

        public double getAltitude(string str, double AltitudeUNL)
        {
            const double MAX = 130000.0;
            //str = " FL 195|FL 60";
            //str = " FL 195|SFC";
            //str = " UNL|FL 95";
            //str = " UNL|1300 FTAMSL";
            //str = " 1300 FTAMSL|SFC";
            //str = " 2500 FTAMSL|1300 FTAMSL";
            //str = " FL 195|2500 FTAMSL";

            // split by "|"

            if (str == "UNL")
            {
                return MAX;
            }
            if (str == "SFC")
            {
                return 0.0;
            }

            if (str.StartsWith("FL "))
            {
                double ret = 0.0;
                bool rt = double.TryParse(str.Substring(3, str.Length - 3), out ret);
                if (ret > 0.0) ret = ret * 100.0 / 3.28084;

                return ret;
            }

            if (str.EndsWith(" FTMSL") || str.EndsWith(" FTAGL"))
            {
                double ret = 0.0;
                //string xx = str.Remove(str.Length - 7);
                bool rt = double.TryParse(str.Remove(str.Length - 6), out ret);
                if (ret > 0.0) ret = ret / 3.28084;
                return ret;
            }


            return 0.0;
        }
        public double getAltitude(string str)
        {
            const double MAX = 130000.0;
            return getAltitude(str, MAX);
        }

        public int getAltitudeType(string str)
        {
            // decide if AltitudeMode.RelativeToGround or AltitudeMode.Asolute
            // NOTE: this can work only 
            // -if the upper layer and lower layer have the same mode (either absolute or relative to ground)
            // -lower layer is surface
            // means
            // FL95 1000 FT AGL will NOT work (upper layer absolute, lower layer relative to ground)
            // 1000 FT AGL SFC  will work (upper layer reative, lower layer can also relative)


            //str = " FL 195|FL 60";
            //str = " FL 195|SFC";
            //str = " UNL|FL 95";
            //str = " UNL|1300 FTAMSL";
            //str = " 1300 FTAGL|SFC";
            //str = " 1300 FTAMSL|SFC";
            //str = " 2500 FTAMSL|1300 FTAMSL";
            //str = " FL 195|2500 FTAMSL";
            //str = " FL 195|2500 FTAGL";

            // split by "|"

            // set altitude mode

            if (str.EndsWith("AGL"))
            {
                return 1; // AltitudeMode.RelativeToGround;
            }
            return 2;  // AltitudeMode.Asolute;
        }

        public static double GetDist(Vector Vector1, Vector Vector2)
        {
            // Calculates Distance between two coordinate points

            double DLat = Vector1.Latitude - Vector2.Latitude;
            double DLon = Vector1.Longitude - Vector2.Longitude;
            // correction for average Latitude
            DLon = Corr(Vector1.Latitude, Vector2.Latitude) * DLon;
            return Math.Sqrt(DLat * DLat + DLon * DLon);
        }

        public static Vector GetIntersectPoint(Vector Vector1, Vector Vector2, double d)
        {
            // Calculates Distance between two coordinate points
            double Dist = GetDist(Vector1, Vector2);
            // 1 degree equals 60 NM on the vertical coordinate axis
            Dist = Dist * 60.0;
            double dLat = Vector1.Latitude + d / Dist * (Vector2.Latitude - Vector1.Latitude);
            double dLon = Vector1.Longitude + d / Dist * (Vector2.Longitude - Vector1.Longitude);
            return new Vector(dLat, dLon);
        }

        public List<Vector> BorderVectorWithIntersect(List<Vector> lstVectors1, double d)
        {
            // input: original channel border lines, start line to final line.
            // this function shifts the border line for the PROH areas from the start line and end line about 0.4 NM "inwards".
            // the original border 'edge' points are replaced with the new ones
            //const double d = 0.4;
            List<Vector> lstVct = new List<Vector>();

            //lstVct.Add(lstVectors1[0]);
            lstVct.Add(GetIntersectPoint(lstVectors1[0], lstVectors1[1], d));
            for (int i = 1; i < lstVectors1.Count - 1; i++)
            {
                lstVct.Add(lstVectors1[i]);
            }
            lstVct.Add(GetIntersectPoint(lstVectors1[lstVectors1.Count - 1], lstVectors1[lstVectors1.Count - 2], d));
            //lstVct.Add(lstVectors1[lstVectors1.Count - 1]);
            return lstVct;
        }
    }

    public class GeoAngle
    {
        public bool IsNegative { get; set; }
        public int Degrees { get; set; }
        public int Minutes { get; set; }
        public int Seconds { get; set; }
        public int Milliseconds { get; set; }

        public GeoAngle FromDouble(double angleInDegrees)
        {
            //ensure the value will fall within the primary range [-180.0..+180.0]
            while (angleInDegrees < -180.0)
                angleInDegrees += 360.0;

            while (angleInDegrees > 180.0)
                angleInDegrees -= 360.0;

            var result = new GeoAngle();

            //switch the value to positive
            result.IsNegative = angleInDegrees < 0;
            angleInDegrees = Math.Abs(angleInDegrees);

            //gets the degree
            result.Degrees = (int)Math.Floor(angleInDegrees);
            var delta = angleInDegrees - result.Degrees;

            //gets minutes and seconds
            var seconds = (int)Math.Floor(3600.0 * delta);
            result.Seconds = seconds % 60;
            result.Minutes = (int)Math.Floor(seconds / 60.0);
            delta = delta * 3600.0 - seconds;

            //gets fractions
            result.Milliseconds = (int)(1000.0 * delta);

            return result;
        }

        public override string ToString()
        {
            var degrees = this.IsNegative
                ? -this.Degrees
                : this.Degrees;

            //return string.Format(
            //    "{0}° {1:00}' {2:00}\"",
            //    degrees,
            //    this.Minutes,
            //    this.Seconds);

            return string.Format(
    "{0}{1:00}{2:00}",
    degrees,
    this.Minutes,
    this.Seconds);
        }

        public string ToString(string format)
        {
            switch (format)
            {
                case "NS":
                    //return string.Format(
                    //    "{0}° {1:00}' {2:00}\".{3:000} {4}",
                    //    this.Degrees,
                    //    this.Minutes,
                    //    this.Seconds,
                    //    this.Milliseconds,
                    //    this.IsNegative ? 'S' : 'N');

                    return string.Format(
                        "{0}{1:00}{2:00}{3}",
                        this.Degrees,
                        this.Minutes,
                        this.Seconds,
                        this.IsNegative ? 'S' : 'N');

                case "WE":
                    //return string.Format(
                    //    "{0}° {1:00}' {2:00}\".{3:000} {4}",
                    //    this.Degrees,
                    //    this.Minutes,
                    //    this.Seconds,
                    //    this.Milliseconds,
                    //    this.IsNegative ? 'W' : 'E');
                    return string.Format(
                        "{0:000}{1:00}{2:00}{3}",
                        this.Degrees,
                        this.Minutes,
                        this.Seconds,
                        this.IsNegative ? 'W' : 'E');

                default:
                    throw new NotImplementedException();
            }
        }

        public string ConvertToStringCoord(Vector vct)
        { // converts a coordinate point from decimal format 60.yyyyy 25.xxxxxx to format "603245N 0251432E"

            GeoAngle ga = new GeoAngle();
            return ga.FromDouble(vct.Latitude).ToString("NS") + " " + ga.FromDouble(vct.Longitude).ToString("WE"); ;
        }

    }
}
