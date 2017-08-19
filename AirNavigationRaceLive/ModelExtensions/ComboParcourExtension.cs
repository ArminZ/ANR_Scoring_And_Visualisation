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
    class ComboParcourExtension
    {
        public ParcourSet p;
        public ComboParcourExtension(ParcourSet p)
        {
            this.p = p;
        }

        public override string ToString()
        {
            return p.Name;
        }
    }
}
