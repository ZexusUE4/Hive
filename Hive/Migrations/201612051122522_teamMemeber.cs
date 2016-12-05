namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class teamMemeber : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ManagerID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Manager_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Manager_Id)
                .Index(t => t.Manager_Id);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamLeaderID = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        TeamLeader_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.TeamLeader_Id)
                .Index(t => t.TeamLeader_Id);
            
            CreateTable(
                "dbo.Task",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Status = c.Int(nullable: false),
                        Priority = c.Int(nullable: false),
                        Deadline = c.DateTime(nullable: false),
                        DateAssigned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Project", t => t.ProjectID, cascadeDelete: true)
                .Index(t => t.ProjectID);
            
            CreateTable(
                "dbo.TeamHiveUser",
                c => new
                    {
                        Team_ID = c.Int(nullable: false),
                        HiveUser_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.Team_ID, t.HiveUser_Id })
                .ForeignKey("dbo.Team", t => t.Team_ID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.HiveUser_Id, cascadeDelete: true)
                .Index(t => t.Team_ID)
                .Index(t => t.HiveUser_Id);
            
            CreateTable(
                "dbo.TeamProject",
                c => new
                    {
                        Team_ID = c.Int(nullable: false),
                        Project_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Team_ID, t.Project_ID })
                .ForeignKey("dbo.Team", t => t.Team_ID, cascadeDelete: true)
                .ForeignKey("dbo.Project", t => t.Project_ID, cascadeDelete: true)
                .Index(t => t.Team_ID)
                .Index(t => t.Project_ID);
            
            CreateTable(
                "dbo.TaskTeam",
                c => new
                    {
                        Task_ID = c.Int(nullable: false),
                        Team_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Task_ID, t.Team_ID })
                .ForeignKey("dbo.Task", t => t.Task_ID, cascadeDelete: true)
                .ForeignKey("dbo.Team", t => t.Team_ID, cascadeDelete: true)
                .Index(t => t.Task_ID)
                .Index(t => t.Team_ID);
            
            AddColumn("dbo.AspNetUsers", "DefaultTeamID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project", "Manager_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Team", "TeamLeader_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Task", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.TaskTeam", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.TaskTeam", "Task_ID", "dbo.Task");
            DropForeignKey("dbo.TeamProject", "Project_ID", "dbo.Project");
            DropForeignKey("dbo.TeamProject", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.TeamHiveUser", "HiveUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeamHiveUser", "Team_ID", "dbo.Team");
            DropIndex("dbo.TaskTeam", new[] { "Team_ID" });
            DropIndex("dbo.TaskTeam", new[] { "Task_ID" });
            DropIndex("dbo.TeamProject", new[] { "Project_ID" });
            DropIndex("dbo.TeamProject", new[] { "Team_ID" });
            DropIndex("dbo.TeamHiveUser", new[] { "HiveUser_Id" });
            DropIndex("dbo.TeamHiveUser", new[] { "Team_ID" });
            DropIndex("dbo.Task", new[] { "ProjectID" });
            DropIndex("dbo.Team", new[] { "TeamLeader_Id" });
            DropIndex("dbo.Project", new[] { "Manager_Id" });
            DropColumn("dbo.AspNetUsers", "DefaultTeamID");
            DropTable("dbo.TaskTeam");
            DropTable("dbo.TeamProject");
            DropTable("dbo.TeamHiveUser");
            DropTable("dbo.Task");
            DropTable("dbo.Team");
            DropTable("dbo.Project");
        }
    }
}
