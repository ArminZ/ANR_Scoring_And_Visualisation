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

        /// <summary>
        /// Latitude scale correction factor, based on averaged latitude
        /// </summary>
        /// <param name="Y1"></param>
        /// <param name="Y2"></param>
        /// <returns></returns>
        public static double Corr(double Y1, double Y2)
        {
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

        public List<Vector> CalculateCurvePoint(List<Vector> points, List<double> headings, double R, bool isRightHandBorderCalculation, int DeltaArc, bool hasRoundedCorners)
        {
            // REV 2021-07-19 hasRoundedCorners implemented

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
                        //lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));

                        if (hasRoundedCorners)
                        {
                            lstVector.AddRange(CalculateOuterCurveArc(point, PhiA, PhiB, R, DeltaArc));
                        }
                        else
                        {
                            lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));
                        }
                    }
                }
                else
                {
                    //left border
                    if (TurnDirection(Phi1, Phi2) == "R")
                    {
                        // Right turn
                        // lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));

                        if (hasRoundedCorners)
                        {
                            lstVector.AddRange(CalculateOuterCurveArc(point, PhiA, PhiB, R, DeltaArc));
                        }
                        else
                        {
                            lstVector.Add(CalculateOuterCurvePoint(point, PhiA, PhiB, R));
                        }
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

        /// <summary>
        /// Calculates Distance between two coordinate points
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <returns></returns>
        public static double GetDist(Vector Vector1, Vector Vector2)
        {
            double DLat = Vector1.Latitude - Vector2.Latitude;
            double DLon = Vector1.Longitude - Vector2.Longitude;
            // correction for average Latitude
            DLon = Corr(Vector1.Latitude, Vector2.Latitude) * DLon;
            return Math.Sqrt(DLat * DLat + DLon * DLon);
        }

        /// <summary>
        /// Calculates Distance between two coordinate points
        /// </summary>
        /// <param name="Vector1"></param>
        /// <param name="Vector2"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static Vector GetIntersectPoint(Vector Vector1, Vector Vector2, double d)
        {
            double Dist = GetDist(Vector1, Vector2);
            // 1 degree equals 60 NM on the vertical coordinate axis
            Dist = Dist * 60.0;
            double dLat = Vector1.Latitude + d / Dist * (Vector2.Latitude - Vector1.Latitude);
            double dLon = Vector1.Longitude + d / Dist * (Vector2.Longitude - Vector1.Longitude);
            return new Vector(dLat, dLon);
        }

        /// <summary>
        /// Returns the intersection point of the route center line with the (hand-designed) NB Line.
        /// </summary>
        /// <param name="routePoints"></param>
        /// <param name="NBLpoints"></param>A manually definition point for the NBLine, close to the route canter line
        /// <returns></returns>
        public Vector NBLPointOnRouteLine(List<Vector> routePoints, List<Vector> NBLpoints, out int idx)
        {
            // Note: we may have several intersections depending on the shape of the route
            // so check that the instersection point is on both line segments (the original NBL and the route section)
            // ensure that we take the last valid intersection 
            Vector nblPoint = null;
            idx = -1;
            LineSegment lsNBL = new LineSegment(NBLpoints[0], NBLpoints[1]);
            for (int i = 0; i < routePoints.Count - 1; i++)
            {
                LineSegment ls = new LineSegment(routePoints[i], routePoints[i + 1]);

                Vector pointInters = ls.IntersectionPoint(lsNBL);
                if (ls.IsOnSegment(pointInters) && lsNBL.IsOnSegment(pointInters))
                {
                    idx = i;
                    nblPoint = pointInters;
                }
            }
            return nblPoint;
        }

        public List<Vector> BorderModification(List<Vector> lstVectors1, double d)
        {
            // input: original channel border lines, start line to final line.
            // this function shifts the border line for the PROH areas from the start line and end line about 0.4 NM "inwards".
            // the original border 'edge' points are replaced with the new ones
            //const double d = 0.4;
            List<Vector> lstVct = new List<Vector>();

            lstVct.Add(GetIntersectPoint(lstVectors1[0], lstVectors1[1], d));
            for (int i = 1; i < lstVectors1.Count - 1; i++)
            {
                lstVct.Add(lstVectors1[i]);
            }
            lstVct.Add(GetIntersectPoint(lstVectors1[lstVectors1.Count - 1], lstVectors1[lstVectors1.Count - 2], d));
            return lstVct;
        }
    }

    public class LineSegment
    {
        public Vector startPt, endPt;

        // constructor
        public LineSegment(Vector startPt, Vector endPt)
        {
            this.startPt = startPt;
            this.endPt = endPt;
        }

        /// <summary>
        /// Calculate the intersection point with the line segment linSeg. 
        /// Note that for non-prallel line segments, there will always be an intersection point
        /// </summary>
        /// <param name="linSeg"></param>
        /// <returns></returns>
        public Vector IntersectionPoint(LineSegment linSeg)
        {

           //  http://community.topcoder.com/tc?module=Static&d1=tutorials&d2=geometry2#line_line_intersection
          //   note: must be checked

            Vector p1 = this.startPt;
            Vector q1 = this.endPt;
            Vector p2 = linSeg.startPt;
            Vector q2 = linSeg.endPt;

            Vector retPt = new Vector();

            double A1 = q1.Latitude - p1.Latitude;
            double B1 = p1.Longitude - q1.Longitude;
            double C1 = A1 * p1.Longitude + B1 * p1.Latitude;
            double A2 = q2.Latitude - p2.Latitude;
            double B2 = p2.Longitude - q2.Longitude;
            double C2 = A2 * p2.Longitude + B2 * p2.Latitude;

            double det = A1 * B2 - A2 * B1;
            if (det == 0.0)
            {
              //  Lines are parallel
            }
            else
            {
                retPt.Longitude = (B2 * C1 - B1 * C2) / det;
                retPt.Latitude = (A1 * C2 - A2 * C1) / det;
            }

            return retPt;


            //double x1 = this.startPt.Longitude;
            //double y1 = this.startPt.Latitude;
            //double x2 = this.endPt.Longitude;
            //double y2 = this.endPt.Latitude;
            //double x3 = linSeg.startPt.Longitude;
            //double y3 = linSeg.startPt.Latitude;
            //double x4 = linSeg.endPt.Longitude;
            //double y4 = linSeg.endPt.Latitude;

            //double de = (y4 - y3) * (x2 - x1) - (x4 - x3) * (y2 - y1);
            ////if de<>0 then //lines are not parallel
            //if (Math.Abs(de - 0) < ConstantValue.SmallValue) //not parallel
            //{
            //    double ua = ((x4 - x3) * (y1 - y3) - (y4 - y3) * (x1 - x3)) / de;
            //    double ub = ((x2 - x1) * (y1 - y3) - (y2 - y1) * (x1 - x3)) / de;

            //    if ((ub > 0) && (ub < 1))
            //        return true;
            //    else
            //        return false;
            //}
            //else    //lines are parallel
            //    return false;
        }
       
        /// <summary>
        /// Given three colinear points p, q, r, the function checks if point q lies on line segment 'pr'
        /// </summary>
        /// <param name="q"></param>
        /// <returns></returns>
        public bool IsOnSegment(Vector q)
        {
            Vector p = this.startPt;
            Vector r = this.endPt;
            if (q.Longitude <= Math.Max(p.Longitude, r.Longitude) && q.Longitude >= Math.Min(p.Longitude, r.Longitude) &&
                q.Latitude <= Math.Max(p.Latitude, r.Latitude) && q.Latitude >= Math.Min(p.Latitude, r.Latitude))
                return true;

            return false;
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
    }
}
