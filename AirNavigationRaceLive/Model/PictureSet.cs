namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("PictureSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class PictureSet
    {
        public PictureSet()
        {
            MapSet = new HashSet<MapSet>();
            SubscriberSet = new HashSet<SubscriberSet>();
        }

        public int Id { get; set; }

        [Required]
        public byte[] Data { get; set; }

        public virtual ICollection<MapSet> MapSet { get; set; }

        public virtual ICollection<SubscriberSet> SubscriberSet { get; set; }
    }
}
