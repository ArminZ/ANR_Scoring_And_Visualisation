using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Shapes;
using PdfSharp.Drawing.Layout;
using AirNavigationRaceLive.Model;
using System.Drawing.Imaging;
using System.Drawing;
using AirNavigationRaceLive.ModelExtensions;
using System.Text.RegularExpressions;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class PDFCreator
    {
        static XFont verdana16Bold = new XFont("Verdana", 16, XFontStyle.Bold);
        static XFont verdana14Bold = new XFont("Verdana", 14, XFontStyle.Bold);
        static XFont verdana13Bold = new XFont("Verdana", 13, XFontStyle.Bold);
        static XFont verdana11Bold = new XFont("Verdana", 11, XFontStyle.Bold);
        static XFont verdana10Bold = new XFont("Verdana", 10, XFontStyle.Bold);
        static XFont verdana9Bold = new XFont("Verdana", 9, XFontStyle.Bold);
        static XFont verdana10Reg = new XFont("Verdana", 10, XFontStyle.Regular);
        static XFont verdana9Reg = new XFont("Verdana", 9, XFontStyle.Regular);

        public static void CreateTeamsPDF(List<TeamSet> teams, Client.DataAccess c, String pathToPDF)
        {

            Document doc = new Document();
            doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
            doc.Info.Comment = "Generated from ANRL Client on " + DateTime.Now.ToString();
            doc.Info.Keywords = "ANRL Crewlist";
            doc.Info.Subject = "Crewlist";
            doc.Info.Title = "Crewlist";
            doc.UseCmykColor = true;
            doc.DefaultPageSetup.PageFormat = PageFormat.A4;
            doc.DefaultPageSetup.Orientation = Orientation.Landscape;
            doc.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(1);
            doc.DefaultPageSetup.TopMargin = Unit.FromCentimeter(1);
            doc.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            doc.DefaultPageSetup.RightMargin = Unit.FromCentimeter(1);


            Section sec = doc.AddSection();

            AddCompetitionAndLogo(c, sec);
            sec.AddParagraph("");
            sec.AddParagraph("Participants list");
            sec.AddParagraph("");

            Table table = sec.AddTable();
            table.Borders.Visible = true;

            //table.AddColumn(Unit.FromCentimeter(0.7));
            table.AddColumn(Unit.FromCentimeter(2.5));
            table.AddColumn();
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn();

            Row row = table.AddRow();
            row.Shading.Color = Colors.Gray;
            //row.Cells[0].AddParagraph("ID");
            row.Cells[0].AddParagraph("CNumber");
            row.Cells[1].AddParagraph("Nationality");
            row.Cells[2].AddParagraph("Pilot Lastname");
            row.Cells[3].AddParagraph("Pilot Firstname");
            row.Cells[4].AddParagraph("Navigator Lastname");
            row.Cells[5].AddParagraph("Navigator Firstname");
            row.Cells[6].AddParagraph("AC");

            foreach (TeamSet t in teams)
            {
                Row r = table.AddRow();
                //r.Cells[0].AddParagraph(t.ID.ToString());
                r.Cells[0].AddParagraph(t.CNumber);
                r.Cells[1].AddParagraph(t.Nationality);
                SubscriberSet pilot = t.Pilot;
                r.Cells[2].AddParagraph(pilot.LastName);
                r.Cells[3].AddParagraph(pilot.FirstName);
                if (t.Navigator != null)
                {
                    SubscriberSet navigator = t.Navigator;
                    r.Cells[4].AddParagraph(navigator.LastName);
                    r.Cells[5].AddParagraph(navigator.FirstName);
                }
                r.Cells[6].AddParagraph(t.AC);
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(pathToPDF);

            Process.Start(pathToPDF);
        }
        public static void CreateParcourPDF(PDFSize pdfSize, bool showCalcTable, double scaleFactor, ParcourPictureBox picBox, Client.DataAccess c, String parcourName, String pathToPDF, String overlayText)
        {
            //PdfDocument doc = new PdfDocument(@"Resources\PDFTemplates\Competition_Map.pdf");
            PdfDocument doc = new PdfDocument();
            doc.Info.Author = "Luc@sharpsoft.ch";
            doc.Info.Keywords = "ANRL Parcour Printout";
            doc.Info.Subject = "Parcour Printout generated from ANRL Client on " + DateTime.Now.ToString();
            doc.Info.Title = "Parcour Printout";
            doc.Options.ColorMode = PdfColorMode.Cmyk;
            doc.Language = "EN";
            doc.PageLayout = PdfPageLayout.SinglePage;

            PdfPage page = doc.AddPage();
            page.Orientation = PdfSharp.PageOrientation.Landscape;
            page.Size = (PdfSharp.PageSize)pdfSize;

            XGraphics gfx = XGraphics.FromPdfPage(page);

            AddLogo(gfx, page);

            #region Competition and Parcour text

            gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
                verdana16Bold, XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(2)));

            gfx.DrawString("Parcour: " + parcourName,
                verdana14Bold, XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(3)));

            #endregion

            XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage);

            double distX = picBox.GetXDistanceKM() / scaleFactor;  // ScaleFactor 1.0 = 1:100 000 in cm
            double distY = picBox.GetYDistanceKM() / scaleFactor;

            gfx.DrawImage(image, XUnit.FromCentimeter(1), XUnit.FromCentimeter(4), page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

            if (showCalcTable && (int)pdfSize == (int)PdfSharp.PageSize.A4)
            {
                #region  Show calculation table for A4, if wanted

                double startX = 190;

                List<XPoint> points = new List<XPoint>();
                points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40)));
                points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * 5), Unit.FromMillimeter(40)));
                points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * 5), Unit.FromMillimeter(40 + 9)));
                points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40 + 9)));
                points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40)));
                gfx.DrawLines(XPens.Black, points.ToArray());

                gfx.DrawString("Comp. Nr:",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(40 + 7)));

                gfx.DrawString("Route:",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 18 * 3 + 1), Unit.FromMillimeter(40 + 7)));

                double startY = 40 + 9 + 5;

                double colWidth = 18;
                double rowHeight = 9;


                for (int i = 0; i < 16; i++)
                {
                    points = new List<XPoint>();
                    points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(startY + i * rowHeight)));
                    points.Add(new XPoint(Unit.FromMillimeter(startX + colWidth * 5), Unit.FromMillimeter(startY + i * rowHeight)));
                    gfx.DrawLines(XPens.Black, points.ToArray());
                }

                for (int i = 0; i < 6; i++)
                {
                    points = new List<XPoint>();
                    points.Add(new XPoint(Unit.FromMillimeter(startX + i * colWidth), Unit.FromMillimeter(startY)));
                    points.Add(new XPoint(Unit.FromMillimeter(startX + i * colWidth), Unit.FromMillimeter(startY + 15 * rowHeight)));
                    gfx.DrawLines(XPens.Black, points.ToArray());
                }

                gfx.DrawString("Dist.",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + colWidth * 1 + 1), Unit.FromMillimeter(startY + 7)));

                gfx.DrawString("TT",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + colWidth * 2 + 1), Unit.FromMillimeter(startY + 7)));

                gfx.DrawString("EET",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + colWidth * 3 + 1), Unit.FromMillimeter(startY + 7)));

                gfx.DrawString("ETO",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + colWidth * 4 + 1), Unit.FromMillimeter(startY + 7)));

                gfx.DrawString("T/O",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 1 + 7)));

                gfx.DrawString("SP",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 2 + 7)));

                for (int i = 3; i < 13; i++)
                {
                    gfx.DrawString("TP" + (i - 3 + 1),
                        verdana14Bold, XBrushes.Black,
                        new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * i + 7)));
                }
                gfx.DrawString("FP",
                    verdana14Bold, XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 13 + 7)));

                gfx.DrawImage(XImage.FromFile(@"Resources\Summe.png"),
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 14 + 2)));

                #endregion
            }

            #region Show overlay text (map scale, time, distance etc.)
            if (!string.IsNullOrEmpty(overlayText.Trim()))
            {
                XRect rect = new XRect(XUnit.FromCentimeter(19.0), XUnit.FromCentimeter(1.5), Unit.FromCentimeter(5.0), Unit.FromCentimeter(2.0));
                gfx.DrawRectangle(XBrushes.White, rect);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.DrawString(overlayText, verdana10Reg, XBrushes.Black, rect, XStringFormats.TopLeft);
            }
            #endregion

            doc.Save(pathToPDF);
            doc.Close();
            Process.Start(pathToPDF);
        }
        public static void CreateRankingListPDF(Client.DataAccess c, QualificationRoundSet qRnd, List<ComboBoxFlights> qRndFlights, String pathToPDF)
        {
            List<Toplist> toplist = new List<Toplist>();
            foreach (ComboBoxFlights cbct in qRndFlights)
            {
                int sum = 0;
                foreach (PenaltySet penalty in cbct.flight.PenaltySet)
                {
                    sum += penalty.Points;
                }
                toplist.Add(new Toplist(cbct.flight, sum));
            }
            toplist.Sort();

            Document doc = new Document();
            doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
            doc.Info.Comment = "Generated from ANRL Client on " + DateTime.Now.ToString();
            doc.Info.Keywords = "ANRL Toplist";
            doc.Info.Subject = "Toplist";
            doc.Info.Title = "Toplist";
            doc.UseCmykColor = true;
            doc.DefaultPageSetup.PageFormat = PageFormat.A4;
            doc.DefaultPageSetup.Orientation = Orientation.Landscape;
            doc.DefaultPageSetup.BottomMargin = Unit.FromCentimeter(1);
            doc.DefaultPageSetup.TopMargin = Unit.FromCentimeter(1);
            doc.DefaultPageSetup.LeftMargin = Unit.FromCentimeter(1.5);
            doc.DefaultPageSetup.RightMargin = Unit.FromCentimeter(1);


            Section sec = doc.AddSection();

            AddCompetitionAndLogo(c, sec);
            sec.AddParagraph("");
            sec.AddParagraph("Ranking List: " + qRnd.Name);
            sec.AddParagraph("");

            Table table = sec.AddTable();
            table.Borders.Visible = true;

            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2.5));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));
            table.AddColumn(Unit.FromCentimeter(4));

            Row row = table.AddRow();
            row.Shading.Color = Colors.Gray;
            row.Cells[0].AddParagraph("Rank");
            row.Cells[1].AddParagraph("Points");
            row.Cells[2].AddParagraph("Nationality");
            row.Cells[3].AddParagraph("Pilot Lastname");
            row.Cells[4].AddParagraph("Pilot Firstname");
            row.Cells[5].AddParagraph("Navigator Lastname");
            row.Cells[6].AddParagraph("Navigator Firstname");

            int oldsum = -1;
            int prevRank = 0;
            int rank = 0;
            foreach (Toplist top in toplist)
            {
                rank++;
                TeamSet t = top.ct.TeamSet;
                Row r = table.AddRow();
                if (rank > 1 && oldsum == top.sum)  // we have a shared rank
                {
                    r.Cells[0].AddParagraph(prevRank + "");
                }
                else  // the normal case
                {
                    prevRank = rank;
                    r.Cells[0].AddParagraph(rank + "");
                }
                r.Cells[1].AddParagraph(top.sum.ToString());
                r.Cells[2].AddParagraph(t.Nationality);
                SubscriberSet pilot = t.Pilot;
                r.Cells[3].AddParagraph(pilot.LastName);
                r.Cells[4].AddParagraph(pilot.FirstName);
                if (t.Navigator != null)
                {
                    SubscriberSet navigator = t.Navigator;
                    r.Cells[5].AddParagraph(navigator.LastName);
                    r.Cells[6].AddParagraph(navigator.FirstName);
                }
                oldsum = top.sum;
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(pathToPDF);

            Process.Start(pathToPDF);
        }
        //public static void CreateResultPDF(VisualisationPictureBox picBox, Client.DataAccess c, QualificationRoundSet qRnd, List<ComboBoxFlights> qRndFlights, String pathToPDF)
        //{
        //    int counter = 0;
        //    List<FlightSet> tempList = new List<FlightSet>();
        //    foreach (ComboBoxFlights cbct in qRndFlights)
        //    {
        //        GC.Collect();
        //        PdfDocument doc = new PdfDocument();
        //        doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
        //        doc.Info.Keywords = "ANRL Results Printout";
        //        doc.Info.Subject = "Results Printout generated from ANRL Client on " + DateTime.Now.ToString();
        //        doc.Info.Title = "Results Printout";
        //        doc.Options.ColorMode = PdfColorMode.Cmyk;
        //        doc.Language = "EN";
        //        doc.PageLayout = PdfPageLayout.SinglePage;

        //        tempList.Clear();
        //        tempList.Add(cbct.flight);
        //        picBox.SetData(tempList);

        //        PdfPage page = doc.AddPage();
        //        page.Orientation = PdfSharp.PageOrientation.Landscape;
        //        page.Size = PdfSharp.PageSize.A4;
        //        double scaleFactor = 2.0;

        //        XGraphics gfx = XGraphics.FromPdfPage(page);
        //        XTextFormatter tf = new XTextFormatter(gfx);
        //        XRect rect = new XRect();

        //        AddLogo(gfx, page);

        //        XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage.VaryQualityLevel());

        //        double distX = picBox.GetXDistanceKM() / scaleFactor;//1:200 000 in cm
        //        double distY = picBox.GetYDistanceKM() / scaleFactor;//1:200 000 in cm

        //        gfx.DrawImage(image, XUnit.FromCentimeter(2).Point, XUnit.FromCentimeter(3).Point, page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

        //        #region Header data (Competition, Qualification round, Crew)

        //        gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
        //            verdana13Bold, XBrushes.Black,
        //            new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(1.5)));

        //        gfx.DrawString("Q-Round: " + qRnd.Name,
        //            verdana11Bold, XBrushes.Black,
        //            new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.1)));

        //        gfx.DrawString("Crew: " + getTeamDsc(c, cbct.flight),
        //            verdana11Bold, XBrushes.Black,
        //            new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.7)));

        //        #endregion

        //        #region Write table with Penalty points

        //        int sum = 0;
        //        int line = 0;
        //        int offsetLine = 20;
        //        gfx.DrawString("Points ", verdana11Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3)));
        //        gfx.DrawString("Reason ", verdana11Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3)));

        //        line++;
        //        foreach (PenaltySet penalty in cbct.flight.PenaltySet)
        //        {
        //            sum += penalty.Points;

        //            // Penalty points, aligned right
        //            rect = new XRect(
        //                new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)),
        //                new XPoint(XUnit.FromCentimeter(offsetLine + 1.3), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
        //            //gfx.DrawRectangle(XBrushes.Yellow, rect);
        //            tf.Alignment = XParagraphAlignment.Right;
        //            tf.DrawString(penalty.Points.ToString(), verdana9Reg, XBrushes.Black, rect, XStringFormats.TopLeft);
        //            //gfx.DrawString(penalty.Points.ToString(), verdana9Reg, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));
                    
        //            // Penalty explanation, aligned left
        //            //List<String> reason = getWrapped(penalty.Reason);
        //            List<string> reason = penalty.Reason.SplitOn(40);
        //            foreach (String s in reason)
        //            {
        //                rect = new XRect(
        //                          new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3.0 + line * 0.4)),
        //                          new XPoint(XUnit.FromCentimeter(offsetLine + 9), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
        //                tf.Alignment = XParagraphAlignment.Left;
        //                //gfx.DrawRectangle(XBrushes.Yellow, rect);
        //                tf.DrawString(s, verdana9Reg, XBrushes.Black, rect, XStringFormats.TopLeft);
        //                //gfx.DrawString(s, verdana9Reg, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));
        //                line++;
        //            }
        //        }
        //        line++;

        //        // Penalty total points, aligned right
        //        rect = new XRect(
        //           new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)),
        //           new XPoint(XUnit.FromCentimeter(offsetLine + 1.3), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
        //        tf.Alignment = XParagraphAlignment.Right;
        //        tf.DrawString(sum.ToString(), verdana9Bold, XBrushes.Black, rect, XStringFormats.TopLeft);
        //        // gfx.DrawString(sum.ToString(), verdana10Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));

        //        // Penalty text, aligned left
        //        rect = new XRect(
        //           new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3.0 + line * 0.4)),
        //           new XPoint(XUnit.FromCentimeter(offsetLine + 9), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
        //        tf.Alignment = XParagraphAlignment.Left;
        //        tf.DrawString("Total Points", verdana9Bold, XBrushes.Black, rect, XStringFormats.TopLeft);
        //        //gfx.DrawString("Total Points", verdana10Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));

        //        #endregion

        //        String path = pathToPDF.Replace(".pdf", (counter++ + "_" + getTeamDsc(c, cbct.flight) + ".pdf"));
        //        doc.Save(path);
        //        doc.Close();
        //        Process.Start(path);
        //    }
        //}
        public static void CreateResultPDF(ParcourPictureBox picBox, Client.DataAccess c, QualificationRoundSet qRnd, List<ComboBoxFlights> qRndFlights, String pathToPDF)
        {
            int counter = 0;
            List<FlightSet> tempList = new List<FlightSet>();
            foreach (ComboBoxFlights cbct in qRndFlights)
            {
                GC.Collect();
                PdfDocument doc = new PdfDocument();
                doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
                doc.Info.Keywords = "ANRL Results Printout";
                doc.Info.Subject = "Results Printout generated from ANRL Client on " + DateTime.Now.ToString();
                doc.Info.Title = "Results Printout";
                doc.Options.ColorMode = PdfColorMode.Cmyk;
                doc.Language = "EN";
                doc.PageLayout = PdfPageLayout.SinglePage;

                tempList.Clear();
                tempList.Add(cbct.flight);
                picBox.SetData(tempList);

                PdfPage page = doc.AddPage();
                page.Orientation = PdfSharp.PageOrientation.Landscape;
                page.Size = PdfSharp.PageSize.A4;
                double scaleFactor = 2.0;

                XGraphics gfx = XGraphics.FromPdfPage(page);
                XTextFormatter tf = new XTextFormatter(gfx);
                XRect rect = new XRect();

                AddLogo(gfx, page);

                XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage.VaryQualityLevel());

                double distX = picBox.GetXDistanceKM() / scaleFactor;//1:200 000 in cm
                double distY = picBox.GetYDistanceKM() / scaleFactor;//1:200 000 in cm

                gfx.DrawImage(image, XUnit.FromCentimeter(2).Point, XUnit.FromCentimeter(3).Point, page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

                #region Header data (Competition, Qualification round, Crew)

                gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
                    verdana13Bold, XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(1.5)));

                gfx.DrawString("Q-Round: " + qRnd.Name,
                    verdana11Bold, XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.1)));

                gfx.DrawString("Crew: " + getTeamDsc(c, cbct.flight),
                    verdana11Bold, XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.7)));

                #endregion

                #region Write table with Penalty points

                int sum = 0;
                int line = 0;
                int offsetLine = 20;
                gfx.DrawString("Points ", verdana11Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3)));
                gfx.DrawString("Reason ", verdana11Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3)));

                line++;
                foreach (PenaltySet penalty in cbct.flight.PenaltySet)
                {
                    sum += penalty.Points;

                    // Penalty points, aligned right
                    rect = new XRect(
                        new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)),
                        new XPoint(XUnit.FromCentimeter(offsetLine + 1.3), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
                    //gfx.DrawRectangle(XBrushes.Yellow, rect);
                    tf.Alignment = XParagraphAlignment.Right;
                    tf.DrawString(penalty.Points.ToString(), verdana9Reg, XBrushes.Black, rect, XStringFormats.TopLeft);
                    //gfx.DrawString(penalty.Points.ToString(), verdana9Reg, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));

                    // Penalty explanation, aligned left
                    //List<String> reason = getWrapped(penalty.Reason);
                    List<string> reason = penalty.Reason.SplitOn(40);
                    foreach (String s in reason)
                    {
                        rect = new XRect(
                                  new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3.0 + line * 0.4)),
                                  new XPoint(XUnit.FromCentimeter(offsetLine + 9), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
                        tf.Alignment = XParagraphAlignment.Left;
                        //gfx.DrawRectangle(XBrushes.Yellow, rect);
                        tf.DrawString(s, verdana9Reg, XBrushes.Black, rect, XStringFormats.TopLeft);
                        //gfx.DrawString(s, verdana9Reg, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));
                        line++;
                    }
                }
                line++;

                // Penalty total points, aligned right
                rect = new XRect(
                   new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)),
                   new XPoint(XUnit.FromCentimeter(offsetLine + 1.3), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
                tf.Alignment = XParagraphAlignment.Right;
                tf.DrawString(sum.ToString(), verdana9Bold, XBrushes.Black, rect, XStringFormats.TopLeft);
                // gfx.DrawString(sum.ToString(), verdana10Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));

                // Penalty text, aligned left
                rect = new XRect(
                   new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3.0 + line * 0.4)),
                   new XPoint(XUnit.FromCentimeter(offsetLine + 9), XUnit.FromCentimeter(3.0 + line * 0.4 + 0.4)));
                tf.Alignment = XParagraphAlignment.Left;
                tf.DrawString("Total Points", verdana9Bold, XBrushes.Black, rect, XStringFormats.TopLeft);
                //gfx.DrawString("Total Points", verdana10Bold, XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));

                #endregion

                String path = pathToPDF.Replace(".pdf", (counter++ + "_" + getTeamDsc(c, cbct.flight) + ".pdf"));
                doc.Save(path);
                doc.Close();
                Process.Start(path);
            }
        }

        private static string getTeamDsc(Client.DataAccess c, FlightSet flight)
        {
            TeamSet team = flight.TeamSet;
            SubscriberSet pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(team.CNumber).Append(" ");
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator != null)
            {
                SubscriberSet navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }

        internal static void CreateStartListPDF(QualificationRoundSet qRnd, Client.DataAccess Client, string pathToPDF)
        {
            Document doc = new Document();
            doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
            doc.Info.Comment = "Generated from ANRL Client on " + DateTime.Now.ToString();
            doc.Info.Keywords = "ANRL Start List";
            doc.Info.Subject = "Start List";
            doc.Info.Title = "Start List";
            doc.UseCmykColor = true;
            doc.DefaultPageSetup.PageFormat = PageFormat.A4;
            doc.DefaultPageSetup.Orientation = Orientation.Landscape;

            Section sec = doc.AddSection();

            AddCompetitionAndLogo(Client, sec);

            sec.AddParagraph("Qualification Round: " + qRnd.Name);
            sec.AddParagraph("Start List");
            sec.AddParagraph("");
            sec.AddParagraph("");

            Table table = sec.AddTable();
            table.Borders.Visible = true;

            table.AddColumn(Unit.FromCentimeter(1.2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(3));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(2));
            table.AddColumn(Unit.FromCentimeter(1.5));

            Row row = table.AddRow();
            row.Shading.Color = Colors.Gray;
            row.Cells[0].AddParagraph("Start ID");
            row.Cells[1].AddParagraph("Crew Number");
            row.Cells[2].AddParagraph("AC");
            row.Cells[3].AddParagraph("Pilot Lastname");
            row.Cells[4].AddParagraph("Pilot Firstname");
            row.Cells[5].AddParagraph("Navigator Lastname");
            row.Cells[6].AddParagraph("Navigator Firstname");
            row.Cells[7].AddParagraph("Take Off (UTC)");
            row.Cells[8].AddParagraph("Start Gate (UTC)");
            row.Cells[9].AddParagraph("End Gate (UTC)");
            row.Cells[10].AddParagraph("Route");

            foreach (FlightSet ct in qRnd.FlightSet.OrderBy(x => x.TimeTakeOff).ThenBy(x => x.Route))
            {
                Row r = table.AddRow();
                r.Cells[0].AddParagraph(ct.StartID.ToString());
                TeamSet teams = ct.TeamSet;
                r.Cells[1].AddParagraph(teams.CNumber);
                r.Cells[2].AddParagraph(teams.AC);
                SubscriberSet pilot = teams.Pilot;
                r.Cells[3].AddParagraph(pilot.LastName);
                r.Cells[4].AddParagraph(pilot.FirstName);
                if (teams.Navigator != null)
                {
                    SubscriberSet navigator = teams.Navigator;
                    r.Cells[5].AddParagraph(navigator.LastName);
                    r.Cells[6].AddParagraph(navigator.FirstName);
                }
                r.Cells[7].AddParagraph(new DateTime(ct.TimeTakeOff).ToString("HH:mm:ss"));
                r.Cells[8].AddParagraph(new DateTime(ct.TimeStartLine).ToString("HH:mm:ss"));
                r.Cells[9].AddParagraph(new DateTime(ct.TimeEndLine).ToString("HH:mm:ss"));
                r.Cells[10].AddParagraph(Enum.GetName(Route.A.GetType(), ct.Route));
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(pathToPDF);
            Process.Start(pathToPDF);
        }

        private static void AddCompetitionAndLogo(Client.DataAccess c, Section sec)
        {
            String competitionName = "Competition: " + c.SelectedCompetition.Name;
            Paragraph pg = sec.AddParagraph();
            pg.Format.Alignment = ParagraphAlignment.Left;
            pg.Format.KeepTogether = false;
            pg.Format.KeepWithNext = false;
            pg.Format.AddTabStop(Unit.FromCentimeter(21));

            FormattedText ft = pg.AddFormattedText(competitionName);
            ft.Bold = true;
            ft.Size = Unit.FromPoint(16);
            pg.AddTab();


            MigraDoc.DocumentObjectModel.Shapes.Image logo = pg.AddImage(@"Resources\ANR_LOGO.jpg");
            logo.Height = Unit.FromCentimeter(1.912);
            logo.Width = Unit.FromCentimeter(2.873);
            logo.LockAspectRatio = true;
            logo.Left = Unit.FromCentimeter(24);
            logo.Top = Unit.FromCentimeter(0);
        }

        private static void AddLogo(XGraphics gfx, PdfPage page)
        {
            XImage logo = XImage.FromFile(@"Resources\ANR_LOGO.jpg");
            XRect point = new XRect(page.Width - XUnit.FromCentimeter(4), XUnit.FromCentimeter(1), Unit.FromCentimeter(2.873), Unit.FromCentimeter(1.912));
            gfx.DrawImage(logo, point);
        }

        //public static void CreateParcourPDFA3(double scaleFactor, ParcourPictureBox picBox, Client.DataAccess c, String parcourName, String pathToPDF, String overlayText)
        //{
        //    PdfDocument doc = new PdfDocument();
        //    doc.Info.Author = "Luc@sharpsoft.ch";
        //    doc.Info.Keywords = "ANRL Parcour Printout";
        //    doc.Info.Subject = "Parcour Printout generated from ANRL Client on " + DateTime.Now.ToString();
        //    doc.Info.Title = "Parcour Printout";
        //    doc.Options.ColorMode = PdfColorMode.Cmyk;
        //    doc.Language = "EN";
        //    doc.PageLayout = PdfPageLayout.SinglePage;

        //    PdfPage page = doc.AddPage();
        //    page.Orientation = PdfSharp.PageOrientation.Landscape;
        //    page.Size = PdfSharp.PageSize.A3;

        //    XGraphics gfx = XGraphics.FromPdfPage(page);
        //    AddLogo(gfx, page);

        //    gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
        //        new XFont("Verdana", 16, XFontStyle.Bold), XBrushes.Black,
        //        new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(2)));

        //    gfx.DrawString("Parcour: " + parcourName,
        //        new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
        //        new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(3)));

        //    XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage);

        //    double distX = picBox.GetXDistanceKM() / scaleFactor;  //1:100 000 in cm
        //    double distY = picBox.GetYDistanceKM() / scaleFactor; //1:100 000 in cm

        //    gfx.DrawImage(image, XUnit.FromCentimeter(1), XUnit.FromCentimeter(4), page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

        //    if (!string.IsNullOrEmpty(overlayText.Trim()))
        //    {
        //        //       XRect rect = new XRect(new XPoint(Unit.FromCentimeter(page.Width.Centimeter - 8), Unit.FromCentimeter(page.Height.Centimeter-4)), new XSize(Unit.FromCentimeter(8), Unit.FromCentimeter(3.6)));
        //        XRect rect = new XRect(XUnit.FromCentimeter(19.0), XUnit.FromCentimeter(1.5), Unit.FromCentimeter(5.0), Unit.FromCentimeter(2.0));
        //        gfx.DrawRectangle(XBrushes.White, rect);
        //        XTextFormatter tf = new XTextFormatter(gfx);
        //        tf.DrawString(overlayText, new XFont("Verdana", 10, XFontStyle.Regular), XBrushes.Black, rect, XStringFormats.TopLeft);
        //    }
        //    doc.Save(pathToPDF);
        //    doc.Close();
        //    Process.Start(pathToPDF);
        //}


        //class Toplist : IComparable
        //{
        //    public Toplist(FlightSet ct,
        //    int sum)
        //    {

        //        this.ct = ct;
        //        this.sum = sum;
        //    }
        //    public FlightSet ct = null;
        //    public int sum = 0;

        //    public int CompareTo(object obj)
        //    {
        //        return sum.CompareTo((obj as Toplist).sum);
        //    }
        //}

        ///// <summary>
        ///// split lengthy lines into multiple lines
        ///// </summary>
        ///// <param name="s"></param>
        ///// <returns></returns>
        //private static List<String> getWrapped(String s)
        //{
        //    List<String> result = new List<String>();
        //    string[] splitted = s.Split(new char[] { ' ', ',' });
        //    int currentLength = 0;
        //    StringBuilder sb = new StringBuilder();
        //    for (int i = 0; i < splitted.Length; i++)
        //    {
        //        if (currentLength + splitted[i].Length < 45)
        //        {
        //            currentLength += splitted.Length;
        //            sb.Append(splitted[i]);
        //            sb.Append(" ");
        //        }
        //        else
        //        {
        //            currentLength = 0;
        //            result.Add(sb.ToString());
        //            sb = new StringBuilder();
        //            i--;
        //        }
        //    }
        //    result.Add(sb.ToString());
        //    return result;
        //}
    }

    public static class StringExtensions
    {

        /// <summary>Use this function like string.Split but instead of a character to split on, 
        /// use a maximum line width size. This is similar to a Word Wrap where no words will be split.</summary>
        /// Note if the a word is longer than the maxcharactes it will be trimmed from the start.
        /// <param name="initial">The string to parse.</param>
        /// <param name="MaxCharacters">The maximum size.</param>
        /// <remarks>This function will remove some white space at the end of a line, but allow for a blank line.</remarks>
        /// 
        /// <returns>An array of strings.</returns>
        public static List<string> SplitOn(this string initial, int MaxCharacters)
        {
            // example: The rain in spain falls mainly on the plain of Jabberwocky falls.

            // List<string> lines = text.SplitOn( 20 );
            /*
            The rain in spain
            falls mainly on the
            plain of Jabberwocky
            falls.
             */

            List<string> lines = new List<string>();

            if (string.IsNullOrEmpty(initial) == false)
            {
                string targetGroup = "Line";
                string pattern = string.Format(@"(?<{0}>.{{1,{1}}})(?:[^\S]+|$)", targetGroup, MaxCharacters);

                lines = Regex.Matches(initial, pattern, RegexOptions.Multiline | RegexOptions.CultureInvariant)
                             .OfType<Match>()
                             .Select(mt => mt.Groups[targetGroup].Value)
                             .ToList();
            }
            return lines;
        }
    }
}
