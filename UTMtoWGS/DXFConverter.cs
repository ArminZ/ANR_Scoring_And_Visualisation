using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;

namespace UTM
{
    static class DXFConverter
    {
        /// <summary> 
        /// Imports a DxfFile that is in the specified Format. Any changes on the import schema may cause Errors!
        /// </summary>
        /// <param name="filepath"></param>
        public static void importFromDxf(int zone, bool southhemi,string filepath)
        {
            StreamReader sr = new StreamReader(filepath);
            List<string> lineList = new List<string>();
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                lineList.Add(line);
            }
            string[] lines = lineList.ToArray();
            string[] linesCH = lineList.ToArray();
            string[] linesWGS = lineList.ToArray();
            for (int i = 1; i < lines.Length; i++) //Looping through Array, starting with 1 (lines[0] is "0")
            {
                //Find Lines Containing a new Element Definition
                if (lines[i].ToUpper() == "LWPOLYLINE" && lines[i - 1] == "  0") //
                {
                    //Reading out Layer ( "8" [\n] layerName) = Type of Element
                    if (lines[i + 5] == "  8" && lines[i + 6].ToUpper().Contains("PROH")) // "Prohibited Zone" = ForbiddenZone
                    {
                        if (lines[i + 9 + 4] == " 90" || lines[i + 9] == " 90")
                        {
                            int correctur = lines[i + 9 + 4] == " 90" ? 4 : 0;

                            int numberOfVertexes = int.Parse(lines[i + 10 + correctur]);
                            for (int j = 0; j < numberOfVertexes; j++)
                            {
                                transform(zone, southhemi, lines, linesCH, linesWGS, i + (j * 4) + 16 + correctur, i + (j * 4) + 18 + correctur);
                            }
                        }
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].ToUpper().Contains("STARTPOINT-"))
                    {
                        int[] ints = getNext4Doubles(lines, i);
                        transform(zone, southhemi, lines, linesCH, linesWGS, ints[0], ints[1]);
                        transform(zone, southhemi, lines, linesCH, linesWGS, ints[2], ints[3]);
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].ToUpper().Contains("ENDPOINT-"))
                    {
                        int[] ints = getNext4Doubles(lines, i);
                        transform(zone, southhemi, lines, linesCH, linesWGS, ints[0], ints[1]);
                        transform(zone, southhemi, lines, linesCH, linesWGS, ints[2], ints[3]);
                    }
                    else if (lines[i + 5] == "  8" && lines[i + 6].ToUpper().Contains("NBLINE"))
                    {
                        if (lines[i + 9] == " 90" && double.Parse(lines[10], NumberFormatInfo.InvariantInfo) == 2)
                        {
                            transform(zone, southhemi, lines, linesCH, linesWGS, i + 16, i + 18);
                            transform(zone, southhemi, lines, linesCH, linesWGS, i + 20, i + 22);
                        }
                    }
                }
            }
            string filepath_ch = filepath.Replace(".dxf", "_ch.dxf");
            string filepath_wgs = filepath.Replace(".dxf", "_wgs84.dxf");
            FileInfo ch = new FileInfo(filepath_ch);
            FileInfo wgs = new FileInfo(filepath_wgs);
            if (ch.Exists)
            {
                ch.Delete();
            }
            if (wgs.Exists)
            {
                wgs.Delete();
            }
            StreamWriter sw_ch = new StreamWriter(ch.Create());
            StreamWriter sw_wgs = new StreamWriter(wgs.Create());
            foreach (string s in linesCH)
            {
                sw_ch.WriteLine(s);
            } 
            foreach (string s in linesWGS)
            {
                sw_wgs.WriteLine(s);
            }
            sw_ch.Close();
            sw_wgs.Close();
        }

        private static int[] getNext4Doubles(string[] lines, int i)
        {
            int[] ints = new int[4];
            int resCount = 0;
            for (int j = 0; resCount < 4; j++)
            {
                try
                {
                    double parsed = double.Parse(lines[i + 6 + 8 + j], NumberFormatInfo.InvariantInfo);
                    int dummy;
                    if (parsed != 0.0f && !Int32.TryParse(lines[i + 6 + 8 + j], out dummy))
                    {
                        ints[resCount++] = i + 6 + 8 + j;
                    }
                }
                catch { }
            }
            return ints;
        }

        private static void transform(int zone, bool southhemi, string[] lines, string[] linesCH, string[] linesWGS, int i_x, int i_y)
        {
            String x = lines[i_x];
            String y = lines[i_y];
            double[] latlon = UTMtoWGS.UTMConverter.getLatLon(zone, double.Parse(y, NumberFormatInfo.InvariantInfo), double.Parse(x, NumberFormatInfo.InvariantInfo), southhemi);
            linesWGS[i_x] = latlon[0].ToString();
            linesWGS[i_y] = latlon[1].ToString();
            linesCH[i_x] = (Converter.WGStoChEastY(latlon[1], latlon[0])/1000).ToString();
            linesCH[i_y] = (Converter.WGStoChNorthX(latlon[1], latlon[0]/1000)/1000).ToString();
        }
    }
}
