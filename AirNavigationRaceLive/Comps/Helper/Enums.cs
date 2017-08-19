using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirNavigationRaceLive.Comps.Helper
{
    public enum LineType : int
    {
        START = 1,
        END = 2,
        START_A = 3,
        START_B = 4,
        START_C = 5,
        START_D = 6,
        END_A = 7,
        END_B = 8,
        END_C = 9,
        END_D = 10,
        LINEOFNORETURN = 11,
        PENALTYZONE = 12,
        Point = 13,
        TakeOff = 14,
    }
    public enum Route : int
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4
    }
}
