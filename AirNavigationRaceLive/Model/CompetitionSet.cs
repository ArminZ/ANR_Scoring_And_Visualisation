namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("CompetitionSet")]
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class CompetitionSet
    {
        public CompetitionSet()
        {
            MapSet = new HashSet<MapSet>();
            ParcourSet = new HashSet<ParcourSet>();
            QualificationRoundSet = new HashSet<QualificationRoundSet>();
            SubscriberSet = new HashSet<SubscriberSet>();
            TeamSet = new HashSet<TeamSet>();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual ICollection<MapSet> MapSet { get; set; }

        public virtual ICollection<ParcourSet> ParcourSet { get; set; }

        public virtual ICollection<QualificationRoundSet> QualificationRoundSet { get; set; }

        public virtual ICollection<SubscriberSet> SubscriberSet { get; set; }

        public virtual ICollection<TeamSet> TeamSet { get; set; }
    }
}
