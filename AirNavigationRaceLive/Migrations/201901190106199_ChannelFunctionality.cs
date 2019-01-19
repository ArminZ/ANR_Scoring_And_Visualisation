namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChannelFunctionality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParcourSet", "ColorIntersectionArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelFillArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "PenWidthChannel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "PenaltyCalcType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParcourSet", "PenaltyCalcType");
            DropColumn("dbo.ParcourSet", "PenWidthChannel");
            DropColumn("dbo.ParcourSet", "ColorChannelFillArgb");
            DropColumn("dbo.ParcourSet", "ColorChannelArgb");
            DropColumn("dbo.ParcourSet", "ColorIntersectionArgb");
        }
    }
}
