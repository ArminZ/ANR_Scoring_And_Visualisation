namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("FlightSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class FlightSet
    {
        public FlightSet()
        {
            PenaltySet = new HashSet<PenaltySet>();
            Point = new HashSet<Point>();
        }

        public int Id { get; set; }

        public int Route { get; set; }

        public long TimeTakeOff { get; set; }

        public long TimeStartLine { get; set; }

        public long TimeEndLine { get; set; }

        public int StartID { get; set; }

        public int QualificationRound_Id { get; set; }

        public int Team_Id { get; set; }

        public virtual QualificationRoundSet QualificationRoundSet { get; set; }

        public virtual TeamSet TeamSet { get; set; }

        public virtual ICollection<PenaltySet> PenaltySet { get; set; }

        public virtual ICollection<Point> Point { get; set; }

        public virtual ICollection<IntersectionPoint> IntersectionPointSet { get; set; }
    }
}
