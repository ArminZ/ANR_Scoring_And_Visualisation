namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("TeamSet")]
    public partial class TeamSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public TeamSet()
        {
            FlightSet = new HashSet<FlightSet>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string CNumber { get; set; }

        [StringLength(10)]
        public string Color { get; set; }

        [Required(AllowEmptyStrings=true)]
        public string Nationality { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string AC { get; set; }

        public int Pilot_Id { get; set; }

        public int? Navigator_Id { get; set; }

        public int Competition_Id { get; set; }

        public virtual CompetitionSet CompetitionSet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]


        public virtual ICollection<FlightSet> FlightSet { get; set; }


        [ForeignKey("Pilot_Id")]
        public virtual SubscriberSet Pilot { get; set; }


        [ForeignKey("Navigator_Id")]
        public virtual SubscriberSet Navigator { get; set; }
    }
}
