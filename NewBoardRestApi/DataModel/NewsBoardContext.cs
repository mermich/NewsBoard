using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace NewBoardRestApi.DataModel
{
    public class TemporaryDbContextFactory : IDbContextFactory<NewsBoardContext>
    {
        public NewsBoardContext Create()
        {
            var builder = new DbContextOptionsBuilder<NewsBoardContext>();
            builder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\source\emermillod\CodingNight\Guild\src\NewBoardRestApi\NewsBoardContext.mdf;Integrated Security=True;Connect Timeout=30");
            return new NewsBoardContext(builder.Options);
        }

        public NewsBoardContext Create(DbContextFactoryOptions options)
        {
            var builder = new DbContextOptionsBuilder<NewsBoardContext>();
            builder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\source\emermillod\CodingNight\Guild\src\NewBoardRestApi\NewsBoardContext.mdf;Integrated Security=True;Connect Timeout=30");
            return new NewsBoardContext(builder.Options);
        }
    }


    public class NewsBoardContext : DbContext
    {
        public NewsBoardContext(DbContextOptions<NewsBoardContext> options)
            : base(options)
        { }


        public NewsBoardContext() :this (new DbContextOptions<NewsBoardContext>())
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\source\emermillod\CodingNight\Guild\src\NewBoardRestApi\NewsBoardContext.mdf;Integrated Security=True;Connect Timeout=30");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Article> Articles { get; set; }


        public DbSet<Feed> Feeds { get; set; }


        public DbSet<Tag> Tags { get; set; }

        public DbSet<FeedTag> FeedTags { get; set; }

        public DbSet<UserArticle> UserArticles { get; set; }

        public DbSet<UserFeed> UserFeeds { get; set; }

        public DbSet<UserGroup> UserGroups { get; set; }

        public DbSet<GroupPermission> GroupPermissions { get; set; }

        public DbSet<Group> Groups { get; set; }


        public DbSet<User> Users { get; set; }

        public DbSet<Permission> Permissions { get; set; }
    }
}