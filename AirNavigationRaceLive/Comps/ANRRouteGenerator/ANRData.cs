using SharpKml.Base;
using SharpKml.Dom;
using SharpKml.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Xml;

namespace AirNavigationRaceLive.Comps.ANRRouteGenerator
{
    public class ANRData
    {
        const double GATE_WIDTH = 0.3;  // gate width for start and end gate is fixed 0.6 = 2*0.3 NM  
        const double SHIFT_DIST = 0.4;  // shift border points 'inwards' (away from start- and end gate)

        private Document document = new Document();
        public Document Document
        {
            get
            {
                return document;
            }
        }

        public void generateParcour(List<List<Vector>> listOfListOfVcts, bool hasMarkers, bool showForbiddenArea, bool isStandardOrder, double channelWidth, string styleName, List<string> routeNames, double altitude)
        {
            // Document document = new Document();
            KMLPolygonStyle.AddStylesForPolygon(document, styleName);

            GeoData gc = new GeoData();
            List<Feature> lstFeaturesPROHPolygon = new List<Feature>();
            List<Feature> lstFeaturesSP = new List<Feature>();
            List<Feature> lstFeaturesFP = new List<Feature>();

            Folder folderGeneral = new Folder();
            folderGeneral.Name = "General";

            Folder folderTKOFLines = new Folder();
            folderTKOFLines.Name = "TakeOffLines";
            // add a description
            SharpKml.Dom.Description descr = new SharpKml.Dom.Description();
            descr.Text = "Add manually one or multiple take-off line(s) for your airfield<br/>into this folder for later use/import. Read more...<br/><b>Naming convention:</b>Take-off line names must start with the word <b>TKOF</b>.<br/><b>Examples:</b>TKOF_RWY21, TKOF_WEST";
            folderTKOFLines.Description = descr;
            folderGeneral.AddFeature(folderTKOFLines);

            Folder folderLiveTracking = new Folder();
            folderLiveTracking.Name = "LiveTracking";
            // folderLiveTracking.Description = "This folder can be used for LiveTracking. Andjust the PROH areas manually.</br> Then (using a text editor) set 'clampToGround' to 'relativeToGround'.</br>";

            List<Vector> lstOldRightBorder = new List<Vector>();

            for (int j = 0; j < listOfListOfVcts.Count(); j++)
            {
                List<Vector> routePoints = listOfListOfVcts[j];
                string routeName = routeNames[j];
                Folder folderBorders = new Folder();
                folderBorders.Name = routeName + "_RouteBorders";

                // calculate headings and left/right borders
                List<double> lstHeadings = gc.CalculateHeadings(routePoints);
                List<Vector> lstLeftBorder = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth, false, 1);
                List<Vector> lstRightBorder = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth, true, 1);

                // shift start and end points of border inwards
                lstLeftBorder = gc.BorderVectorWithIntersect(lstLeftBorder, SHIFT_DIST);
                lstRightBorder = gc.BorderVectorWithIntersect(lstRightBorder, SHIFT_DIST);

                // calculate the gate points for start and end point gate. Fixed width 0.6 NM
                List<Vector> lstGateLeft = gc.CalculateCurvePoint(routePoints, lstHeadings, GATE_WIDTH, false, 1);
                List<Vector> lstGateRight = gc.CalculateCurvePoint(routePoints, lstHeadings, GATE_WIDTH, true, 1);

                // SP line and FP line (use first/last points of left/right borders)
                List<Vector> lstSP, lstFP;
                lstSP = new List<Vector>();
                lstSP.Add(new Vector(lstGateRight[0].Latitude, lstGateRight[0].Longitude, altitude));
                lstSP.Add(new Vector(lstGateLeft[0].Latitude, lstGateLeft[0].Longitude, altitude));
                lstFP = new List<Vector>();
                lstFP.Add(new Vector(lstGateRight[lstGateRight.Count - 1].Latitude, lstGateRight[lstGateRight.Count - 1].Longitude, altitude));
                lstFP.Add(new Vector(lstGateLeft[lstGateLeft.Count - 1].Latitude, lstGateLeft[lstGateLeft.Count - 1].Longitude, altitude));


                List<Vector> lstLeftBorderForbiddenArea = new List<Vector>();
                List<Vector> lstRightBorderForbiddenArea = new List<Vector>();

