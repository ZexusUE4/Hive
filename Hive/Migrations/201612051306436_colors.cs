namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class colors : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project", "Color", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Project", "Color");
        }
    }
}
