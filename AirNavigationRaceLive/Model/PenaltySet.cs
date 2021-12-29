namespace AirNavigationRaceLive.Model
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PenaltySet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class PenaltySet
    {
        public int Id { get; set; }

        public int Points { get; set; }

        [Required]
        [StringLength(500)]
        public string Reason { get; set; }

        public int Flight_Id { get; set; }

        public virtual FlightSet FlightSet { get; set; }
    }
}
