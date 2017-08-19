using AirNavigationRaceLive.Comps.Helper;
using AirNavigationRaceLive.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.ModelExtensions
{
    [NotMapped]
    public class ParcourExtension : ParcourSet
    {
        public ParcourExtension()
        {
        }
        public ParcourExtension(ParcourExtension p)
        {
            this.Id = p.Id;
            this.MapSet = p.MapSet;
            foreach (Line l in p.Line)
            {
                this.Line.Add(Factory.Line(l));
            }
            this.Name = p.Name;
        }
        //public volatile bool finished;
        public double best;
        private readonly List<Line> Modified = new List<Line>();
        public void addModifiedLine(Line l)
        {
            if (!Modified.Contains(l))
            {
                Modified.Add(l);
            }
        }
        //public List<Line> getModifiedLines()
        //{
        //    return Modified.ToList();
        //}
    }
}
