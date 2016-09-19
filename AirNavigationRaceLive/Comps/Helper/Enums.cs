using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetworkObjects
{
    public enum ERequestType : int
    {
        Login = 0,
        Register = 1,
        Get = 2,
        Save = 3,
        Delete = 4,
        GetAll = 5,
        Upload = 6
    }

    public enum EObjectType : int
    {
        Login = 0,
        Map = 1,
        Picture = 2,
        Parcour = 3,
        Tracker = 4,
        Pilot = 5,
        Team = 6,
        Competition = 8,
        GPSData = 9,
        Penalty = 10,
        CompetitionTeam = 11,
        CompetitionSet = 12
    }
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
    public enum Access : int
    {
        None = 0,
        Read = 1,
        Write = 2,
        Owner = 3,
        Admin = 4
    }
}