                if (isStandardOrder)
                {
                    if (j == 0)
                    {
                        // calculate LeftBorder PROH for first route 
                        List<Vector> lstLeftBorderPROH = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth * 10, false, 1);
                        lstLeftBorderForbiddenArea = combineBorderVectorsForPolygon(lstLeftBorderPROH, lstLeftBorder);
                        setAltitude(lstLeftBorderForbiddenArea, altitude);

                    }
                    else
                    {
                        // calculate LeftBorder PROH area from LeftBorder[j] and RightBorder[j-1]
                        lstLeftBorderForbiddenArea = combineBorderVectorsForPolygon(lstOldRightBorder, lstLeftBorder);
                        setAltitude(lstLeftBorderForbiddenArea, altitude);
                    }

                    // for the last route, also calculate right border PROH
                    if (j == listOfListOfVcts.Count - 1)
                    {
                        List<Vector> lstRightBorderPROH = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth * 10, true, 1);
                        lstRightBorderForbiddenArea = combineBorderVectorsForPolygon(lstRightBorderPROH, lstRightBorder);
                        setAltitude(lstRightBorderForbiddenArea, altitude);
                    }
                }
                else
                {   // not standard order, calculate right and left PROH areas for each channel
                    List<Vector> lstLeftBorderPROH = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth * 10, false, 1);
                    List<Vector> lstRightBorderPROH = gc.CalculateCurvePoint(routePoints, lstHeadings, channelWidth * 10, true, 1);
                    lstLeftBorderForbiddenArea = combineBorderVectorsForPolygon(lstLeftBorderPROH, lstLeftBorder);
                    lstRightBorderForbiddenArea = combineBorderVectorsForPolygon(lstRightBorderPROH, lstRightBorder);
                    setAltitude(lstLeftBorderForbiddenArea, altitude);
                    setAltitude(lstRightBorderForbiddenArea, altitude);
                }

                lstOldRightBorder = lstRightBorder;

                var lineStrRoute = new SharpKml.Dom.LineString();
                var lineStrRightBorder = new SharpKml.Dom.LineString();
                var lineStrLeftBorder = new SharpKml.Dom.LineString();
                var lineStrSP = new SharpKml.Dom.LineString();
                var lineStrFP = new SharpKml.Dom.LineString();


                lineStrRoute.Coordinates = new CoordinateCollection(routePoints);
                lineStrRoute.Tessellate = true;
                lineStrRightBorder.Coordinates = new CoordinateCollection(lstRightBorder);
                lineStrLeftBorder.Coordinates = new CoordinateCollection(lstLeftBorder);
                lineStrSP.Coordinates = new CoordinateCollection(lstSP);
                lineStrFP.Coordinates = new CoordinateCollection(lstFP);

                var polygLeftBorderForbiddenArea = makeSimplePolygon(lstLeftBorderForbiddenArea, AltitudeMode.ClampToGround);
                var polygRightBorderForbiddenArea = makeSimplePolygon(lstRightBorderForbiddenArea, AltitudeMode.ClampToGround);
                var polySP = makeSimplePolygonFromLineString(lineStrSP, AltitudeMode.RelativeToGround);
                var polyFP = makeSimplePolygonFromLineString(lineStrFP, AltitudeMode.RelativeToGround);

                // Route itself
                var plm = new SharpKml.Dom.Placemark();
                plm = makeSimplePlacemark(lineStrRoute, routeName);
                //plm.Geometry = lineStrRoute;
                //plm.Name = routeName;
                folderGeneral.AddFeature(plm);
                //folderRoute.AddFeature(placemarkRoute);

                // Right border line
                plm = makeSimplePlacemark(lineStrRightBorder, routeName + "_RightBorder");
                folderBorders.AddFeature(plm);

                // Left border line
                plm = makeSimplePlacemark(lineStrLeftBorder, routeName + "_LeftBorder");
                folderBorders.AddFeature(plm);

                // SP line
                plm = makeSimplePlacemark(lineStrSP, "STARTPOINT-" + routeName);
                folderBorders.AddFeature(plm);

                // FP line
                plm = makeSimplePlacemark(lineStrFP, "ENDPOINT-" + routeName);
                folderBorders.AddFeature(plm);

                plm = makeSimplePlacemark(polySP, "STARTPOINT-" + routeName, styleName);
                lstFeaturesSP.Add(plm);

                plm = makeSimplePlacemark(polyFP, "ENDPOINT-" + routeName, styleName);
                lstFeaturesFP.Add(plm);

                folderGeneral.AddFeature(folderBorders);

                if (showForbiddenArea)
                {
                    //Folder folder = new Folder();
                    //folder.Name = "PROH-Areas " + routeName;

                    // add forbidden Area (Polygon area)
                    if (lstRightBorderForbiddenArea.Count > 0)
                    {
                        var placemarkPolyg = makeSimplePlacemark(polygRightBorderForbiddenArea, "PROH " + routeName + " Right", styleName);
                        lstFeaturesPROHPolygon.Add(placemarkPolyg);
                    }

                    if (lstLeftBorderForbiddenArea.Count > 0)
                    {
                        var placemarkPolyg = makeSimplePlacemark(polygLeftBorderForbiddenArea, "PROH " + routeName + " Left", styleName);
                        lstFeaturesPROHPolygon.Add(placemarkPolyg);
                    }
                    //document.AddFeature(folder);
                }

                if (hasMarkers)
                {
                    // OPTIONAL: add markers if required for all route points
                    Folder folder = new Folder();
                    folder.Name = routeName + "_RouteMarkers";
                    for (int i = 0; i < routePoints.Count; i++)
                    {
                        Placemark pm = new Placemark();
                        SharpKml.Dom.Point pt = new SharpKml.Dom.Point();
                        pt.Coordinate = new Vector(routePoints[i].Latitude, routePoints[i].Longitude);
                        pm.Geometry = pt;
                        pm.Name = String.Format("TP_{0}_{1}", i.ToString().PadLeft(2, '0'), routeName);
                        if (i == 0)
                        {
                            pm.Name = String.Format("SP_{0}", routeName);
                        }
                        if (i == routePoints.Count - 1)
                        {
                            pm.Name = String.Format("FP_{0}", routeName);
                        }
                        folder.AddFeature(pm);
                    }
                    folderGeneral.AddFeature(folder);

                }
            }

            for (int i = 0; i < lstFeaturesPROHPolygon.Count; i++)
            {
                folderLiveTracking.AddFeature(lstFeaturesPROHPolygon[i].Clone());
            }
            for (int i = 0; i < lstFeaturesSP.Count; i++)
            {
                folderLiveTracking.AddFeature(lstFeaturesSP[i].Clone());
            }
            for (int i = 0; i < lstFeaturesFP.Count; i++)
            {
                folderLiveTracking.AddFeature(lstFeaturesFP[i].Clone());
            }

            document.AddFeature(folderGeneral);
            document.AddFeature(folderLiveTracking);
            //return document;
        }

        /// <summary>
        /// Exporting parcour data (Route center lines, Start/END lines, PROH zones and TKOF line ) coordinates to a text file
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <returns>List of string</returns>
        public List<string> exportParcourCoordinates(XmlDocument xmlDoc)
        {
            CultureInfo ci = CultureInfo.InvariantCulture;
            List<string> lst = new List<string>();
            XmlNodeList nodeList;
            // note the xPath: TKOF lines may be in a lower folder
            // leave upper part unchanged other wise we get duplicate STARTPOINT and ENDPOINT records
            string xPath = @"/*[local-name()='kml']/*[local-name()='Document']/*[local-name()='Folder']/*[local-name()='Placemark']"
           + "["
           + "./*[local-name()='name']='A' or ./*[local-name()='name']='B' or ./*[local-name()='name']='C' or ./*[local-name()='name']='D' "
           + "or starts-with(./*[local-name()='name'],'PROH') "
           + "or starts-with(./*[local-name()='name'],'STARTPOINT-') "
           + "or starts-with(./*[local-name()='name'],'ENDPOINT-') "
           + "or starts-with(./*[local-name()='name'],'NBLINE') "
           + "or starts-with(./*[local-name()='name'],'TKOF')"
           + "]"
           + " | "
           + "//*[local-name()='Folder']/*[local-name()='Placemark']"
           + "[starts-with(./*[local-name()='name'],'TKOF')]";

            string xPathName = @"./*[local-name()='name']";
            string xPathCoord = @".//*[local-name()='coordinates']";
            nodeList = xmlDoc.SelectNodes(xPath);
            foreach (XmlNode nod in nodeList)
            {
                XmlNode nodeName = nod.SelectSingleNode(xPathName);
                string name = nodeName.InnerText.ToString().Trim();
                if (name == "A" || name == "B" || name == "C" || name == "D")
                {
                    name = @"Route Center Line, Route " + name; // add some additional text for the Route
                }
                lst.Add(name);
                XmlNode nodeCoord = nod.SelectSingleNode(xPathCoord);

                string kmlCoordStr = nodeCoord.InnerText.ToString().Trim();
                List<Point> lstPoint = new List<Point>();
                lstPoint = Helper.Importer.getPointsFromKMLCoordinates(kmlCoordStr);
                for (int i = 0; i < lstPoint.Count; i++)
                {
                    if ((name.StartsWith("STARTPOINT-") || name.StartsWith("ENDPOINT-")) && i==2)
                    {
                        // do nothing on STARTPOINT / ENDPOINT for the third coordinate point
                        // (the third point is the same as the first for technical reasons)
                    }
                    else
                    {
                    lst.Add(lstPoint[i].latitude.ToString(ci) + "," + lstPoint[i].longitude.ToString(ci));
                    }
                }
            }
            return lst;
        }

        public List<Vector> combineBorderVectorsForPolygon(List<Vector> lstVectors1, List<Vector> lstVectors2)
        {
            // input: two vectors for channel borderlines
            // check if they have the same orientation
            // if yes: invert lstvector2, else do nothing
            // append to lstvector1, then add first point of lstVectors1

            List<Vector> lstVct = new List<Vector>();
            if (GeoData.GetDist(lstVectors1[0], lstVectors2[0]) < GeoData.GetDist(lstVectors1[0], lstVectors2[lstVectors2.Count - 1]))
            {
                //same orientation, so must reverse lstVector2
                lstVectors2.Reverse();
            }
            lstVct.AddRange(lstVectors1);
            lstVct.AddRange(lstVectors2);
            lstVct.Add(lstVectors1[0]);

            return lstVct;
        }

        public void setAltitude(List<Vector> lstVct, double Altitude)
        {
            // input: two vectors for channel borderlines
            // check if they have the same orientation
            // if yes: invert lstvector2, else do nothing
            // append to lstvector1, then add first point of lstVectors1

            for (int i = 0; i < lstVct.Count; i++)
            {
                lstVct[i].Altitude = Altitude;
            }
        }

        public SharpKml.Dom.Polygon makeSimplePolygon(List<Vector> lstVct, AltitudeMode altMode)
        {
            var pg = new SharpKml.Dom.Polygon();
            pg.Tessellate = true;
            pg.Extrude = true;
            pg.AltitudeMode = altMode;
            var linring = new LinearRing();

            OuterBoundary obdr = new OuterBoundary();
            linring.Coordinates = new CoordinateCollection(lstVct);
            obdr.LinearRing = linring;
            obdr.LinearRing.CalculateBounds();
            pg.OuterBoundary = obdr;
            return pg;
        }

        public SharpKml.Dom.Polygon makeSimplePolygonFromLineString(SharpKml.Dom.LineString lineString, AltitudeMode altMode)
        {
            var pg = new SharpKml.Dom.Polygon();
            List<Vector> lstVct = new List<Vector>();
            lstVct.AddRange(lineString.Coordinates);
            lstVct.Add(lineString.Coordinates.First());
            pg.Extrude = true;
            pg.AltitudeMode = altMode;
            var linring = new LinearRing();

            OuterBoundary obdr = new OuterBoundary();
            linring.Coordinates = new CoordinateCollection(lstVct);
            obdr.LinearRing = linring;
            obdr.LinearRing.CalculateBounds();
            pg.OuterBoundary = obdr;
            return pg;
        }

        public SharpKml.Dom.Placemark makeSimplePlacemark(SharpKml.Dom.Polygon poly, string name, string styleName)
        {
            var pm = new SharpKml.Dom.Placemark();
            pm.Geometry = poly;
            pm.Name = name;
            pm.StyleUrl = new Uri("#" + styleName, UriKind.Relative);
            return pm;
        }

        public SharpKml.Dom.Placemark makeSimplePlacemark(SharpKml.Dom.LineString lineString, string name)
        {
            var pm = new SharpKml.Dom.Placemark();
            pm.Geometry = lineString;
            pm.Name = name;
            return pm;
        }
    }
}

