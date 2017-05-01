using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AirNavigationRaceLive.Comps.Model;
using Facet.Combinatorics;
using NetworkObjects;
using System.Xml.Linq;
using System.Globalization;
using Microsoft.VisualBasic.FileIO;

namespace AirNavigationRaceLive.Comps.Helper
{
    public static class Importer
    {
        public static List<string> lstWarnings = new List<string>();
        /// <summary> 
        /// Imports a DxfFile that is in the specified Format. Any changes on the import schema may cause Errors!
        /// </summary>
        /// <param name="filepath"></param>
        public static Parcour importFromDxfCH(string filepath)
        {
            Parcour result = new Parcour();

            StreamReader sr = new StreamReader(filepath);
            List<string> Line = new List<string>();
            while (!sr.EndOfStream)
            {
                Line.Add(sr.ReadLine());
            }
            string[] lines = Line.ToArray();
            for (int i = 1; i < lines.Length; i++) //Looping through Array, starting with 1 (lines[0] is "0")
            {
                //Find Lines Containing a new Element Definition
                if (lines[i] == "LWPOLYLINE" && lines[i - 1] == "  0") //
                {
                    //Reading out Layer ( "8" [\n] layerName) = Type of Element
                    if (lines[i + 5] == "  8" && lines[i + 6].Contains("PROH")) // "Prohibited Zone" = ForbiddenZone
                    {
                        if (lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90")
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            int numberOfVertexes = int.Parse(lines[i + 10 + correctur]);
                            List<Vector> input = new List<Vector>();
                            for (int j = 0; j < numberOfVertexes; j++)
                            {

                                double Longitude = Converter.CHtoWGSlng(double.Parse(lines[i + (j * 4) + 16 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + (j * 4) + 18 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                                double Latitude = Converter.CHtoWGSlat(double.Parse(lines[i + (j * 4) + 16 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + (j * 4) + 18 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                                Vector v = new Vector(Longitude, Latitude, 0);
                                input.Add(v);

                            }
                            if (input.Count > 2)
                            {
                                List<List<Vector>> konvexLists = Vector.KonvexPolygons(input);
                                foreach (List<Vector> list in konvexLists)
                                {
                                    double sumX = 0;
                                    double sumY = 0;
                                    double count = 0;
                                    foreach (Vector v in list)
                                    {
                                        sumX += v.X;
                                        sumY += v.Y;
                                        count += 1;
                                    }
                                    Vector o = new Vector(sumX / count, sumY / count, 0);
                                    for (int j = 0; j < list.Count; j++)
                                    {
                                        Line l = new Line();
                                        l.Type = (int)LineType.PENALTYZONE;
                                        int i_a = j % list.Count;
                                        int i_b = (j + 1) % list.Count;
                                        Vector a = list[i_a];
                                        Vector b = list[i_b];
                                        l.A = a.toGPSPoint();
                                        l.B = b.toGPSPoint();
                                        l.O = o.toGPSPoint();
                                        result.Line.Add(l);
                                    }
                                }
                            }
                        }
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("STARTPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Longitude1 = Converter.CHtoWGSlng(res[0] * 1000, res[1] * 1000);
                        double Latitude1 = Converter.CHtoWGSlat(res[0] * 1000, res[1] * 1000);
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Longitude2 = Converter.CHtoWGSlng(res[2] * 1000, res[3] * 1000);
                        double Latitude2 = Converter.CHtoWGSlat(res[2] * 1000, res[3] * 1000);
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(11, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.START_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.START_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.START_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.START_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("ENDPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Longitude1 = Converter.CHtoWGSlng(res[0] * 1000, res[1] * 1000);
                        double Latitude1 = Converter.CHtoWGSlat(res[0] * 1000, res[1] * 1000);
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Longitude2 = Converter.CHtoWGSlng(res[2] * 1000, res[3] * 1000);
                        double Latitude2 = Converter.CHtoWGSlat(res[2] * 1000, res[3] * 1000);
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(9, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.END_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.END_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.END_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.END_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);

                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("NBLINE"))
                    {
                        if ((lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90") && double.Parse(lines[10], NumberFormatInfo.InvariantInfo) == 2)
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            Line l = new Line();
                            double Longitude1 = Converter.CHtoWGSlng(double.Parse(lines[i + 16 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + 18 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                            double Latitude1 = Converter.CHtoWGSlat(double.Parse(lines[i + 16 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + 18 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                            l.A = new Point();
                            l.A.longitude = Longitude1;
                            l.A.latitude = Latitude1;

                            double Longitude2 = Converter.CHtoWGSlng(double.Parse(lines[i + 20 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + 22 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                            double Latitude2 = Converter.CHtoWGSlat(double.Parse(lines[i + 20 + correctur], NumberFormatInfo.InvariantInfo) * 1000, double.Parse(lines[i + 22 + correctur], NumberFormatInfo.InvariantInfo) * 1000);
                            l.B = new Point();
                            l.B.longitude = Longitude2;
                            l.B.latitude = Latitude2;


                            Vector start = new Vector(Longitude1, Latitude1, 0);
                            Vector end = new Vector(Longitude2, Latitude2, 0);
                            Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                            l.O = o.toGPSPoint();
                            l.Type = (int)LineType.LINEOFNORETURN;
                            result.Line.Add(l);
                        }
                    }
                }
            }
            return result;
        }/// <summary> 
         /// Imports a DxfFile that is in the specified Format. Any changes on the import schema may cause Errors!
         /// </summary>
         /// <param name="filepath"></param>
        public static Parcour importFromDxfWGS(string filepath)
        {
            Parcour result = new Parcour();

            StreamReader sr = new StreamReader(filepath);
            List<string> Line = new List<string>();
            while (!sr.EndOfStream)
            {
                Line.Add(sr.ReadLine());
            }
            string[] lines = Line.ToArray();
            for (int i = 1; i < lines.Length; i++) //Looping through Array, starting with 1 (lines[0] is "0")
            {
                //Find Lines Containing a new Element Definition
                if (lines[i] == "LWPOLYLINE" && lines[i - 1] == "  0") //
                {
                    //Reading out Layer ( "8" [\n] layerName) = Type of Element
                    if (lines[i + 5] == "  8" && lines[i + 6].Contains("PROH")) // "Prohibited Zone" = ForbiddenZone
                    {
                        if (lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90")
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            int numberOfVertexes = int.Parse(lines[i + 10 + correctur]);
                            List<Vector> input = new List<Vector>();
                            for (int j = 0; j < numberOfVertexes; j++)
                            {

                                double Longitude = double.Parse(lines[i + (j * 4) + 18 + correctur], NumberFormatInfo.InvariantInfo);
                                double Latitude = double.Parse(lines[i + (j * 4) + 16 + correctur], NumberFormatInfo.InvariantInfo);
                                Vector v = new Vector(Longitude, Latitude, 0);
                                input.Add(v);

                            }
                            if (input.Count > 2)
                            {
                                List<List<Vector>> konvexLists = Vector.KonvexPolygons(input);
                                foreach (List<Vector> list in konvexLists)
                                {
                                    double sumX = 0;
                                    double sumY = 0;
                                    double count = 0;
                                    foreach (Vector v in list)
                                    {
                                        sumX += v.X;
                                        sumY += v.Y;
                                        count += 1;
                                    }
                                    Vector o = new Vector(sumX / count, sumY / count, 0);
                                    for (int j = 0; j < list.Count; j++)
                                    {
                                        Line l = new Line();
                                        l.Type = (int)LineType.PENALTYZONE;
                                        int i_a = j % list.Count;
                                        int i_b = (j + 1) % list.Count;
                                        Vector a = list[i_a];
                                        Vector b = list[i_b];
                                        l.A = a.toGPSPoint();
                                        l.B = b.toGPSPoint();
                                        l.O = o.toGPSPoint();
                                        result.Line.Add(l);
                                    }
                                }
                            }
                        }
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("STARTPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Longitude1 = res[1];
                        double Latitude1 = res[0];
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Longitude2 = res[3];
                        double Latitude2 = res[2];
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(11, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.START_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.START_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.START_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.START_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("ENDPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Longitude1 = res[1];
                        double Latitude1 = res[0];
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Longitude2 = res[3];
                        double Latitude2 = res[2];
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(9, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.END_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.END_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.END_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.END_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);

                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("NBLINE"))
                    {
                        if ((lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90") && double.Parse(lines[10], NumberFormatInfo.InvariantInfo) == 2)
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            Line l = new Line();
                            double Longitude1 = double.Parse(lines[i + 18 + correctur], NumberFormatInfo.InvariantInfo);
                            double Latitude1 = double.Parse(lines[i + 16 + correctur], NumberFormatInfo.InvariantInfo);
                            l.A = new Point();
                            l.A.longitude = Longitude1;
                            l.A.latitude = Latitude1;

                            double Longitude2 = double.Parse(lines[i + 22 + correctur], NumberFormatInfo.InvariantInfo);
                            double Latitude2 = double.Parse(lines[i + 20 + correctur], NumberFormatInfo.InvariantInfo);
                            l.B = new Point();
                            l.B.longitude = Longitude2;
                            l.B.latitude = Latitude2;

                            Vector start = new Vector(Longitude1, Latitude1, 0);
                            Vector end = new Vector(Longitude2, Latitude2, 0);
                            Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                            l.O = o.toGPSPoint();
                            l.Type = (int)LineType.LINEOFNORETURN;
                            result.Line.Add(l);
                        }
                    }
                }
            }
            return result;
        }
        /// <summary> 
        /// Imports a DxfFile that is in the specified Format. Any changes on the import schema may cause Errors!
        /// </summary>
        /// <param name="filepath"></param>
        public static Parcour importFromDxfWGSSwitched(string filepath)
        {
            Parcour result = new Parcour();

            StreamReader sr = new StreamReader(filepath);
            List<string> Line = new List<string>();
            while (!sr.EndOfStream)
            {
                Line.Add(sr.ReadLine());
            }
            string[] lines = Line.ToArray();
            for (int i = 1; i < lines.Length; i++) //Looping through Array, starting with 1 (lines[0] is "0")
            {
                //Find Lines Containing a new Element Definition
                if (lines[i] == "LWPOLYLINE" && lines[i - 1] == "  0") //
                {
                    //Reading out Layer ( "8" [\n] layerName) = Type of Element
                    if (lines[i + 5] == "  8" && lines[i + 6].Contains("PROH")) // "Prohibited Zone" = ForbiddenZone
                    {
                        if (lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90")
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            int numberOfVertexes = int.Parse(lines[i + 10 + correctur]);
                            List<Vector> input = new List<Vector>();
                            for (int j = 0; j < numberOfVertexes; j++)
                            {

                                double Latitude = double.Parse(lines[i + (j * 4) + 18 + correctur], NumberFormatInfo.InvariantInfo);
                                double Longitude = double.Parse(lines[i + (j * 4) + 16 + correctur], NumberFormatInfo.InvariantInfo);
                                Vector v = new Vector(Longitude, Latitude, 0);
                                input.Add(v);

                            }
                            if (input.Count > 2)
                            {
                                List<List<Vector>> konvexLists = Vector.KonvexPolygons(input);
                                foreach (List<Vector> list in konvexLists)
                                {
                                    double sumX = 0;
                                    double sumY = 0;
                                    double count = 0;
                                    foreach (Vector v in list)
                                    {
                                        sumX += v.X;
                                        sumY += v.Y;
                                        count += 1;
                                    }
                                    Vector o = new Vector(sumX / count, sumY / count, 0);
                                    for (int j = 0; j < list.Count; j++)
                                    {
                                        Line l = new Line();
                                        l.Type = (int)LineType.PENALTYZONE;
                                        int i_a = j % list.Count;
                                        int i_b = (j + 1) % list.Count;
                                        Vector a = list[i_a];
                                        Vector b = list[i_b];
                                        l.A = a.toGPSPoint();
                                        l.B = b.toGPSPoint();
                                        l.O = o.toGPSPoint();
                                        result.Line.Add(l);
                                    }
                                }
                            }
                        }
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("STARTPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Latitude1 = res[1];
                        double Longitude1 = res[0];
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Latitude2 = res[3];
                        double Longitude2 = res[2];
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(11, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.START_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.START_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.START_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.START_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("ENDPOINT-"))
                    {
                        double[] res = new double[4];
                        res[3] = 0.0f;
                        int resCount = 0;
                        for (int j = 0; resCount < 4; j++)
                        {
                            try
                            {
                                double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                                int dummy;
                                if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                                {
                                    res[resCount++] = parsed;
                                }
                            }
                            catch { }
                        }

                        Line l = new Line();
                        double Latitude1 = res[1];
                        double Longitude1 = res[0];
                        l.A = new Point();
                        l.A.longitude = Longitude1;
                        l.A.latitude = Latitude1;

                        double Latitude2 = res[3];
                        double Longitude2 = res[2];
                        l.B = new Point();
                        l.B.longitude = Longitude2;
                        l.B.latitude = Latitude2;

                        Vector start = new Vector(Longitude1, Latitude1, 0);
                        Vector end = new Vector(Longitude2, Latitude2, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(lines[i + 6].ToString().Trim());

                        //string gatename = lines[i + 6].Substring(9, 1);
                        //switch (gatename)
                        //{
                        //    case "A":
                        //        {
                        //            l.Type = (int)LineType.END_A;
                        //            break;
                        //        }
                        //    case "B":
                        //        {
                        //            l.Type = (int)LineType.END_B;
                        //            break;
                        //        }
                        //    case "C":
                        //        {
                        //            l.Type = (int)LineType.END_C;
                        //            break;
                        //        }
                        //    case "D":
                        //        {
                        //            l.Type = (int)LineType.END_D;
                        //            break;
                        //        }
                        //}
                        result.Line.Add(l);

                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].Contains("NBLINE"))
                    {
                        if ((lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90") && double.Parse(lines[10], NumberFormatInfo.InvariantInfo) == 2)
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;
                            Line l = new Line();
                            double Latitude1 = double.Parse(lines[i + 18 + correctur], NumberFormatInfo.InvariantInfo);
                            double Longitude1 = double.Parse(lines[i + 16 + correctur], NumberFormatInfo.InvariantInfo);
                            l.A = new Point();
                            l.A.longitude = Longitude1;
                            l.A.latitude = Latitude1;

                            double Latitude2 = double.Parse(lines[i + 22 + correctur], NumberFormatInfo.InvariantInfo);
                            double Longitude2 = double.Parse(lines[i + 20 + correctur], NumberFormatInfo.InvariantInfo);
                            l.B = new Point();
                            l.B.longitude = Longitude2;
                            l.B.latitude = Latitude2;

                            Vector start = new Vector(Longitude1, Latitude1, 0);
                            Vector end = new Vector(Longitude2, Latitude2, 0);
                            Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                            l.O = o.toGPSPoint();
                            l.Type = (int)LineType.LINEOFNORETURN;
                            result.Line.Add(l);
                        }
                    }
                }
            }
            return result;
        }


        /// <summary> 
        /// Imports the Layer of prohibited data from a KML file that is in a specified Format
        /// </summary>
        /// <param name="filepath"></param>
        public static Parcour importFromKMLLayer(string filepath)
        {
            // parcour generation using GoogleEarth kml file (generated with the Route Generator) as input
            //
            // read coordinates directly from a kml file
            // all prohibited zones must have a name starting with PROH (kml object: polygons)
            // Startpoint, Endpoint, NBL lines (kml object: path)

            // PROH
            // STARTPOINT-A, STARTPOINT-B, STARTPOINT-C, STARTPOINT-D
            // ENDPOINT-A, ENDPOINT-B, ENDPOINT-C, ENDPOINT-D
            // NBLINE

            Parcour result = new Parcour();

            XNamespace nsKml = XNamespace.Get("http://www.opengis.net/kml/2.2");
            XDocument gpxDoc = XDocument.Load(filepath);
            var folders = from flder in gpxDoc.Descendants(nsKml + "Folder")
                          where flder.Element(nsKml + "name").Value.ToString().Trim() == "LiveTracking"
                          select flder;

            if (folders.Count() == 0)
            {
                throw new ApplicationException("Cannot import kml data.\r\nData is expected to be in a kml folder named 'LiveTracking', but this folder is missing from the imported file.", null);
            }
            foreach (var placemark in folders.Elements(nsKml + "Placemark"))
            {
                string pmName = placemark.Element(nsKml + "name").Value.Trim();

                if (pmName.StartsWith("PROH"))
                {
                    // create polygon elements
                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        List<Point> lst = getPointsFromKMLCoordinates(coord.Value);
                        int numberOfVertexes = lst.Count;
                        List<Vector> input = new List<Vector>();
                        foreach (var pt in lst)
                        {
                            Vector v = new Vector(pt.longitude, pt.latitude, 0);
                            input.Add(v);
                        }
                        if (input.Count > 2)
                        {
                            // not very clear what we are doing here, copied from above
                            List<List<Vector>> konvexLists = Vector.KonvexPolygons(input);
                            foreach (List<Vector> list in konvexLists)
                            {
                                double sumX = 0;
                                double sumY = 0;
                                double count = 0;
                                foreach (Vector v in list)
                                {
                                    sumX += v.X;
                                    sumY += v.Y;
                                    count += 1;
                                }
                                Vector o = new Vector(sumX / count, sumY / count, 0);
                                for (int j = 0; j < list.Count; j++)
                                {
                                    Line l = new Line();
                                    l.Type = (int)LineType.PENALTYZONE;
                                    int i_a = j % list.Count;
                                    int i_b = (j + 1) % list.Count;
                                    Vector a = list[i_a];
                                    Vector b = list[i_b];
                                    l.A = a.toGPSPoint();
                                    l.B = b.toGPSPoint();
                                    l.O = o.toGPSPoint();
                                    result.Line.Add(l);
                                }
                            }
                        }
                    }
                }
                else if (pmName.StartsWith("STARTPOINT-"))
                {
                    // create line elements
                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        List<Point> lst = getPointsFromKMLCoordinates(coord.Value);
                        Line l = new Line();
                        l.A = lst[0];
                        l.B = lst[1];
                        Vector start = new Vector(l.A.longitude, l.A.latitude, 0);
                        Vector end = new Vector(l.B.longitude, l.B.latitude, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(pmName);
                        result.Line.Add(l);
                    }
                }
                else if (pmName.StartsWith("ENDPOINT-"))
                {
                    // create line elements
                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        Line l = new Line();
                        List<Point> lst = getPointsFromKMLCoordinates(coord.Value);
                        l.A = lst[0];
                        l.B = lst[1];

                        Vector start = new Vector(l.A.longitude, l.A.latitude, 0);
                        Vector end = new Vector(l.B.longitude, l.B.latitude, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();
                        l.Type = (int)gateLineTypeFromGateName(pmName);
                        result.Line.Add(l);

                    }
                }
                else if (pmName.StartsWith("NBLINE"))
                {
                    // create line elements
                    foreach (var coord in placemark.Descendants(nsKml + "coordinates"))
                    {
                        Line l = new Line();
                        List<Point> lst = getPointsFromKMLCoordinates(coord.Value);
                        l.A = lst[0];
                        l.B = lst[1];

                        Vector start = new Vector(l.A.longitude, l.A.latitude, 0);
                        Vector end = new Vector(l.B.longitude, l.B.latitude, 0);
                        Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                        l.O = o.toGPSPoint();

                        l.Type = (int)LineType.LINEOFNORETURN;
                        result.Line.Add(l);
                    }
                }
            }

            return result;
        }

        /// <summary> 
        /// Imports one or several Take-off lines from a KML file.
        /// The Take-off line is defined as Path object in GoogleEarth
        /// The code detects all Placemarks that have a name staring like 'TKOF', and reads the forst two points
        /// </summary>
        /// <param name="filepath"></param>
        public static List<Line> importTKOFLineFromKML(string filepath, out List<string> lstTKOFLineNames)
        {
            XNamespace nsKml = XNamespace.Get("http://www.opengis.net/kml/2.2");
            XDocument gpxDoc = XDocument.Load(filepath);
            var placemark = from plmk in gpxDoc.Descendants(nsKml + "Placemark")
                            where plmk.Element(nsKml + "name").Value.ToString().Trim().StartsWith("TKOF")
                            select plmk;

            List<string> lstNames = new List<string>();
            List<Line> lstLines = new List<Line>();


            foreach (var pm in placemark)
            {
                // create line elements
                foreach (var coord in pm.Descendants(nsKml + "coordinates"))
                {
                    Line l = new Line();
                    List<Point> lst = getPointsFromKMLCoordinates(coord.Value);
                    l.A = lst[0];
                    l.B = lst[1];

                    Vector start = new Vector(l.A.longitude, l.A.latitude, 0);
                    Vector end = new Vector(l.B.longitude, l.B.latitude, 0);
                    Vector o = Vector.Middle(start, end) - Vector.Orthogonal(end - start);
                    l.O = o.toGPSPoint();
                    lstLines.Add(l);
                    lstNames.Add(pm.Element(nsKml + "name").Value.ToString());
                }
            }
            lstTKOFLineNames = lstNames;
            return lstLines;
        }


        /// <summary>
        /// Imports a GAC File of a Flight.
        /// </summary>
        /// <param name="filepath"></param>
        /// <returns>The created Flight object</returns>
        public static List<Point> GPSdataFromGAC(int year, int month, int day, string filename)
        {
            lstWarnings.Clear();
            List<Point> result = new List<Point>();
            StreamReader gacFileStreamReader = new StreamReader(filename);
            string line = string.Empty;
            long iCnt = 0;
            //line = gacFileStreamReader.ReadLine();
            //while (!line.Substring(0, 1).Equals("I") && !gacFileStreamReader.EndOfStream)
            //{
            //    line = gacFileStreamReader.ReadLine();
            //}
            {
                while (!gacFileStreamReader.EndOfStream)
                {
                    line = gacFileStreamReader.ReadLine();
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    if (line.Substring(0, 1).Equals("B"))
                    {
                        iCnt++;
                        //B082337 4758489N 008 30 945 E A99999 0224901011680001
                        //B1601114816962N00700724EA003100037007532330012
                        // timestamp
                        DateTime newPointTimeStamp = new DateTime(year, month, day);
                        // in certain cases loggerds may produce a timestamp as 100860 (10:08:60 is basically an invalid timestamp)
                        // this case can however be handled
                        try
                        {
                            // this will reject a formally invalid timestamp 101160
                            //newPointTimeStamp = new DateTime(year, month, day,
                            //Convert.ToInt32(line.Substring(1, 2)),
                            //Convert.ToInt32(line.Substring(3, 2)),
                            //Convert.ToInt32(line.Substring(5, 2)));

                            // this will accept a formally invalid timestamp 101160 --> 101200

                            newPointTimeStamp = newPointTimeStamp.AddHours(Convert.ToInt32(line.Substring(1, 2)));
                            newPointTimeStamp = newPointTimeStamp.AddMinutes(Convert.ToInt32(line.Substring(3, 2)));
                            newPointTimeStamp = newPointTimeStamp.AddSeconds(Convert.ToInt32(line.Substring(5, 2)));
                            newPointTimeStamp.ToString("HHmmss");
                            if (Convert.ToInt32(line.Substring(1, 2)) < 0 || Convert.ToInt32(line.Substring(1, 2)) > 23)    
                            {
                                lstWarnings.Add(string.Format("WARNING: data line {0}, time value [{1}] is invalid. [{2}] will be used instead.", iCnt, line.Substring(1, 6), newPointTimeStamp.ToString("HHmmss")));
                            }
                            if (Convert.ToInt32(line.Substring(3, 2)) < 0 || Convert.ToInt32(line.Substring(3, 2)) > 59)
                            {
                                lstWarnings.Add(string.Format("WARNING: data line {0}, time value [{1}] value is invalid. [{2}] will be used instead.", iCnt, line.Substring(1, 6), newPointTimeStamp.ToString("HHmmss")));
                            }
                            if (Convert.ToInt32(line.Substring(5, 2)) < 0 || Convert.ToInt32(line.Substring(5, 2)) > 59)
                            {
                                lstWarnings.Add(string.Format("WARNING: data line {0}, time value [{1}] value is invalid. [{2}] will be used instead.", iCnt, line.Substring(1, 6), newPointTimeStamp.ToString("HHmmss")));

                            }
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException(String.Format("\nError in time import on data line {1}\ndata {0} is probably not a valid time format (HHmmss)", line.Substring(1, 6), iCnt));
                        }

                        // latitude
                        double newPointLatitude;
                        try
                        {
                            newPointLatitude = Convert.ToDouble(line.Substring(7, 2)) + Convert.ToDouble(line.Substring(9, 2) + "." + line.Substring(11, 3), NumberFormatInfo.InvariantInfo) / 60;
                            switch (line.Substring(14, 1))
                            {
                                case "N":
                                    break;
                                case "S":
                                    newPointLatitude *= (-1);
                                    break;
                                default:
                                    // TODO: Error
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException(String.Format("\nError in Longitude import\ndata line {1}: data value: {0}", line.Substring(7, 8), iCnt));
                        }

                        // longitude
                        double newPointLongitude;
                        try
                        {
                            newPointLongitude = Convert.ToDouble(line.Substring(15, 3)) + Convert.ToDouble(line.Substring(18, 2) + "." + line.Substring(20, 3), NumberFormatInfo.InvariantInfo) / 60;
                            switch (line.Substring(23, 1))
                            {
                                case "E":
                                    break;
                                case "W":
                                    newPointLongitude *= (-1);
                                    break;
                                default:
                                    // ToDo: Error
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException(String.Format("\nError in Longitude import\ndata line {1}: data value: {0}", line.Substring(15, 9), iCnt));
                        }

                        if (line.Length<46)
                        {
                            throw new ApplicationException(String.Format("\nError in import\ndata line {1}: line length: {0}, expected: 46", line.Length, iCnt));
                        }

                        double altitude, speed, bearing, acc;
                        string strFld=string.Empty, strPos=String.Empty;
                        try
                        {
                            strFld = "altitude"; strPos = line.Substring(30, 5);
                            altitude = double.Parse(line.Substring(30, 5), NumberFormatInfo.InvariantInfo) * 0.3048f; //Feet to Meter
                            strFld = "speed"; strPos = line.Substring(35, 4);
                            speed = (double.Parse(line.Substring(35, 4), NumberFormatInfo.InvariantInfo) / 10) / 0.514444444f; //Knot to m/s
                            strFld = "bearing"; strPos = line.Substring(39, 3);
                            bearing = double.Parse(line.Substring(39, 3), NumberFormatInfo.InvariantInfo);
                            strFld = "acc"; strPos = line.Substring(42, 4);
                            acc = double.Parse(line.Substring(42, 4), NumberFormatInfo.InvariantInfo);
                        }
                        catch (Exception)
                        {
                            throw new ApplicationException(String.Format("\nError in {2} import\ndata line {1}: data value: {0}", strPos, iCnt, strFld));
                        }


                        Point data = new Point();
                        data.Timestamp = newPointTimeStamp.Ticks;
                        data.latitude = newPointLatitude;
                        data.longitude = newPointLongitude;
                        data.altitude = altitude;
                        result.Add(data);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Importing data in GPX format
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        internal static List<Point> GPSdataFromGPX(string filename)
        {
            // note the namespace can be ...GPX/1/1  but for livetrack24 etc. also GPX/1/0 so do not use a fixed namespace....
            //XNamespace gpx = XNamespace.Get("http://www.topografix.com/GPX/1/1");
            List<Point> result = new List<Point>();
            XDocument gpxDoc = XDocument.Load(filename);
            XNamespace gpx = gpxDoc.Root.Name.Namespace;
            var tracks = from track in gpxDoc.Descendants(gpx + "trk")
                         select new
                         {
                             Name = track.Element(gpx + "name") != null ?
                                 track.Element(gpx + "name").Value : null,
                             Segs = (
                                  from trackpoint in track.Descendants(gpx + "trkpt")
                                  select new
                                  {
                                      Latitude = trackpoint.Attribute("lat").Value,
                                      Longitude = trackpoint.Attribute("lon").Value,
                                      Elevation = trackpoint.Element(gpx + "ele") != null ?
                                        trackpoint.Element(gpx + "ele").Value : null,
                                      Time = trackpoint.Element(gpx + "time") != null ?
                                        trackpoint.Element(gpx + "time").Value : null
                                  }
                                )
                         };

            foreach (var trk in tracks)
            {
                foreach (var trkSeg in trk.Segs)
                {
                    Point data = new Point();
                    data.Timestamp = DateTime.Parse(trkSeg.Time).Ticks;
                    data.latitude = Double.Parse(trkSeg.Latitude, NumberFormatInfo.InvariantInfo);
                    data.longitude = Double.Parse(trkSeg.Longitude, NumberFormatInfo.InvariantInfo);
                    data.altitude = Double.Parse(trkSeg.Elevation, NumberFormatInfo.InvariantInfo);
                    result.Add(data);
                }
            }
            return result;
        }

        /// <summary>
        /// generate a list of points from a string with coordinates.
        /// </summary>
        /// <example>22.5632362885002,61.6393348726027,300 22.5676987760501,61.649107691418,300 22.5632362885002,61.6393348726027,300
        /// </example>
        /// <param name="str"></param>
        /// <returns>A list of points</returns>
        public static List<Point> getPointsFromKMLCoordinates(string str, bool includeAltitude = false)
        {
            Line l = new Line();
            Point point = new Point();
            List<Point> lst = new List<Point>();
            string[] pt;
            // NOTE: string may contain linebreaks instead of space
            string[] ptstrings = str.Replace("\n", " ").Split(' ');

            foreach (var ptstring in ptstrings)
            {
                double lon, lat, alt = 0.0;
                point = new Point();
                pt = ptstring.Split(',');
                if (double.TryParse(pt[0].Trim(), System.Globalization.NumberStyles.Float, NumberFormatInfo.InvariantInfo, out lon) &&
                    double.TryParse(pt[1].Trim(), System.Globalization.NumberStyles.Float, NumberFormatInfo.InvariantInfo, out lat) &&
                    double.TryParse(pt[2].Trim(), System.Globalization.NumberStyles.Float, NumberFormatInfo.InvariantInfo, out alt)
)
                {
                    point.longitude = lon;
                    point.latitude = lat;
                    if (includeAltitude)
                    {
                        point.altitude = alt;
                    }
                    lst.Add(point);
                }
            }
            return lst;
        }

        /// <summary>
        /// retrieve the LineType based on the gate name.
        /// </summary>
        /// <param name="gateName"></param>
        /// <returns>The LineType</returns>
        internal static LineType gateLineTypeFromGateName(string gateName)
        {
            switch (gateName)
            {
                case "STARTPOINT-A":
                    {
                        return LineType.START_A;
                    }
                case "STARTPOINT-B":
                    {
                        return LineType.START_B;
                    }
                case "STARTPOINT-C":
                    {
                        return LineType.START_C;
                    }
                case "STARTPOINT-D":
                    {
                        return LineType.START_D;
                    }

                case "ENDPOINT-A":
                    {
                        return LineType.END_A;
                    }
                case "ENDPOINT-B":
                    {
                        return LineType.END_B;
                    }
                case "ENDPOINT-C":
                    {
                        return LineType.END_C;
                    }
                case "ENDPOINT-D":
                    {
                        return LineType.END_D;
                    }
            }
            // nothing found...
            throw new Exception("cannot define Line type for layer");
        }

        internal static List<Subscriber> getPilotsListCSV(string filePath)
        {
            //var filePath = @"C:\Person.csv"; // Habeeb, "Dubai Media City, Dubai"
            List<Subscriber> lst = new List<Subscriber>();
            using (TextFieldParser csvParser = new TextFieldParser(filePath))
            {
                csvParser.CommentTokens = new string[] { "#" };
                csvParser.SetDelimiters(new string[] { ",", ";", "|" });
                csvParser.HasFieldsEnclosedInQuotes = true;

                // Skip the row with the column names
                //csvParser.ReadLine();
                while (!csvParser.EndOfData)
                {
                    // Read current line fields, pointer moves to the next line.
                    string[] fields = csvParser.ReadFields();
                    Subscriber pil = new Subscriber();
                    pil.LastName = fields[0].Trim();
                    pil.FirstName = fields[1].Trim();
                    lst.Add(pil);
                }
            }

            return lst;
        }
    }
}
