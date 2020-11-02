namespace University_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminTbl",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.ClubMemberDLs",
                c => new
                    {
                        ClubMemberID = c.Int(nullable: false, identity: true),
                        Designation = c.String(nullable: false),
                        ClubID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ClubMemberID)
                .ForeignKey("dbo.ClubTbl", t => t.ClubID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.ClubID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ClubTbl",
                c => new
                    {
                        ClubID = c.Int(nullable: false, identity: true),
                        ClubName = c.String(nullable: false),
                        Eligibility = c.String(nullable: false),
                        ClubDescription = c.String(nullable: false),
                        Designations = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ClubID);
            
            CreateTable(
                "dbo.UserTbl",
                c => new
                    {
                        UserID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        DoB = c.DateTime(nullable: false),
                        Gender = c.String(nullable: false),
                        PhoneNumber = c.Long(nullable: false),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.CommentOnIdeaDLs",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Name = c.String(),
                        IdeaID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.IdeaTbl", t => t.IdeaID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.IdeaID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.IdeaTbl",
                c => new
                    {
                        IdeaID = c.Int(nullable: false, identity: true),
                        IdeaTitle = c.String(nullable: false),
                        IdeaDescription = c.String(nullable: false),
                        PostIdeaDate = c.DateTime(nullable: false),
                        UpVoteCount = c.Int(nullable: false),
                        DownVoteCount = c.Int(nullable: false),
                        ClubName = c.String(nullable: false),
                        Category = c.String(nullable: false),
                        Approve = c.String(),
                        UserID = c.Int(nullable: false),
                        ClubID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdeaID)
                .ForeignKey("dbo.ClubTbl", t => t.ClubID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ClubID);
            
            CreateTable(
                "dbo.ShareIdeaContentDL",
                c => new
                    {
                        ShareIdeaContentID = c.Int(nullable: false, identity: true),
                        IdeaID = c.Int(nullable: false),
                        SharedWithUserID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShareIdeaContentID)
                .ForeignKey("dbo.IdeaTbl", t => t.IdeaID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.IdeaID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.UserActivityOnIdeaTbl",
                c => new
                    {
                        UserActivityOnIdeaID = c.Int(nullable: false, identity: true),
                        UpVote = c.Int(nullable: false),
                        DownVote = c.Int(nullable: false),
                        IdeaID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserActivityOnIdeaID)
                .ForeignKey("dbo.IdeaTbl", t => t.IdeaID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.IdeaID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.PostCommentTbl",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Name = c.String(),
                        EventID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.EventTbl", t => t.EventID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.EventID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.EventTbl",
                c => new
                    {
                        EventID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDescription = c.String(nullable: false),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        ClubName = c.String(nullable: false),
                        LikeCount = c.Int(nullable: false),
                        DisLikeCount = c.Int(nullable: false),
                        Category = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.EventID);
            
            CreateTable(
                "dbo.SharedContentTbl",
                c => new
                    {
                        SharedCintentId = c.Int(nullable: false, identity: true),
                        UserID = c.Int(nullable: false),
                        EventID = c.Int(nullable: false),
                        SharedWithUserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SharedCintentId)
                .ForeignKey("dbo.EventTbl", t => t.EventID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.EventID);
            
            CreateTable(
                "dbo.UserOnEventActityTbl",
                c => new
                    {
                        UserActivityId = c.Int(nullable: false, identity: true),
                        Attendence = c.String(),
                        Like = c.Int(nullable: false),
                        Dislike = c.Int(nullable: false),
                        Interested = c.String(),
                        NotInterested = c.String(),
                        EventID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserActivityId)
                .ForeignKey("dbo.EventTbl", t => t.EventID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.EventID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.ServiceJoinerDLs",
                c => new
                    {
                        ServiceJoinerID = c.Int(nullable: false, identity: true),
                        Slot = c.String(),
                        UserID = c.Int(nullable: false),
                        ServiceID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceJoinerID)
                .ForeignKey("dbo.ServiceTbl", t => t.ServiceID)
                .ForeignKey("dbo.UserTbl", t => t.UserID)
                .Index(t => t.UserID)
                .Index(t => t.ServiceID);
            
            CreateTable(
                "dbo.ServiceTbl",
                c => new
                    {
                        ServiceID = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(nullable: false),
                        ServiceDescription = c.String(nullable: false),
                        Volunteer = c.Int(nullable: false),
                        Slots = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ServiceID);
            
            CreateTable(
                "dbo.ShareEventStoryDLs",
                c => new
                    {
                        ShareEventStoryID = c.Int(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        EventDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ShareEventStoryID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceJoinerDLs", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.ServiceJoinerDLs", "ServiceID", "dbo.ServiceTbl");
            DropForeignKey("dbo.PostCommentTbl", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.UserOnEventActityTbl", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.UserOnEventActityTbl", "EventID", "dbo.EventTbl");
            DropForeignKey("dbo.SharedContentTbl", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.SharedContentTbl", "EventID", "dbo.EventTbl");
            DropForeignKey("dbo.PostCommentTbl", "EventID", "dbo.EventTbl");
            DropForeignKey("dbo.CommentOnIdeaDLs", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.CommentOnIdeaDLs", "IdeaID", "dbo.IdeaTbl");
            DropForeignKey("dbo.IdeaTbl", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.UserActivityOnIdeaTbl", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.UserActivityOnIdeaTbl", "IdeaID", "dbo.IdeaTbl");
            DropForeignKey("dbo.ShareIdeaContentDL", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.ShareIdeaContentDL", "IdeaID", "dbo.IdeaTbl");
            DropForeignKey("dbo.IdeaTbl", "ClubID", "dbo.ClubTbl");
            DropForeignKey("dbo.ClubMemberDLs", "UserID", "dbo.UserTbl");
            DropForeignKey("dbo.ClubMemberDLs", "ClubID", "dbo.ClubTbl");
            DropIndex("dbo.ServiceJoinerDLs", new[] { "ServiceID" });
            DropIndex("dbo.ServiceJoinerDLs", new[] { "UserID" });
            DropIndex("dbo.UserOnEventActityTbl", new[] { "UserID" });
            DropIndex("dbo.UserOnEventActityTbl", new[] { "EventID" });
            DropIndex("dbo.SharedContentTbl", new[] { "EventID" });
            DropIndex("dbo.SharedContentTbl", new[] { "UserID" });
            DropIndex("dbo.PostCommentTbl", new[] { "UserID" });
            DropIndex("dbo.PostCommentTbl", new[] { "EventID" });
            DropIndex("dbo.UserActivityOnIdeaTbl", new[] { "UserID" });
            DropIndex("dbo.UserActivityOnIdeaTbl", new[] { "IdeaID" });
            DropIndex("dbo.ShareIdeaContentDL", new[] { "UserID" });
            DropIndex("dbo.ShareIdeaContentDL", new[] { "IdeaID" });
            DropIndex("dbo.IdeaTbl", new[] { "ClubID" });
            DropIndex("dbo.IdeaTbl", new[] { "UserID" });
            DropIndex("dbo.CommentOnIdeaDLs", new[] { "UserID" });
            DropIndex("dbo.CommentOnIdeaDLs", new[] { "IdeaID" });
            DropIndex("dbo.ClubMemberDLs", new[] { "UserID" });
            DropIndex("dbo.ClubMemberDLs", new[] { "ClubID" });
            DropTable("dbo.ShareEventStoryDLs");
            DropTable("dbo.ServiceTbl");
            DropTable("dbo.ServiceJoinerDLs");
            DropTable("dbo.UserOnEventActityTbl");
            DropTable("dbo.SharedContentTbl");
            DropTable("dbo.EventTbl");
            DropTable("dbo.PostCommentTbl");
            DropTable("dbo.UserActivityOnIdeaTbl");
            DropTable("dbo.ShareIdeaContentDL");
            DropTable("dbo.IdeaTbl");
            DropTable("dbo.CommentOnIdeaDLs");
            DropTable("dbo.UserTbl");
            DropTable("dbo.ClubTbl");
            DropTable("dbo.ClubMemberDLs");
            DropTable("dbo.AdminTbl");
        }
    }
}
