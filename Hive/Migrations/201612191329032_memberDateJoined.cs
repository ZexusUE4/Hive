namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class memberDateJoined : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TeamMember", "DateJoined", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TeamMember", "DateJoined");
        }
    }
}
