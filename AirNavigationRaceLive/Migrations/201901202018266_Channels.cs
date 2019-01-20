namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Channels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParcourSet", "ColorPROHBorderArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorIntersectionArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelFillArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "PenWidthPROHBorder", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "PenWidthChannel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "PenWidthIntersection", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "IntersectionCircleRadius", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "HasIntersectionCircles", c => c.Boolean(nullable: false));
            AddColumn("dbo.ParcourSet", "HasPROHBorder", c => c.Boolean(nullable: false));
            AddColumn("dbo.ParcourSet", "PenaltyCalcType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            this.DeleteDefaultConstraint("dbo.ParcourSet", "PenaltyCalcType");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "HasPROHBorder");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "HasIntersectionCircles");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "IntersectionCircleRadius");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "PenWidthIntersection");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "PenWidthChannel");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "PenWidthPROHBorder");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "ColorChannelFillArgb");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "ColorChannelArgb");
            this.DeleteDefaultConstraint("dbo.ParcourSet", "ColorIntersectionArgb");

            DropColumn("dbo.ParcourSet", "PenaltyCalcType");
            DropColumn("dbo.ParcourSet", "HasPROHBorder");
            DropColumn("dbo.ParcourSet", "HasIntersectionCircles");
            DropColumn("dbo.ParcourSet", "IntersectionCircleRadius");
            DropColumn("dbo.ParcourSet", "PenWidthIntersection");
            DropColumn("dbo.ParcourSet", "PenWidthChannel");
            DropColumn("dbo.ParcourSet", "PenWidthPROHBorder");
            DropColumn("dbo.ParcourSet", "ColorChannelFillArgb");
            DropColumn("dbo.ParcourSet", "ColorChannelArgb");
            DropColumn("dbo.ParcourSet", "ColorIntersectionArgb");
            DropColumn("dbo.ParcourSet", "ColorPROHBorderArgb");
        }
    }
}
