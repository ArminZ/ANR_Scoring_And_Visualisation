namespace AirNavigationRaceLive.Model
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("SubscriberSet")]
    public partial class SubscriberSet
    {
        public SubscriberSet()
        {
            TeamSet = new HashSet<TeamSet>();
        }

        public int Id { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string FirstName { get; set; }

        public int Competition_Id { get; set; }

        public int? Picture_Id { get; set; }

        public string External_Id { get; set; }

        public virtual CompetitionSet CompetitionSet { get; set; }

        public virtual PictureSet PictureSet { get; set; }

        public virtual ICollection<TeamSet> TeamSet { get; set; }

    }
}
