namespace AirNavigationRaceLive.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Drawing;

    [Table("ParcourSet")]
    public partial class ParcourSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ParcourSet()
        {
            this.Alpha = 40;
            this.Line = new HashSet<Line>();
            //Line = new HashSet<Line>();
            this.QualificationRoundSet = new HashSet<QualificationRoundSet>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

//        private const int DEFAULT_ALPHA = 40;
 //       [DefaultValue(DEFAULT_ALPHA)]
        public int Alpha { get; set; }

        public int Map_Id { get; set; }

        public int Competition_Id { get; set; }

        public decimal PenWidthPROH { get; set; }
        public decimal PenWidthGates { get; set; }
        public bool HasCircleOnGates { get; set; }

        public Int32 ColorPROHArgb { get { return ColorPROH.ToArgb(); } set { ColorPROH = Color.FromArgb(value); } }
        public Int32 ColorGatesArgb { get { return ColorGates.ToArgb(); } set { ColorGates = Color.FromArgb(value); } }

        [NotMapped]
        public Color ColorPROH { get; set; }
        [NotMapped]
        public Color ColorGates { get; set; }

        public virtual CompetitionSet CompetitionSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Line> Line { get; set; }

        public virtual MapSet MapSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QualificationRoundSet> QualificationRoundSet { get; set; }
    }
}
