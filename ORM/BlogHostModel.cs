namespace ORM
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BlogHostModel : DbContext
    {
        public BlogHostModel()
            : base("name=BlogHostModel")
        {
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Blog> Blogs { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Article)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Articles)
                .Map(m => m.ToTable("ArticleTags").MapLeftKey("ArticleId").MapRightKey("TagId"));

            modelBuilder.Entity<Blog>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Blog)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.ArticlesLikes)
                .WithMany(e => e.LikedUsers)
                .Map(m => m.ToTable("ArticleLikes").MapLeftKey("UserId").MapRightKey("ArticleId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.BlogsLikes)
                .WithMany(e => e.LikedUsers)
                .Map(m => m.ToTable("BlogLikes").MapLeftKey("UserId").MapRightKey("BlogId"));

            modelBuilder.Entity<User>()
                .HasMany(e => e.CommentsLikes)
                .WithMany(e => e.LikedUsers)
                .Map(m => m.ToTable("CommentLikes").MapLeftKey("UserId").MapRightKey("CommentId"));
        }
    }
}
