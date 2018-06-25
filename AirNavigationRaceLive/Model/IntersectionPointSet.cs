namespace AirNavigationRaceLive.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("IntersectionPointSet")]
    public partial class IntersectionPoint
    {
        public int Id { get; set; }

        public double altitude { get; set; }

        public double longitude { get; set; }

        public double latitude { get; set; }

        public long Timestamp { get; set; }

        public int? Flight_Id { get; set; }

        public virtual FlightSet FlightSet { get; set; }
    }
}