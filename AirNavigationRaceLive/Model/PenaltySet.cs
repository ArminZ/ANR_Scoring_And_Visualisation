namespace AirNavigationRaceLive.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PenaltySet")]
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
