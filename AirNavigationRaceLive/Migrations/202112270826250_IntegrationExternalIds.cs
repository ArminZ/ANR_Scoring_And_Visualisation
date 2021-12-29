namespace AirNavigationRaceLive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IntegrationExternalIds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamSet", "External_Id", c => c.String());
            AddColumn("dbo.SubscriberSet", "External_Id", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SubscriberSet", "External_Id");
            DropColumn("dbo.TeamSet", "External_Id");
        }
    }
}
