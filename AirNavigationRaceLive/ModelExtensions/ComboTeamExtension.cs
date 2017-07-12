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
    class ComboTeamExtension
    {
        public TeamSet p;
        private String toString;
        public ComboTeamExtension(TeamSet p, String toString)
        {
            this.p = p;
            this.toString = toString;
        }

        public override string ToString()
        {
            return toString;
        }
    }
}
