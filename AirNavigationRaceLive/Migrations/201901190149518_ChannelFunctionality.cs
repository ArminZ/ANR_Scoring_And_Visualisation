namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Migrations.Infrastructure;
    using System.Data.Entity.Migrations.Model;

    public partial class ChannelFunctionality : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParcourSet", "ColorIntersectionArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "ColorChannelFillArgb", c => c.Int(nullable: false));
            AddColumn("dbo.ParcourSet", "PenWidthChannel", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "PenWidthIntersection", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "IntersectionCircleRadius", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ParcourSet", "HasIntersectionCircles", c => c.Boolean(nullable: false));
            AddColumn("dbo.ParcourSet", "PenaltyCalcType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__Penal__540C7B00");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__HasIn__531856C7");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__Inter__5224328E");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__PenWi__51300E55");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__PenWi__503BEA1C");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__Color__4F47C5E3");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__Color__4E53A1AA");
            this.DeleteDefaultContraint("dbo.ParcourSet", "DF__ParcourSe__Color__4D5F7D71");
            DropColumn("dbo.ParcourSet", "PenaltyCalcType");
            DropColumn("dbo.ParcourSet", "HasIntersectionCircles");
            DropColumn("dbo.ParcourSet", "IntersectionCircleRadius");
            DropColumn("dbo.ParcourSet", "PenWidthIntersection");
            DropColumn("dbo.ParcourSet", "PenWidthChannel");
            DropColumn("dbo.ParcourSet", "ColorChannelFillArgb");
            DropColumn("dbo.ParcourSet", "ColorChannelArgb");
            DropColumn("dbo.ParcourSet", "ColorIntersectionArgb");
        }
    }
}
