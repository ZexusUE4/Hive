namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colorable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "ColorID", c => c.Int());
            AddColumn("dbo.Team", "ColorID", c => c.Int());
            CreateIndex("dbo.Project", "ColorID");
            CreateIndex("dbo.Team", "ColorID");
            AddForeignKey("dbo.Project", "ColorID", "dbo.Color", "ID");
            AddForeignKey("dbo.Team", "ColorID", "dbo.Color", "ID");
            DropColumn("dbo.Project", "Color");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Project", "Color", c => c.String());
            DropForeignKey("dbo.Team", "ColorID", "dbo.Color");
            DropForeignKey("dbo.Project", "ColorID", "dbo.Color");
            DropIndex("dbo.Team", new[] { "ColorID" });
            DropIndex("dbo.Project", new[] { "ColorID" });
            DropColumn("dbo.Team", "ColorID");
            DropColumn("dbo.Project", "ColorID");
        }
    }
}
