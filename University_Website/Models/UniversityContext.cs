namespace University_Website.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public partial class UniversityContext : DbContext
    {
        public UniversityContext()
            : base("name=connect")
        {
        }


       public virtual DbSet<AdminDL> Admins { get; set; }
        public virtual DbSet<UserDL> Users { get; set; }

        public virtual DbSet<EventDL> Events { get; set; }
        public virtual DbSet<UserOnEventActityDL> UserOnEventActities { get; set; }

        public virtual DbSet<PostCommentDL> PostComments { get; set; }
      
        public virtual DbSet<SharedContent> SharedContents { get; set; }

        public virtual DbSet<IdeaDL> Ideas { get; set; }
        public virtual DbSet<CommentOnIdeaDL> CommentOnIdeas { get; set; }
        public virtual DbSet<UserActivityOnIdea> UserActivityOnIdeas { get; set; }

        public virtual DbSet<ShareIdeaContentDL> ShareIdeaContents { get; set; }

        public virtual DbSet<ClubDL> Clubs { get; set; }

        public virtual DbSet<ClubMemberDL> ClubMembers { get; set; } 

        public virtual DbSet<ServiceDL> Services { get; set; }

        public virtual DbSet<ServiceJoinerDL> ServiceJoiners { get; set; }

        public virtual DbSet<ShareEventStoryDL> ShareEventStories { get; set; }
       

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

    }
}
