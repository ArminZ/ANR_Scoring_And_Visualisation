using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirNavigationRaceLive
{
    public partial class AddParcourFieldsClass : DbMigration
    {
       
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Posts",
            //    c => new
            //    {
            //        PostId = c.Int(nullable: false, identity: true),
            //        Title = c.String(maxLength: 200),
            //        Content = c.String(),
            //        BlogId = c.Int(nullable: false),
            //    })
            //    .PrimaryKey(t => t.PostId)
            //    .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
            //    .Index(t => t.BlogId)
            //    .Index(p => p.Title, unique: true);

            AddColumn("dbo.ParcourSet", "Color", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.ParcourSet", "LineWidth", c => c.Int(nullable: false, defaultValue: 0));
        }

        public override void Down()
        {
            //DropIndex("dbo.Posts", new[] { "Title" });
            //DropIndex("dbo.Posts", new[] { "BlogId" });
            //DropForeignKey("dbo.Posts", "BlogId", "dbo.Blogs");
            DropColumn("dbo.ParcourSet", "Color");
            DropColumn("dbo.ParcourSet", "LineWidth");
            //DropTable("dbo.Posts");
        }
    }
}
