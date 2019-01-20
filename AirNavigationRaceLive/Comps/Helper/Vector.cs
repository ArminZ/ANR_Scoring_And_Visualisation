using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeometryUtility;
using PolygonCuttingEar;
using AirNavigationRaceLive.Model;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class Vector
    {
        public Vector(double X, double Y, double Z)
        {
            this.X = X;
            this.Y = Y;
            this.Z = Z;
        }

        public Vector(Vector v)
        {
            X = v.X;
            Y = v.Y;
            Z = v.Z;
        }
        public double X = 0;
        public double Y = 0;
        public double Z = 0;

        public override bool Equals(object obj)
        {
            Vector v = obj as Vector;
            return v != null && v.X.Equals(X) && v.Y.Equals(Y) && v.Z.Equals(Z);
        }
        public override int GetHashCode()
        {
            return (X + Y + Z).GetHashCode();
        }
        public override string ToString()
        {
            return "X: " + X + " Y: " + Y + " Z: " + Z;
        }

        public static Vector operator /(Vector a, double b)
        {
            return new Vector(a.X / b, a.Y / b, a.Z / b);
        }
        public static Vector operator *(Vector a, double b)
        {
            return new Vector(a.X * b, a.Y * b, a.Z * b);
        }
        public static double operator *(Vector a, Vector b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }
        public static Vector operator +(Vector a, Vector b)
        {
            return new Vector(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }
        public static Vector operator -(Vector a, Vector b)
        {
            return new Vector(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }
        public static Vector Cross(Vector a, Vector b)
        {
            return new Vector(a.Y * b.Z - a.Z * b.Y, a.Z * b.X - a.X * b.Z, a.X * b.Y - a.Y * b.X);
        }
        public static double Spat(Vector a, Vector b, Vector c)
        {
            return a.X * b.Y * c.Z + b.X * c.Y * a.Z + c.X * a.Y * b.Z - a.X * c.Y * b.Z - b.X * a.Y * c.Z - c.X * b.Y * a.Z;
        }
        public static double Abs(Vector a)
        {
            return Math.Sqrt(a.X * a.X + a.Y * a.Y + a.Z * a.Z);
        }

        /// <summary>
        /// Only works for 2D (z=0)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector Orthogonal(Vector a)
        {
            return new Vector(-a.Y, a.X, 0);
        }
        /// <summary>
        /// Initial bearing
        /// see http://www.movable-type.co.uk/scripts/latlong.html?from=48.619,-120.412&to=48.59617,-120.4020
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>initial bearing (as angle)</returns>
        public static double Bearing(Vector a, Vector b)
        {
            double radiansCorr = Math.PI / 180.0;
            double dlong = (b.X - a.X)* radiansCorr;
            var y = Math.Sin(dlong) * Math.Cos(b.Y * radiansCorr);
            var x = Math.Cos(a.Y * radiansCorr) * Math.Sin(b.Y * radiansCorr) -
                    Math.Sin(a.Y * radiansCorr) * Math.Cos(b.Y * radiansCorr) * Math.Cos(dlong);
            var brng = Math.Atan2(y, x);
            var brngDegree = (Math.Atan2(y, x) / radiansCorr +360.0) % 360.0;
            return brngDegree;
        }

        /// <summary>
        /// Destination point given distance and bearing from start point
        /// used to calculate a correct ortogonal vector
        /// see  http://www.movable-type.co.uk/scripts/latlong.html?from=48.619,-120.412&to=48.59617,-120.4020
        /// </summary>
        /// <returns></returns>
        public static Vector destinationPointByBearingAndDistance( Vector a, double brng, double d)
        {
            double radiansCorr = Math.PI / 180.0;
            const double R = 6371;
            var lat2 = Math.Asin(Math.Sin(a.Y * radiansCorr) * Math.Cos(d / R) + Math.Cos(a.Y * radiansCorr) * Math.Sin(d / R) * Math.Cos(brng));
            var lon2 = a.X *radiansCorr + Math.Atan2(
                                    Math.Sin(brng) * Math.Sin(d / R) * Math.Cos(a.Y * radiansCorr),
                                    Math.Cos(d / R) - Math.Sin(a.Y * radiansCorr) * Math.Sin(lat2)
                                    );
            lat2 = lat2 / radiansCorr;
            lon2 = lon2 / radiansCorr;
            lon2 = (lon2 + 540) % 360 -180.0;

            return new Vector(lon2, lat2, 0);
        }
        /// <summary>
        ///  used to calculate the orienation arrow on SP and FP
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="dist"></param>
        /// <returns></returns>
        public static Vector ArrowPointFromGivenLine(Vector a, Vector b, double dist)
        {
            // dist is in NM
            double radiansCorr = Math.PI / 180.0;
            Vector c = Middle(a, b);
            // calculate Bearing, change by + 90 degrees
            double brng = (Bearing(a, b) + 90.0) * radiansCorr;
            Vector d = destinationPointByBearingAndDistance(c, brng, dist * 1.852); //dist in Km
            return d;
        }
        /// <summary>
        /// returns the Ortsvektor of the middle
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector Middle(Vector a, Vector b)
        {
            return new Vector(a.X + (b.X - a.X) / 2, a.Y + (b.Y - a.Y) / 2, a.Z + (b.Z - a.Z) / 2);
        }

        public static Vector Direction(Vector startPoint, Vector endPoint)
        {
            return new Vector(endPoint.X - startPoint.X, endPoint.Y - startPoint.Y, endPoint.Z - startPoint.Z);
        }

        public static Vector Interception(Vector LineA_A, Vector LineA_B, Vector LineB_A, Vector LineB_B)
        {
            try
            {
                Vector va = Vector.Direction(LineA_A, LineA_B);
                Vector vb = Vector.Direction(LineB_A, LineB_B);
                double lambda = -(LineA_A.Y * vb.X - LineB_A.Y * vb.X - LineA_A.X * vb.Y + LineB_A.X * vb.Y) / (va.Y * vb.X - va.X * vb.Y);
                double sigma = -(LineA_A.Y * va.X - LineB_A.Y * va.X - LineA_A.X * va.Y + LineB_A.X * va.Y) / (va.Y * vb.X - va.X * vb.Y);
                if (lambda == double.NaN || lambda > 1 || lambda < 0 || sigma > 1 || sigma < 0 || LineA_A.Z + lambda * va.Z != LineB_A.Z + sigma * vb.Z) { return null; }
                Vector result = LineA_A + (va * lambda);
                return result;
            }
            catch { }
            return null;
        }
        public static Vector InterceptionLine(Vector A, Vector vA, Vector LineB_A, Vector LineB_B)
        {
            try
            {
                Vector va = vA;
                Vector vb = Vector.Direction(LineB_A, LineB_B);
                double lambda = -(A.Y * vb.X - LineB_A.Y * vb.X - A.X * vb.Y + LineB_A.X * vb.Y) / (va.Y * vb.X - va.X * vb.Y);
                double sigma = -(A.Y * va.X - LineB_A.Y * va.X - A.X * va.Y + LineB_A.X * va.Y) / (va.Y * vb.X - va.X * vb.Y);
                if (lambda == double.NaN || sigma == double.NaN || sigma > 1 || sigma < 0 || A.Z + lambda * va.Z != LineB_A.Z + sigma * vb.Z) { return null; }
                Vector result = A + (va * lambda);
                return result;
            }
            catch { }
            return null;
        }

        public static Vector MinDistance(Vector Start, Vector End, Vector Point)
        {
            Vector StartEnd = Direction(Start, End);
            double lambda = (StartEnd.X * Point.X - StartEnd.X * Start.X + StartEnd.Y * Point.Y - StartEnd.Y * Start.Y + StartEnd.Z * Point.Z - StartEnd.Z * Start.Z) /
                (StartEnd.X * StartEnd.X + StartEnd.Y * StartEnd.Y + StartEnd.Z * StartEnd.Z);
            //only on the vector, not the whole line
            lambda = Math.Min(lambda, 1);
            lambda = Math.Max(lambda, 0);
            Vector SchnittPunkt = new Vector(Start.X + lambda * StartEnd.X, Start.Y + lambda * StartEnd.Y, Start.Z + lambda * StartEnd.Z);
            Vector lot = Direction(SchnittPunkt, Point);
            return lot;
        }

        public static Vector LotInterception(Vector Start, Vector End, Vector Point)
        {
            Vector StartEnd = Direction(Start, End);
            double lambda = (StartEnd.X * Point.X - StartEnd.X * Start.X + StartEnd.Y * Point.Y - StartEnd.Y * Start.Y + StartEnd.Z * Point.Z - StartEnd.Z * Start.Z) /
                (StartEnd.X * StartEnd.X + StartEnd.Y * StartEnd.Y + StartEnd.Z * StartEnd.Z);
            Vector SchnittPunkt = new Vector(Start.X + lambda * StartEnd.X, Start.Y + lambda * StartEnd.Y, Start.Z + lambda * StartEnd.Z);
            return SchnittPunkt;
        }

        public static bool hasIntersections(List<Vector> list)
        {
            bool result = false;
            int count = list.Count;
            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    if (i != j && ((i + 1) % count) != j && ((j + 1) % count) != i && ((j + 1) % count) != ((i + 1) % count))
                    {
                        if (Vector.Interception(list[i % count], list[(i + 1) % count], list[j % count], list[(j + 1) % count]) != null)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// Calculates the Angle in Radians between point a and c in point b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Angle in Radians</returns>
        public static double Angle(Vector Point_A, Vector Point_B, Vector Point_C)
        {
            return Angle(Point_A - Point_B, Point_C - Point_B);
        }

        /// <summary>
        /// Calculates the Angle in Radians between vector a and vector b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Angle in Radians</returns>
        public static double Angle(Vector a, Vector b)
        {
            return Math.Acos((a * b) / (Abs(a) * Abs(b)));
        }

        /// <summary>
        /// Calculates the Clokwise Angle in Radians between point a and c in point b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <returns>Angle in Radians</returns>
        public static double AngleClockwise(Vector Point_A, Vector Point_B, Vector Point_C)
        {
            return AngleClockwise(Point_A - Point_B, Point_C - Point_B);
        }

        public static double AngleClockwise(Vector a, Vector b)
        {
            return ((Math.Atan2(a.Y, a.X) - Math.Atan2(b.Y, b.X)) + (Math.PI * 2)) % (Math.PI * 2);
        }
        public static Vector Normalized(Vector a)
        {
            return a / Abs(a);
        }
        public static Vector AngleHalf(Vector Point_A, Vector Point_B, Vector Point_C)
        {
            return Middle(Point_B + Normalized(Point_A - Point_B), Point_B + Normalized(Point_C - Point_B));
        }

        public static List<List<Vector>> KonvexPolygons(List<Vector> input)
        {
            List<List<Vector>> result = new List<List<Vector>>();
            List<Vector> NoDoubles = new List<Vector>();
            foreach (Vector v in input)
            {
                if (!(NoDoubles.Count(p => p.Equals(v)) > 0))
                {
                    NoDoubles.Add(v);
                }
            }
            List<Vector> list = NoDoubles;


            int nVertices = list.Count;

            CPoint2D[] vertices = new CPoint2D[nVertices];
            for (int i = 0; i < nVertices; i++)
            {
                vertices[i] = new CPoint2D(list[i].X,
                    list[i].Y);
            }
            CPolygonShape cutPolygon = new CPolygonShape(vertices);
            cutPolygon.CutEar();
            
            for (int i = 0; i < cutPolygon.NumberOfPolygons; i++)
            {
                int nPoints = cutPolygon.Polygons(i).Length;
                List<Vector> polygon = new List<Vector>();
                for (int j = 0; j < nPoints; j++)
                {
                    polygon.Add(new Vector(cutPolygon.Polygons(i)[j].X,cutPolygon.Polygons(i)[j].Y,0));
                }
                result.Add(polygon);
            }
            return result;
        }

        public static bool IsKonvex(List<Vector> input)
        {
            bool result = true;
            int count = input.Count;
            for (int i = 0; i < count; i++)
            {
                if (AngleClockwise(input[(i + 2) % count], input[(i + 1) % count], input[i % count]) > Math.PI)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        ///Graham-Scan (Only 2D !! z = 0)
        ///bestimme Punkt q mit minimaler y-Koordinate;
        ///sortiere die Punkte des Arrays nach ihrem Winkel, und bei gleichem Winkel nach ihrem Abstand zum Nullpunkt (der Punkt q wird zu p0);
        ///Z will be set to 0 for all Vectors!!
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static List<Vector> Sort(List<Vector> input)
        {
            List<Vector> result = new List<Vector>();
            List<Vector> list = new List<Vector>(input);
            Vector minY = input[0];
            int count = 0;
            int pos = 0;
            foreach (Vector v in list)
            {
                v.Z = 0;
                if (v.Y < minY.Y)
                {
                    minY = v;
                    pos = count;
                }
                count++;
            }
            result.Add(minY);
            list.Remove(minY);
            Vector reference = minY - new Vector(1, 1, 0);
            while (list.Count > 0)
            {
                Vector minVec = list[0];
                double minAngle = Vector.AngleClockwise(reference, minY, minVec);
                double minDist = Vector.Abs(minVec - minY);
                foreach (Vector v in list)
                {
                    double minAngleTmp = Vector.AngleClockwise(reference, minY, v);
                    double minDistTmp = Vector.Abs(v - minY);
                    if (minAngleTmp < minAngle || (minAngleTmp == minAngle && minDistTmp < minDist))
                    {
                        minVec = v;
                        minAngle = minAngleTmp;
                        minDist = minDistTmp;
                    }
                }
                list.Remove(minVec);
                result.Add(minVec);
            }

            return result;
        }

        public Point toGPSPoint()
        {
            Point result = new Point();
            result.longitude = X;
            result.latitude = Y;
            result.altitude = Z;
            return result;
        }
    }
}
