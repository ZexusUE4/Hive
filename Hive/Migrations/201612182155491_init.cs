namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
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
            
            CreateTable(
                "dbo.Project",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ManagerID = c.String(maxLength: 128),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.ManagerID)
                .Index(t => t.ManagerID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DefaultTeamID = c.Int(nullable: false),
                        JoinedDate = c.DateTime(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        ProfilePicturePath = c.String(),
                        Gender = c.Int(),
                        Bio = c.String(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Team", t => t.DefaultTeamID, cascadeDelete: true)
                .Index(t => t.DefaultTeamID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsDefaultTeam = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
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
                "dbo.TeamMember",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TeamID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        IsLeader = c.Boolean(nullable: false),
                        Member_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.AspNetUsers", t => t.Member_Id)
                .ForeignKey("dbo.Team", t => t.TeamID, cascadeDelete: true)
                .Index(t => t.TeamID)
                .Index(t => t.Member_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Project", "ManagerID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DefaultTeamID", "dbo.Team");
            DropForeignKey("dbo.TeamMember", "TeamID", "dbo.Team");
            DropForeignKey("dbo.TeamMember", "Member_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Task", "ProjectID", "dbo.Project");
            DropForeignKey("dbo.TaskTeam", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.TaskTeam", "Task_ID", "dbo.Task");
            DropForeignKey("dbo.TeamProject", "Project_ID", "dbo.Project");
            DropForeignKey("dbo.TeamProject", "Team_ID", "dbo.Team");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.TaskTeam", new[] { "Team_ID" });
            DropIndex("dbo.TaskTeam", new[] { "Task_ID" });
            DropIndex("dbo.TeamProject", new[] { "Project_ID" });
            DropIndex("dbo.TeamProject", new[] { "Team_ID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.TeamMember", new[] { "Member_Id" });
            DropIndex("dbo.TeamMember", new[] { "TeamID" });
            DropIndex("dbo.Task", new[] { "ProjectID" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "DefaultTeamID" });
            DropIndex("dbo.Project", new[] { "ManagerID" });
            DropTable("dbo.TaskTeam");
            DropTable("dbo.TeamProject");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.TeamMember");
            DropTable("dbo.Task");
            DropTable("dbo.Team");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Project");
            DropTable("dbo.Color");
        }
    }
}
