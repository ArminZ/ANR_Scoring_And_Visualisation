namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompetitionSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.MapSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false),
                    XSize = c.Double(nullable: false),
                    YSize = c.Double(nullable: false),
                    XRot = c.Double(nullable: false),
                    YRot = c.Double(nullable: false),
                    XTopLeft = c.Double(nullable: false),
                    YTopLeft = c.Double(nullable: false),
                    Picture_Id = c.Int(nullable: false),
                    Competition_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PictureSet", t => t.Picture_Id, cascadeDelete: true)
                .ForeignKey("dbo.CompetitionSet", t => t.Competition_Id, cascadeDelete: true)
                .Index(t => t.Picture_Id)
                .Index(t => t.Competition_Id);

            CreateTable(
                "dbo.ParcourSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    Alpha = c.Int(nullable: false),
                    Map_Id = c.Int(nullable: false),
                    Competition_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MapSet", t => t.Map_Id)
                .ForeignKey("dbo.CompetitionSet", t => t.Competition_Id, cascadeDelete: true)
                .Index(t => t.Map_Id)
                .Index(t => t.Competition_Id);

            CreateTable(
                "dbo.LineSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Type = c.Int(nullable: false),
                    A_Id = c.Int(nullable: false),
                    B_Id = c.Int(nullable: false),
                    O_Id = c.Int(nullable: false),
                    ParcourLine_Line_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PointSet", t => t.A_Id)
                .ForeignKey("dbo.PointSet", t => t.B_Id)
                .ForeignKey("dbo.PointSet", t => t.O_Id)
                .ForeignKey("dbo.ParcourSet", t => t.ParcourLine_Line_Id)
                .Index(t => t.A_Id)
                .Index(t => t.B_Id)
                .Index(t => t.O_Id)
                .Index(t => t.ParcourLine_Line_Id);

            CreateTable(
                "dbo.PointSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    altitude = c.Double(nullable: false),
                    longitude = c.Double(nullable: false),
                    latitude = c.Double(nullable: false),
                    Timestamp = c.Long(nullable: false),
                    Flight_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FlightSet", t => t.Flight_Id, cascadeDelete: true)
                .Index(t => t.Flight_Id);

            CreateTable(
                "dbo.FlightSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Route = c.Int(nullable: false),
                    TimeTakeOff = c.Long(nullable: false),
                    TimeStartLine = c.Long(nullable: false),
                    TimeEndLine = c.Long(nullable: false),
                    StartID = c.Int(nullable: false),
                    QualificationRound_Id = c.Int(nullable: false),
                    Team_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QualificationRoundSet", t => t.QualificationRound_Id, cascadeDelete: true)
                .ForeignKey("dbo.TeamSet", t => t.Team_Id)
                .Index(t => t.QualificationRound_Id)
                .Index(t => t.Team_Id);

            CreateTable(
                "dbo.PenaltySet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Points = c.Int(nullable: false),
                    Reason = c.String(nullable: false, maxLength: 500, unicode: false),
                    Flight_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FlightSet", t => t.Flight_Id, cascadeDelete: true)
                .Index(t => t.Flight_Id);

            CreateTable(
                "dbo.QualificationRoundSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Name = c.String(nullable: false, maxLength: 50, unicode: false),
                    Competition_Id = c.Int(nullable: false),
                    TakeOffLine_Id = c.Int(nullable: false),
                    Parcour_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LineSet", t => t.TakeOffLine_Id, cascadeDelete: true)
                .ForeignKey("dbo.ParcourSet", t => t.Parcour_Id)
                .ForeignKey("dbo.CompetitionSet", t => t.Competition_Id, cascadeDelete: true)
                .Index(t => t.Competition_Id)
                .Index(t => t.TakeOffLine_Id)
                .Index(t => t.Parcour_Id);

            CreateTable(
                "dbo.TeamSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    CNumber = c.String(maxLength: 50, unicode: false),
                    Color = c.String(maxLength: 10, unicode: false),
                    Nationality = c.String(nullable: false),
                    AC = c.String(nullable: false),
                    Pilot_Id = c.Int(nullable: false),
                    Navigator_Id = c.Int(),
                    Competition_Id = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SubscriberSet", t => t.Pilot_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubscriberSet", t => t.Navigator_Id)
                .ForeignKey("dbo.CompetitionSet", t => t.Competition_Id)
                .Index(t => t.Pilot_Id)
                .Index(t => t.Navigator_Id)
                .Index(t => t.Competition_Id);

            CreateTable(
                "dbo.SubscriberSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    LastName = c.String(nullable: false),
                    FirstName = c.String(nullable: false),
                    Competition_Id = c.Int(nullable: false),
                    Picture_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PictureSet", t => t.Picture_Id)
                .ForeignKey("dbo.CompetitionSet", t => t.Competition_Id, cascadeDelete: true)
                .Index(t => t.Competition_Id)
                .Index(t => t.Picture_Id);

            CreateTable(
                "dbo.PictureSet",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Data = c.Binary(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            return;

            //DropForeignKey("dbo.TeamSet", "Competition_Id", "dbo.CompetitionSet");
            //DropForeignKey("dbo.SubscriberSet", "Competition_Id", "dbo.CompetitionSet");
            //DropForeignKey("dbo.QualificationRoundSet", "Competition_Id", "dbo.CompetitionSet");
            //DropForeignKey("dbo.ParcourSet", "Competition_Id", "dbo.CompetitionSet");
            //DropForeignKey("dbo.MapSet", "Competition_Id", "dbo.CompetitionSet");
            //DropForeignKey("dbo.ParcourSet", "Map_Id", "dbo.MapSet");
            //DropForeignKey("dbo.QualificationRoundSet", "Parcour_Id", "dbo.ParcourSet");
            //DropForeignKey("dbo.LineSet", "ParcourLine_Line_Id", "dbo.ParcourSet");
            //DropForeignKey("dbo.QualificationRoundSet", "TakeOffLine_Id", "dbo.LineSet");
            //DropForeignKey("dbo.LineSet", "O_Id", "dbo.PointSet");
            //DropForeignKey("dbo.TeamSet", "Navigator_Id", "dbo.SubscriberSet");
            //DropForeignKey("dbo.TeamSet", "Pilot_Id", "dbo.SubscriberSet");
            //DropForeignKey("dbo.SubscriberSet", "Picture_Id", "dbo.PictureSet");
            //DropForeignKey("dbo.MapSet", "Picture_Id", "dbo.PictureSet");
            //DropForeignKey("dbo.FlightSet", "Team_Id", "dbo.TeamSet");
            //DropForeignKey("dbo.FlightSet", "QualificationRound_Id", "dbo.QualificationRoundSet");
            //DropForeignKey("dbo.PointSet", "Flight_Id", "dbo.FlightSet");
            //DropForeignKey("dbo.PenaltySet", "Flight_Id", "dbo.FlightSet");
            //DropForeignKey("dbo.LineSet", "B_Id", "dbo.PointSet");
            //DropForeignKey("dbo.LineSet", "A_Id", "dbo.PointSet");
            //DropIndex("dbo.SubscriberSet", new[] { "Picture_Id" });
            //DropIndex("dbo.SubscriberSet", new[] { "Competition_Id" });
            //DropIndex("dbo.TeamSet", new[] { "Competition_Id" });
            //DropIndex("dbo.TeamSet", new[] { "Navigator_Id" });
            //DropIndex("dbo.TeamSet", new[] { "Pilot_Id" });
            //DropIndex("dbo.QualificationRoundSet", new[] { "Parcour_Id" });
            //DropIndex("dbo.QualificationRoundSet", new[] { "TakeOffLine_Id" });
            //DropIndex("dbo.QualificationRoundSet", new[] { "Competition_Id" });
            //DropIndex("dbo.PenaltySet", new[] { "Flight_Id" });
            //DropIndex("dbo.FlightSet", new[] { "Team_Id" });
            //DropIndex("dbo.FlightSet", new[] { "QualificationRound_Id" });
            //DropIndex("dbo.PointSet", new[] { "Flight_Id" });
            //DropIndex("dbo.LineSet", new[] { "ParcourLine_Line_Id" });
            //DropIndex("dbo.LineSet", new[] { "O_Id" });
            //DropIndex("dbo.LineSet", new[] { "B_Id" });
            //DropIndex("dbo.LineSet", new[] { "A_Id" });
            //DropIndex("dbo.ParcourSet", new[] { "Competition_Id" });
            //DropIndex("dbo.ParcourSet", new[] { "Map_Id" });
            //DropIndex("dbo.MapSet", new[] { "Competition_Id" });
            //DropIndex("dbo.MapSet", new[] { "Picture_Id" });
            //DropTable("dbo.PictureSet");
            //DropTable("dbo.SubscriberSet");
            //DropTable("dbo.TeamSet");
            //DropTable("dbo.QualificationRoundSet");
            //DropTable("dbo.PenaltySet");
            //DropTable("dbo.FlightSet");
            //DropTable("dbo.PointSet");
            //DropTable("dbo.LineSet");
            //DropTable("dbo.ParcourSet");
            //DropTable("dbo.MapSet");
            //DropTable("dbo.CompetitionSet");
        }
    }
}
