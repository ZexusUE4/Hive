namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamLeaderID : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Team", new[] { "TeamLeader_Id" });
            DropColumn("dbo.Team", "TeamLeaderID");
            RenameColumn(table: "dbo.Team", name: "TeamLeader_Id", newName: "TeamLeaderID");
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.String(maxLength: 128));
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.String(maxLength: 128));
            CreateIndex("dbo.Team", "TeamLeaderID");
            DropColumn("dbo.AspNetUsers", "DefaultTeamID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DefaultTeamID", c => c.Int(nullable: false));
            DropIndex("dbo.Team", new[] { "TeamLeaderID" });
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Team", "TeamLeaderID", c => c.String());
            RenameColumn(table: "dbo.Team", name: "TeamLeaderID", newName: "TeamLeader_Id");
            AddColumn("dbo.Team", "TeamLeaderID", c => c.String());
            CreateIndex("dbo.Team", "TeamLeader_Id");
        }
    }
}
