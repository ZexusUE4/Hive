namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allColors : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Project", "ColorID", "dbo.Color");
            DropForeignKey("dbo.Team", "ColorID", "dbo.Color");
            DropIndex("dbo.Project", new[] { "ColorID" });
            DropIndex("dbo.Team", new[] { "ColorID" });
            AddColumn("dbo.Project", "Color", c => c.String());
            AddColumn("dbo.Team", "Color", c => c.String());
            DropColumn("dbo.Project", "ColorID");
            DropColumn("dbo.Team", "ColorID");
            DropTable("dbo.Color");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Color",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CSSCode = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.Team", "ColorID", c => c.Int());
            AddColumn("dbo.Project", "ColorID", c => c.Int());
            DropColumn("dbo.Team", "Color");
            DropColumn("dbo.Project", "Color");
            CreateIndex("dbo.Team", "ColorID");
            CreateIndex("dbo.Project", "ColorID");
            AddForeignKey("dbo.Team", "ColorID", "dbo.Color", "ID");
            AddForeignKey("dbo.Project", "ColorID", "dbo.Color", "ID");
        }
    }
}
