namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIntersectionPointSet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IntersectionPointSet",
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
            
            AddColumn("dbo.ParcourSet", "PenWidthPROH", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 1));
            AddColumn("dbo.ParcourSet", "PenWidthGates", c => c.Decimal(nullable: false, precision: 18, scale: 2, defaultValue: 1));
            AddColumn("dbo.ParcourSet", "HasCircleOnGates", c => c.Boolean(nullable: false, defaultValue: false));
            AddColumn("dbo.ParcourSet", "ColorPROHArgb", c => c.Int(nullable: false, defaultValue: System.Drawing.Color.Red.ToArgb()));
            AddColumn("dbo.ParcourSet", "ColorGatesArgb", c => c.Int(nullable: false, defaultValue: System.Drawing.Color.Red.ToArgb()));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IntersectionPointSet", "Flight_Id", "dbo.FlightSet");
            DropIndex("dbo.IntersectionPointSet", new[] { "Flight_Id" });
            DropColumn("dbo.ParcourSet", "ColorGatesArgb");
            DropColumn("dbo.ParcourSet", "ColorPROHArgb");
            DropColumn("dbo.ParcourSet", "HasCircleOnGates");
            DropColumn("dbo.ParcourSet", "PenWidthGates");
            DropColumn("dbo.ParcourSet", "PenWidthPROH");
            DropTable("dbo.IntersectionPointSet");
        }
    }
}
