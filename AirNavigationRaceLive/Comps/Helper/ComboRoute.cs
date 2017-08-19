using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive.Comps.Helper
{
  class ComboRoute
    {
        public Route p;
        public ComboRoute(Route p)
        {
            this.p = p;
        }

        public override string ToString()
        {
            return p.ToString();
        }
    }
}
