using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirNavigationRaceLive.Comps.Helper
{
    class Status
    {
        public static void SetStatus(String message)
        {
            AirNavigationRaceLiveMain.SetStatusText(message);
        }
    }
}
