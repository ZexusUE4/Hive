namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberIDString : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TeamMember", new[] { "Member_Id" });
            DropColumn("dbo.TeamMember", "MemberID");
            RenameColumn(table: "dbo.TeamMember", name: "Member_Id", newName: "MemberID");
            AlterColumn("dbo.TeamMember", "MemberID", c => c.String(maxLength: 128));
            CreateIndex("dbo.TeamMember", "MemberID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TeamMember", new[] { "MemberID" });
            AlterColumn("dbo.TeamMember", "MemberID", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.TeamMember", name: "MemberID", newName: "Member_Id");
            AddColumn("dbo.TeamMember", "MemberID", c => c.Int(nullable: false));
            CreateIndex("dbo.TeamMember", "Member_Id");
        }
    }
}
