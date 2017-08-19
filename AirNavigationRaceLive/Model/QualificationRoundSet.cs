namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("QualificationRoundSet")]
    public partial class QualificationRoundSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public QualificationRoundSet()
        {
            FlightSet = new HashSet<FlightSet>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int Competition_Id { get; set; }

        public int TakeOffLine_Id { get; set; }

        public int Parcour_Id { get; set; }

        public virtual CompetitionSet CompetitionSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FlightSet> FlightSet { get; set; }

        public virtual Line TakeOffLine { get; set; }

        public virtual ParcourSet ParcourSet { get; set; }
    }
}
