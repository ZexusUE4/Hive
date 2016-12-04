namespace Hive.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hiveUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Course", "ActivityID", "dbo.Activity");
            DropForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Course", "VenueID", "dbo.Venue");
            DropForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor");
            DropForeignKey("dbo.Enrollement", "CourseID", "dbo.Course");
            DropForeignKey("dbo.Enrollement", "MemberID", "dbo.Member");
            DropForeignKey("dbo.AspNetUsers", "MemberID", "dbo.Member");
            DropIndex("dbo.CourseInstructor", new[] { "CourseID" });
            DropIndex("dbo.CourseInstructor", new[] { "InstructorID" });
            DropIndex("dbo.Course", new[] { "ActivityID" });
            DropIndex("dbo.Course", new[] { "VenueID" });
            DropIndex("dbo.Enrollement", new[] { "CourseID" });
            DropIndex("dbo.Enrollement", new[] { "MemberID" });
            DropIndex("dbo.AspNetUsers", new[] { "MemberID" });
            AddColumn("dbo.AspNetUsers", "JoinedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "FirstName", c => c.String());
            AddColumn("dbo.AspNetUsers", "LastName", c => c.String());
            AddColumn("dbo.AspNetUsers", "Bio", c => c.String());
            DropColumn("dbo.AspNetUsers", "MemberID");
            DropTable("dbo.Activity");
            DropTable("dbo.CourseInstructor");
            DropTable("dbo.Course");
            DropTable("dbo.Venue");
            DropTable("dbo.Instructor");
            DropTable("dbo.Enrollement");
            DropTable("dbo.Member");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Member",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(),
                        Type = c.Int(),
                        Barcode = c.String(),
                        PersonalIdentityNumber = c.String(),
                        Name = c.String(),
                        ArabicName = c.String(),
                        PicturePath = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Mobile = c.String(),
                        Address = c.String(),
                        BirthDate = c.DateTime(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Enrollement",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        MemberID = c.Int(nullable: false),
                        Type = c.Int(),
                        Status = c.Int(),
                        EnrollementStartDate = c.DateTime(),
                        EnrollementEndDate = c.DateTime(),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Status = c.Int(),
                        Type = c.Int(),
                        Name = c.String(),
                        PicturePath = c.String(),
                        Email = c.String(),
                        Phone = c.String(),
                        Address = c.String(),
                        Mobile = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Venue",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        Status = c.Int(),
                        Title = c.String(),
                        Location = c.String(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ActivityID = c.Int(nullable: false),
                        VenueID = c.Int(nullable: false),
                        Type = c.Int(),
                        Status = c.Int(),
                        Title = c.String(),
                        Notes = c.String(),
                        Fees = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.CourseInstructor",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CourseID = c.Int(nullable: false),
                        InstructorID = c.Int(nullable: false),
                        Status = c.Int(),
                        Type = c.Int(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        Status = c.Int(),
                        Title = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            AddColumn("dbo.AspNetUsers", "MemberID", c => c.Int());
            DropColumn("dbo.AspNetUsers", "Bio");
            DropColumn("dbo.AspNetUsers", "LastName");
            DropColumn("dbo.AspNetUsers", "FirstName");
            DropColumn("dbo.AspNetUsers", "JoinedDate");
            CreateIndex("dbo.AspNetUsers", "MemberID");
            CreateIndex("dbo.Enrollement", "MemberID");
            CreateIndex("dbo.Enrollement", "CourseID");
            CreateIndex("dbo.Course", "VenueID");
            CreateIndex("dbo.Course", "ActivityID");
            CreateIndex("dbo.CourseInstructor", "InstructorID");
            CreateIndex("dbo.CourseInstructor", "CourseID");
            AddForeignKey("dbo.AspNetUsers", "MemberID", "dbo.Member", "ID");
            AddForeignKey("dbo.Enrollement", "MemberID", "dbo.Member", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Enrollement", "CourseID", "dbo.Course", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "InstructorID", "dbo.Instructor", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Course", "VenueID", "dbo.Venue", "ID", cascadeDelete: true);
            AddForeignKey("dbo.CourseInstructor", "CourseID", "dbo.Course", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Course", "ActivityID", "dbo.Activity", "ID", cascadeDelete: true);
        }
    }
}
