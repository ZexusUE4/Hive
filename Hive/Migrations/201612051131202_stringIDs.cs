namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringIDs : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Project", new[] { "Manager_Id" });
            DropColumn("dbo.Project", "ManagerID");
            RenameColumn(table: "dbo.Project", name: "Manager_Id", newName: "ManagerID");
            AlterColumn("dbo.Project", "ManagerID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.String());
            CreateIndex("dbo.Project", "ManagerID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Project", new[] { "ManagerID" });
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.Int(nullable: false));
            AlterColumn("dbo.Project", "ManagerID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Project", name: "ManagerID", newName: "Manager_Id");
            AddColumn("dbo.Project", "ManagerID", c => c.Int(nullable: false));
            CreateIndex("dbo.Project", "Manager_Id");
        }
    }
}
