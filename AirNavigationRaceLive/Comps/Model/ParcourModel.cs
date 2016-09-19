using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetworkObjects;
using AirNavigationRaceLive.Comps.Helper;

namespace AirNavigationRaceLive.Comps.Model
{
    public class ParcourModel : Parcour
    {
        public ParcourModel()
        {
        }
        public ParcourModel(ParcourModel p)
        {
            this.Id = p.Id;
            this.Map = p.Map;
            foreach (Line l in p.Line)
            {
                this.Line.Add(Factory.Line(l));
            }
            this.Name = p.Name;
        }
        public volatile bool finished;
        public double best;
        private readonly List<Line> Modified = new List<Line>();
        public void addModifiedLine(Line l)
        {
            if (!Modified.Contains(l))
            {
                Modified.Add(l);
            }
        }
        public List<Line> getModifiedLines()
        {
            return Modified.ToList();
        }
    }
}
