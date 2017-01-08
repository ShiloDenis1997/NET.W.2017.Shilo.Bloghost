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

        public virtual DbSet<Articles> Articles { get; set; }
        public virtual DbSet<ArticleTags> ArticleTags { get; set; }
        public virtual DbSet<Blogs> Blogs { get; set; }
        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Tags> Tags { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articles>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Articles>()
                .HasMany(e => e.ArticleTags)
                .WithRequired(e => e.Articles)
                .HasForeignKey(e => e.ArticleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Articles>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Articles)
                .HasForeignKey(e => e.ArticleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Blogs>()
                .HasMany(e => e.Articles)
                .WithRequired(e => e.Blogs)
                .HasForeignKey(e => e.BlogId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Comments>()
                .Property(e => e.Content)
                .IsUnicode(false);

            modelBuilder.Entity<Roles>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Roles)
                .HasForeignKey(e => e.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tags>()
                .HasMany(e => e.ArticleTags)
                .WithRequired(e => e.Tags)
                .HasForeignKey(e => e.TagId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Blogs)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Articles)
                .WithMany(e => e.Users)
                .Map(m => m.ToTable("ArticleLikes").MapLeftKey("UserId").MapRightKey("ArticleId"));

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Blogs1)
                .WithMany(e => e.Users1)
                .Map(m => m.ToTable("BlogLikes").MapLeftKey("UserId").MapRightKey("BlogId"));

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Comments1)
                .WithMany(e => e.Users1)
                .Map(m => m.ToTable("CommentLikes").MapLeftKey("UserId").MapRightKey("CommentId"));
        }
    }
}
