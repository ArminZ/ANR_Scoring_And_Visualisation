namespace AirNavigationRaceLive.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LineSet")]
    public partial class Line
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Line()
        {
            QualificationRoundSet = new HashSet<QualificationRoundSet>();
        }

        public int Id { get; set; }

        public int Type { get; set; }

        public int A_Id { get; set; }

        public int B_Id { get; set; }

        public int O_Id { get; set; }

        public int? ParcourLine_Line_Id { get; set; }

        public virtual ParcourSet ParcourSet { get; set; }

        public virtual Point A { get; set; }

        public virtual Point B { get; set; }

        public virtual Point O { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualificationRoundSet> QualificationRoundSet { get; set; }
    }
}
