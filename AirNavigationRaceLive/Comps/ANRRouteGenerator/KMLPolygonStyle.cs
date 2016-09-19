using SharpKml.Base;
using SharpKml.Dom;
using System;

namespace AirNavigationRaceLive.Comps.ANRRouteGenerator
{
    public static class KMLPolygonStyle
    {
        public static void AddStylesForPolygon(Document document, string styleName)
        {
            // adding a stylemap that can be referenced from the elements

            // create two styles, both contain definitions for LineStyle and PolygonStyle 
            Style stylePolyAndLine0 = new Style();
            Style stylePolyAndLine1 = new Style();

            PolygonStyle stPoly = new PolygonStyle();
            LineStyle stLine = new LineStyle();

            stPoly.Color = new Color32(80, 0, 0, 255);
            stPoly.ColorMode = ColorMode.Normal;
            stPoly.Fill = true;
            stPoly.Outline = true;

            stLine.Color = new Color32(255, 0, 0, 255);
            stLine.ColorMode = ColorMode.Normal;
            stylePolyAndLine0.Id = styleName + "_Normal";
            stylePolyAndLine0.Polygon = stPoly;
            stylePolyAndLine0.Line = stLine;
            document.AddStyle(stylePolyAndLine0);

            stylePolyAndLine1.Id = styleName + "_High";
            stylePolyAndLine1.Polygon = stPoly;
            stylePolyAndLine1.Line = stLine;
            document.AddStyle(stylePolyAndLine1);

            // create a StyleMap collection and add above Styles as a pair
            // with different Style States
            StyleMapCollection smc = new StyleMapCollection();
            smc.Id = styleName;
            Pair pr0 = new Pair();
            Pair pr1 = new Pair();

            pr0.State = StyleState.Normal;
            pr0.StyleUrl = new Uri("#" + styleName + "_Normal", UriKind.Relative);
            smc.Add(pr0);
            pr1.State = StyleState.Highlight;
            pr1.StyleUrl = new Uri("#" + styleName + "_High", UriKind.Relative);
            smc.Add(pr1);

            // add the stylemap collection to the document (in the same way as a style)
            document.AddStyle(smc);


        }
    }
}
