using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;

namespace NewBoardRestApi.DataModel
{
    public class NewsBoardContext : DbContext
    {
        public NewsBoardContext(DbContextOptions<NewsBoardContext> options)
            : base(options)
        { }


        public NewsBoardContext() :this (new DbContextOptions<NewsBoardContext>())
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", true);

            optionsBuilder.UseSqlServer(builder.Build().GetConnectionString("DefaultConnection"));
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Article> Articles { get; set; }


        public DbSet<Feed> Feeds { get; set; }

        public DbSet<WebSite> WebSites { get; set; }


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