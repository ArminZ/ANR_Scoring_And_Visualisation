namespace AirNavigationRaceLive.Model
{
    using Client;
    //using Migrations;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Migrations;

    public partial class AnrlModel : DbContext
    {
        public AnrlModel() : base("name=AnrlModel2Container")
        {
        }

        public virtual DbSet<CompetitionSet> CompetitionSet { get; set; }
        public virtual DbSet<FlightSet> FlightSet { get; set; }
        public virtual DbSet<Line> Line { get; set; }
        public virtual DbSet<MapSet> MapSet { get; set; }
        public virtual DbSet<ParcourSet> ParcourSet { get; set; }
        public virtual DbSet<PenaltySet> PenaltySet { get; set; }
        public virtual DbSet<PictureSet> PictureSet { get; set; }
        public virtual DbSet<Point> Point { get; set; }
        public virtual DbSet<QualificationRoundSet> QualificationRoundSet { get; set; }
        public virtual DbSet<SubscriberSet> SubscriberSet { get; set; }
        public virtual DbSet<TeamSet> TeamSet { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompetitionSet>()
                .HasMany(e => e.MapSet)
                .WithRequired(e => e.CompetitionSet)
                .HasForeignKey(e => e.Competition_Id);

            modelBuilder.Entity<CompetitionSet>()
                .HasMany(e => e.ParcourSet)
                .WithRequired(e => e.CompetitionSet)
                .HasForeignKey(e => e.Competition_Id);

            modelBuilder.Entity<CompetitionSet>()
                .HasMany(e => e.QualificationRoundSet)
                .WithRequired(e => e.CompetitionSet)
                .HasForeignKey(e => e.Competition_Id);

            modelBuilder.Entity<CompetitionSet>()
                .HasMany(e => e.SubscriberSet)
                .WithRequired(e => e.CompetitionSet)
                .HasForeignKey(e => e.Competition_Id);

            modelBuilder.Entity<CompetitionSet>()
                .HasMany(e => e.TeamSet)
                .WithRequired(e => e.CompetitionSet)
                .HasForeignKey(e => e.Competition_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<FlightSet>()
                .HasMany(e => e.PenaltySet)
                .WithRequired(e => e.FlightSet)
                .HasForeignKey(e => e.Flight_Id);

            modelBuilder.Entity<FlightSet>()
                .HasMany(e => e.Point)
                .WithOptional(e => e.FlightSet)
                .HasForeignKey(e => e.Flight_Id)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Line>()
                .HasMany(e => e.QualificationRoundSet)
                .WithRequired(e => e.TakeOffLine)
                .HasForeignKey(e => e.TakeOffLine_Id);

            modelBuilder.Entity<MapSet>()
                .HasMany(e => e.ParcourSet)
                .WithRequired(e => e.MapSet)
                .HasForeignKey(e => e.Map_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ParcourSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ParcourSet>()
                .HasMany(e => e.Line)
                .WithOptional(e => e.ParcourSet)
                .HasForeignKey(e => e.ParcourLine_Line_Id);

            modelBuilder.Entity<ParcourSet>()
                .HasMany(e => e.QualificationRoundSet)
                .WithRequired(e => e.ParcourSet)
                .HasForeignKey(e => e.Parcour_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PenaltySet>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<PictureSet>()
                .HasMany(e => e.MapSet)
                .WithRequired(e => e.PictureSet)
                .HasForeignKey(e => e.Picture_Id);

            modelBuilder.Entity<PictureSet>()
                .HasMany(e => e.SubscriberSet)
                .WithOptional(e => e.PictureSet)
                .HasForeignKey(e => e.Picture_Id);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.A)
                .WithRequired(e => e.A)
                .HasForeignKey(e => e.A_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.B)
                .WithRequired(e => e.B)
                .HasForeignKey(e => e.B_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Point>()
                .HasMany(e => e.O)
                .WithRequired(e => e.O)
                .HasForeignKey(e => e.O_Id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<QualificationRoundSet>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<QualificationRoundSet>()
                .HasMany(e => e.FlightSet)
                .WithRequired(e => e.QualificationRoundSet)
                .HasForeignKey(e => e.QualificationRound_Id);

            modelBuilder.Entity<SubscriberSet>()
                .HasMany(e => e.TeamSet)
                .WithOptional(e => e.Navigator)
                .HasForeignKey(e => e.Navigator_Id);

            modelBuilder.Entity<SubscriberSet>()
                .HasMany(e => e.TeamSet)
                .WithRequired(e => e.Pilot)
                .HasForeignKey(e => e.Pilot_Id);

            modelBuilder.Entity<TeamSet>()
                .Property(e => e.CNumber)
                .IsUnicode(false);

            modelBuilder.Entity<TeamSet>()
                .Property(e => e.Color)
                .IsUnicode(false);

            modelBuilder.Entity<TeamSet>()
                .HasMany(e => e.FlightSet)
                .WithRequired(e => e.TeamSet)
                .HasForeignKey(e => e.Team_Id)
                .WillCascadeOnDelete(false);
        }
    }
}
