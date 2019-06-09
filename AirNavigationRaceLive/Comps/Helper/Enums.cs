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
        NOBACKTRACKLINE = 11,
        PENALTYZONE = 12,
        Point = 13,
        TKOF = 14,
        CHANNEL_A = 15,
        CHANNEL_B = 16,
        CHANNEL_C = 17,
        CHANNEL_D = 18,
        PROH_A = 19,
        PROH_B = 20,
        PROH_C = 21,
        PROH_D = 22,
        CENTERLINE_A = 23,
        CENTERLINE_B = 24,
        CENTERLINE_C = 25,
        CENTERLINE_D = 26
    }
    public enum Route : int
    {
        A = 1,
        B = 2,
        C = 3,
        D = 4
    }

    public enum PDFSize: int
    {
        // note: must match with definitions of PDFSharp.PageSize enumerations
        A3 = 4,
        A4 = 5
    }

    public enum ParcourType : int
    {
        // note: must match with definitions of PDFSharp.PageSize enumerations
        PROHZONES = 0,
        CHANNELS = 1
    }
}
