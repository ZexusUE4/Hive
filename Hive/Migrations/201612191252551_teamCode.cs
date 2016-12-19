namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamCode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Team", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Team", "Code");
        }
    }
}
