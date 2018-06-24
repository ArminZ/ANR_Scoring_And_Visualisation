using SharpKml.Base;
using SharpKml.Dom;
using System;

namespace AirNavigationRaceLive.Comps.ANRRouteGenerator
{
    public static class KMLPolygonStyle
    {
        public static void AddStylesForPolygon(Document document, string[] styleNames)
        {
            // adding a stylemap that can be referenced from the elements
            Color32[] polyColors = { new Color32(80, 0, 0, 255), new Color32(255, 255, 255, 255) };
            Color32[] lineColors = { new Color32(255, 0, 0, 255), new Color32(255, 255, 255, 255) };
            bool[] polyFills = { true, false };
            bool[] polyOutlines = { true, true };
            // create two styles, both contain definitions for LineStyle and PolygonStyle 

            for (int i = 0; i < styleNames.Length; i++)
            {
                StyleMapCollection smc = new StyleMapCollection();

                Style[] stylePolyAndLine = { new Style(), new Style() };

                PolygonStyle stPoly = new PolygonStyle();
                stPoly.Color = polyColors[i];
                stPoly.ColorMode = ColorMode.Normal;
                stPoly.Fill = polyFills[i];
                stPoly.Outline = polyOutlines[i];

                LineStyle stLine = new LineStyle();
                stLine.Color = lineColors[i];
                stLine.ColorMode = ColorMode.Normal;

                stylePolyAndLine[0].Id = styleNames[i] + "_Normal";
                stylePolyAndLine[0].Polygon = stPoly;
                stylePolyAndLine[0].Line = stLine;
                document.AddStyle(stylePolyAndLine[0]);

                stylePolyAndLine[1].Id = styleNames[i] + "_High";
                stylePolyAndLine[1].Polygon = stPoly;
                stylePolyAndLine[1].Line = stLine;
                document.AddStyle(stylePolyAndLine[1]);

                // create a StyleMap collection and add above Styles as a pair
                // with different Style States
                smc.Id = styleNames[i];
                Pair[] pr = { new Pair(), new Pair() };
                pr[0].State = StyleState.Normal;
                pr[0].StyleUrl = new Uri("#" + styleNames[i] + "_Normal", UriKind.Relative);
                smc.Add(pr[0]);
                pr[1].State = StyleState.Highlight;
                pr[1].StyleUrl = new Uri("#" + styleNames[i] + "_High", UriKind.Relative);
                smc.Add(pr[1]);
                // add the stylemap collection to the document (in the same way as a style)
                document.AddStyle(smc);
            }
        }
    }
}
