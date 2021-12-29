namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MapSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class MapSet
    {
        public MapSet()
        {
            ParcourSet = new HashSet<ParcourSet>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double XSize { get; set; }

        public double YSize { get; set; }

        public double XRot { get; set; }

        public double YRot { get; set; }

        public double XTopLeft { get; set; }

        public double YTopLeft { get; set; }

        public int Picture_Id { get; set; }

        public int Competition_Id { get; set; }

        public virtual CompetitionSet CompetitionSet { get; set; }

        public virtual PictureSet PictureSet { get; set; }

        public virtual ICollection<ParcourSet> ParcourSet { get; set; }
    }
}
