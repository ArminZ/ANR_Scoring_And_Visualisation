namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("LineSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Line
    {
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

        public virtual ICollection<QualificationRoundSet> QualificationRoundSet { get; set; }
    }
}
