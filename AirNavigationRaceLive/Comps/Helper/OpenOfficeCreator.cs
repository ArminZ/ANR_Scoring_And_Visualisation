using AirNavigationRaceLive.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Helper
{
    class OpenOfficeCreator
    {
        public static void CreateRankingListExcel(string CompName, string QRName, List<ComboBoxFlights> qRndFlights, String filename)
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

            var newFile = new FileInfo(filename);
            if (newFile.Exists)
            {
                newFile.Delete();
            }
            using (var pck = new ExcelPackage(newFile))
            {
                ExcelWorksheet ResultList = pck.Workbook.Worksheets.Add("ResultList");
                ResultList.Cells[1, 1].Value = String.Format("Competition: {0}", CompName);
                ResultList.Cells[2, 1].Value = String.Format("Qualification Round: {0}", QRName);

                string[] colNamesValues = { "Rank", "Points", "Nationality", "Pilot Lastname", "Pilot Firstname", "Navigator Lastname", "Navigator Firstname" };

                for (int jCol = 0; jCol < colNamesValues.Length; jCol++)
                {
                    ResultList.Cells[3, jCol + 1].Value = colNamesValues[jCol];
                }

                int oldsum = -1;
                int prevRank = 0;
                int rank = 0;
                int i = 0;
                int iBase = 3;

                foreach (Toplist top in toplist)
                {
                    rank++;
                    i++;
                    TeamSet t = top.ct.TeamSet;
                    if (i > 0 && oldsum == top.sum)  // we have a shared rank
                    {
                        ResultList.Cells[i + iBase, 1].Value = prevRank;
                    }
                    else  // the normal case
                    {
                        prevRank = rank;
                        ResultList.Cells[i + iBase, 1].Value = rank;
                    }
                    ResultList.Cells[i + iBase, 2].Value = top.sum.ToString();
                    ResultList.Cells[i + iBase, 3].Value = t.Nationality;
                    SubscriberSet pilot = t.Pilot;
                    ResultList.Cells[i + iBase, 4].Value = pilot.LastName;
                    ResultList.Cells[i + iBase, 5].Value = pilot.FirstName;
                    if (t.Navigator != null)
                    {
                        SubscriberSet navigator = t.Navigator;
                        ResultList.Cells[i + iBase, 6].Value = navigator.LastName;
                        ResultList.Cells[i + iBase, 7].Value = navigator.FirstName;
                    }
                    oldsum = top.sum;
                }
                pck.Save();
            }
        }

    }
}
