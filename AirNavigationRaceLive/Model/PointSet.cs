namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PointSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Point
    {
        public Point()
        {
            A = new HashSet<Line>();
            B = new HashSet<Line>();
            O = new HashSet<Line>();
        }

        public int Id { get; set; }

        public double altitude { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public long Timestamp { get; set; }

        public int? Flight_Id { get; set; }

        public virtual FlightSet FlightSet { get; set; }

        public virtual ICollection<Line> A { get; set; }

        public virtual ICollection<Line> B { get; set; }

        public virtual ICollection<Line> O { get; set; }
    }
}
