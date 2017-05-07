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
using NetworkObjects;
using PdfSharp.Drawing.Layout;

namespace AirNavigationRaceLive.Comps.Helper
{
    public class PDFCreator
    {
        public static void CreateTeamsPDF(List<Team> teams, Client.DataAccess c, String pathToPDF)
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

            foreach (Team t in teams)
            {
                Row r = table.AddRow();
                //r.Cells[0].AddParagraph(t.ID.ToString());
                r.Cells[0].AddParagraph(t.CNumber);
                r.Cells[1].AddParagraph(t.Nationality);
                Subscriber pilot = t.Pilot;
                r.Cells[2].AddParagraph(pilot.LastName);
                r.Cells[3].AddParagraph(pilot.FirstName);
                if (t.Navigator != null)
                {
                    Subscriber navigator = t.Navigator;
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


            Image logo = pg.AddImage(@"Resources\ANR_LOGO.jpg");
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

        public static void CreateParcourPDF(double scaleFactor, ParcourPictureBox picBox, Client.DataAccess c, String parcourName, String pathToPDF, String overlayText)
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
            page.Size = PdfSharp.PageSize.A4;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            AddLogo(gfx, page);

            gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
                new XFont("Verdana", 16, XFontStyle.Bold), XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(2)));

            gfx.DrawString("Parcour: " + parcourName,
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(3)));

            XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage);

            double distX = picBox.GetXDistanceKM() / scaleFactor;//1:200 000 in cm
            double distY = picBox.GetYDistanceKM() / scaleFactor;//1:200 000 in cm

            gfx.DrawImage(image, XUnit.FromCentimeter(1), XUnit.FromCentimeter(4), page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

            double startX = 190;

            List<XPoint> points = new List<XPoint>();
            points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40)));
            points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * 5), Unit.FromMillimeter(40)));
            points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * 5), Unit.FromMillimeter(40 + 9)));
            points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40 + 9)));
            points.Add(new XPoint(Unit.FromMillimeter(startX), Unit.FromMillimeter(40)));
            gfx.DrawLines(XPens.Black, points.ToArray());
            //for (int i = 0; i < 5; i++)
            //{
            //    if (i == 1) continue;
            //    points = new List<XPoint>();
            //    points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * i), Unit.FromMillimeter(40)));
            //    points.Add(new XPoint(Unit.FromMillimeter(startX + 18 * i), Unit.FromMillimeter(40 + 9)));
            //    gfx.DrawLines(XPens.Black, points.ToArray());
            //}

            gfx.DrawString("Comp. Nr:",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(40 + 7)));

            gfx.DrawString("Route:",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
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
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + colWidth * 1 + 1), Unit.FromMillimeter(startY + 7)));

            gfx.DrawString("TT",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + colWidth * 2 + 1), Unit.FromMillimeter(startY + 7)));

            gfx.DrawString("EET",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + colWidth * 3 + 1), Unit.FromMillimeter(startY + 7)));

            gfx.DrawString("ETO",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + colWidth * 4 + 1), Unit.FromMillimeter(startY + 7)));

            gfx.DrawString("T/O",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 1 + 7)));

            gfx.DrawString("SP",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 2 + 7)));

            for (int i = 3; i < 13; i++)
            {
                gfx.DrawString("TP" + (i - 3),
                    new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                    new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * i + 7)));
            }
            gfx.DrawString("FP",
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 13 + 7)));

            gfx.DrawImage(XImage.FromFile(@"Resources\Summe.png"),
                new XPoint(Unit.FromMillimeter(startX + 1), Unit.FromMillimeter(startY + rowHeight * 14 + 2)));

            if (!string.IsNullOrEmpty(overlayText.Trim()))
            {
                XRect rect = new XRect(XUnit.FromCentimeter(19.0), XUnit.FromCentimeter(1.5), Unit.FromCentimeter(5.0), Unit.FromCentimeter(2.0));
  //              XRect rect = new XRect(new XPoint(Unit.FromMillimeter(startX - 80 - 2), Unit.FromMillimeter(startY + rowHeight * 14 + 2 - 16)), new XSize(Unit.FromCentimeter(8), Unit.FromCentimeter(3.6)));
                gfx.DrawRectangle(XBrushes.White, rect);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.DrawString(overlayText, new XFont("Verdana", 10, XFontStyle.Regular), XBrushes.Black, rect, XStringFormats.TopLeft);
            }
            //else
            //{
            //    writeScale(gfx, page, scaleFactor);
            //}
            doc.Save(pathToPDF);
            doc.Close();
            Process.Start(pathToPDF);
        }
        //private static void writeScale(XGraphics gfx, PdfPage page, double scaleFactor)
        //{
        //    string strScFactor = "";
        //    if (scaleFactor == 2.0) { strScFactor = "1:200'000"; }
        //    if (scaleFactor == 2.5) { strScFactor = "1:250'000"; }
      
        //    XRect rect = new XRect(XUnit.FromCentimeter(1.0), XUnit.FromCentimeter(19.5), Unit.FromCentimeter(5.0), Unit.FromCentimeter(1.0));
        //    gfx.DrawRectangle(XBrushes.White, rect);
        //    XTextFormatter tf = new XTextFormatter(gfx);
        //    tf.DrawString(string.Format("Map scale {0}", strScFactor), new XFont("Verdana", 10, XFontStyle.Regular), XBrushes.Black, rect, XStringFormats.TopLeft);
        //}

        public static void CreateParcourPDF100k(ParcourPictureBox picBox, Client.DataAccess c, String parcourName, String pathToPDF, String overlayText)
        {
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
            page.Size = PdfSharp.PageSize.A3;

            XGraphics gfx = XGraphics.FromPdfPage(page);
            AddLogo(gfx, page);

            gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
                new XFont("Verdana", 16, XFontStyle.Bold), XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(2)));

            gfx.DrawString("Parcour: " + parcourName,
                new XFont("Verdana", 14, XFontStyle.Bold), XBrushes.Black,
                new XPoint(XUnit.FromCentimeter(1), XUnit.FromCentimeter(3)));

            XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage);

            double distX = picBox.GetXDistanceKM();//1:100 000 in cm
            double distY = picBox.GetYDistanceKM();//1:100 000 in cm

            gfx.DrawImage(image, XUnit.FromCentimeter(1), XUnit.FromCentimeter(4), page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

            if (!string.IsNullOrEmpty(overlayText.Trim()))
            {
                //       XRect rect = new XRect(new XPoint(Unit.FromCentimeter(page.Width.Centimeter - 8), Unit.FromCentimeter(page.Height.Centimeter-4)), new XSize(Unit.FromCentimeter(8), Unit.FromCentimeter(3.6)));
                XRect rect = new XRect(XUnit.FromCentimeter(19.0), XUnit.FromCentimeter(1.5), Unit.FromCentimeter(5.0), Unit.FromCentimeter(2.0));
                gfx.DrawRectangle(XBrushes.White, rect);
                XTextFormatter tf = new XTextFormatter(gfx);
                tf.DrawString(overlayText, new XFont("Verdana", 10, XFontStyle.Regular), XBrushes.Black, rect, XStringFormats.TopLeft);
            }
            doc.Save(pathToPDF);
            doc.Close();
            Process.Start(pathToPDF);
        }
        public static void CreateToplistResultPDF(Client.DataAccess c, QualificationRound qRnd, List<ComboBoxFlights> qRndFlights, String pathToPDF)
        {
            List<Toplist> toplist = new List<Toplist>();
            foreach (ComboBoxFlights cbct in qRndFlights)
            {
                int sum = 0;
                foreach (Penalty penalty in cbct.flight.Penalty)
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
            sec.AddParagraph("Toplist " + qRnd.Name);
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
                Team t = top.ct.Team;
                Row r = table.AddRow();
                if (rank>1 && oldsum == top.sum)  // we have a shared rank
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
                Subscriber pilot = t.Pilot;
                r.Cells[3].AddParagraph(pilot.LastName);
                r.Cells[4].AddParagraph(pilot.FirstName);
                if (t.Navigator != null)
                {
                    Subscriber navigator = t.Navigator;
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
        class Toplist : IComparable
        {
            public Toplist(Flight ct,
            int sum)
            {

                this.ct = ct;
                this.sum = sum;
            }
            public Flight ct = null;
            public int sum = 0;

            public int CompareTo(object obj)
            {
                return sum.CompareTo((obj as Toplist).sum);
            }
        }

        public static void CreateResultPDF(VisualisationPictureBox picBox, Client.DataAccess c, QualificationRound qRnd, List<ComboBoxFlights> qRndFlights, String pathToPDF)
        {
            int counter = 0;
            List<Flight> tempList = new List<Flight>();
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

                XGraphics gfx = XGraphics.FromPdfPage(page);
                AddLogo(gfx, page);

                XImage image = XImage.FromGdiPlusImage(picBox.PrintOutImage);

                double distX = picBox.GetXDistanceKM() / 2;//1:200 000 in cm
                double distY = picBox.GetYDistanceKM() / 2;//1:200 000 in cm

                gfx.DrawImage(image, XUnit.FromCentimeter(2).Point, XUnit.FromCentimeter(3).Point, page.Width.Point * (distX / page.Width.Centimeter), page.Height.Point * (distY / page.Height.Centimeter));

                gfx.DrawString("Competition: " + c.SelectedCompetition.Name,
                    new XFont("Verdana", 13, XFontStyle.Bold), XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(1.5)));

                gfx.DrawString("Q-Round: " + qRnd.Name,
                    new XFont("Verdana", 11, XFontStyle.Bold), XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.1)));

                gfx.DrawString("Crew: " + getTeamDsc(c, cbct.flight),
                    new XFont("Verdana", 11, XFontStyle.Bold), XBrushes.Black,
                    new XPoint(XUnit.FromCentimeter(2), XUnit.FromCentimeter(2.7)));

                int sum = 0;
                int line = 0;
                int offsetLine = 20;
                //gfx.DrawString("ID", new XFont("Verdana", 11, XFontStyle.Bold), XBrushes.Black, new XPoint(XUnit.FromCentimeter(18), XUnit.FromCentimeter(3)));
                gfx.DrawString("Points ", new XFont("Verdana", 11, XFontStyle.Bold), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3)));
                gfx.DrawString("Reason ", new XFont("Verdana", 11, XFontStyle.Bold), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3)));

                foreach (Penalty penalty in cbct.flight.Penalty)
                {
                    sum += penalty.Points;
                    line++;
                    //gfx.DrawString(penalty.ID.ToString(), new XFont("Verdana", 9, XFontStyle.Regular), XBrushes.Black, new XPoint(XUnit.FromCentimeter(18), XUnit.FromCentimeter(3 + line * 0.4)));
                    gfx.DrawString(penalty.Points.ToString(), new XFont("Verdana", 9, XFontStyle.Regular), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));

                    List<String> reason = getWrapped(penalty.Reason);
                    bool loop = false;
                    foreach (String s in reason)
                    {
                        gfx.DrawString(s, new XFont("Verdana", 9, XFontStyle.Regular), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));
                        line++;
                        loop = true;
                    }
                    if (loop)
                    {
                        line--;
                    }
                }
                line++;
                //gfx.DrawString("-", new XFont("Verdana", 9, XFontStyle.Regular), XBrushes.Black, new XPoint(XUnit.FromCentimeter(18), XUnit.FromCentimeter(3 + line * 0.4)));
                gfx.DrawString(sum.ToString(), new XFont("Verdana", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine), XUnit.FromCentimeter(3 + line * 0.4)));
                gfx.DrawString("Total Points", new XFont("Verdana", 10, XFontStyle.Bold), XBrushes.Black, new XPoint(XUnit.FromCentimeter(offsetLine + 2), XUnit.FromCentimeter(3 + line * 0.4)));


                String path = pathToPDF.Replace(".pdf", (counter++ + "_" + getTeamDsc(c, cbct.flight) + ".pdf"));
                doc.Save(path);
                doc.Close();
                Process.Start(path);
            }
        }

        private static List<String> getWrapped(String s)
        {
            List<String> result = new List<String>();
            string[] splitted = s.Split(new char[] { ' ' });
            int currentLenght = 0;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < splitted.Length; i++)
            {
                if (currentLenght + splitted[i].Length < 45)
                {
                    currentLenght += splitted.Length;
                    sb.Append(splitted[i]);
                    sb.Append(" ");
                }
                else
                {
                    currentLenght = 0;
                    result.Add(sb.ToString());
                    sb = new StringBuilder();
                    i--;
                }
            }
            result.Add(sb.ToString());
            return result;
        }

        private static string getTeamDsc(Client.DataAccess c, Flight flight)
        {
            Team team = flight.Team;
            Subscriber pilot = team.Pilot;
            StringBuilder sb = new StringBuilder();
            sb.Append(team.CNumber).Append(" ");
            sb.Append(pilot.LastName).Append(" ").Append(pilot.FirstName);
            if (team.Navigator != null)
            {
                Subscriber navi = team.Navigator;
                sb.Append(" - ").Append(navi.LastName).Append(" ").Append(navi.FirstName);
            }
            return sb.ToString();
        }

        internal static void CreateStartListPDF(QualificationRound qRnd, Client.DataAccess Client, string pathToPDF)
        {
            Document doc = new Document();
            doc.Info.Author = "Luc.Baumann@sharpsoft.ch";
            doc.Info.Comment = "Generated from ANRL Client on " + DateTime.Now.ToString();
            doc.Info.Keywords = "ANRL StartList";
            doc.Info.Subject = "StartList";
            doc.Info.Title = "StartList";
            doc.UseCmykColor = true;
            doc.DefaultPageSetup.PageFormat = PageFormat.A4;
            doc.DefaultPageSetup.Orientation = Orientation.Landscape;

            Section sec = doc.AddSection();

            AddCompetitionAndLogo(Client, sec);

            sec.AddParagraph("Qualification Round: " + qRnd.Name);
            sec.AddParagraph("Startlist");
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

            foreach (Flight ct in qRnd.Flight.OrderBy(x => x.TimeTakeOff).ThenBy(x=>x.Route))
            {
                Row r = table.AddRow();
                r.Cells[0].AddParagraph(ct.StartID.ToString());
                Team teams = ct.Team;
                r.Cells[1].AddParagraph(teams.CNumber);
                r.Cells[2].AddParagraph(teams.AC);
                Subscriber pilot = teams.Pilot;
                r.Cells[3].AddParagraph(pilot.LastName);
                r.Cells[4].AddParagraph(pilot.FirstName);
                if (teams.Navigator != null)
                {
                    Subscriber navigator = teams.Navigator;
                    r.Cells[5].AddParagraph(navigator.LastName);
                    r.Cells[6].AddParagraph(navigator.FirstName);
                }
                r.Cells[7].AddParagraph(new DateTime(ct.TimeTakeOff).ToString("HH:mm:ss"));
                r.Cells[8].AddParagraph(new DateTime(ct.TimeStartLine).ToString("HH:mm:ss"));
                r.Cells[9].AddParagraph(new DateTime(ct.TimeEndLine).ToString("HH:mm:ss"));
                r.Cells[10].AddParagraph(Enum.GetName(NetworkObjects.Route.A.GetType(), ct.Route));
            }

            PdfDocumentRenderer renderer = new PdfDocumentRenderer(true, PdfSharp.Pdf.PdfFontEmbedding.Always);
            renderer.Document = doc;
            renderer.RenderDocument();
            renderer.PdfDocument.Save(pathToPDF);
            Process.Start(pathToPDF);
        }
    }
}
