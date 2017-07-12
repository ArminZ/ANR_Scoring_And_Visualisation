namespace AirNavigationRaceLive.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FlightSet")]
    public partial class FlightSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PenaltySet> PenaltySet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Point> Point { get; set; }
    }
}
