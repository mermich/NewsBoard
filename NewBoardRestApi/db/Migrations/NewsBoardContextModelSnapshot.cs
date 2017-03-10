using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Migrations
{
    [DbContext(typeof(NewsBoardContext))]
    partial class NewsBoardContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NewBoardRestApi.DataModel.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedId");

                    b.Property<string>("Label");

                    b.Property<DateTime>("LastUpdatedTime");

                    b.Property<DateTime>("PublishDate");

                    b.Property<string>("Summary");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.ToTable("Articles");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Feed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastTimeFetched");

                    b.Property<string>("SyndicationUrl");

                    b.Property<string>("Title");

                    b.Property<int>("WebSiteId");

                    b.HasKey("Id");

                    b.HasIndex("WebSiteId");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.FeedTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.HasIndex("TagId");

                    b.ToTable("FeedTags");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.GroupPermission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<int>("PermissionId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("PermissionId");

                    b.ToTable("GroupPermissions");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Enabled");

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserArticle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ArticleId");

                    b.Property<bool>("IsHidden");

                    b.Property<bool>("IsOpened");

                    b.Property<string>("Label");

                    b.Property<int>("Score");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserArticles");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserFeed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FeedId");

                    b.Property<bool>("IsReported");

                    b.Property<bool>("IsSubscribed");

                    b.Property<string>("Label");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FeedId");

                    b.HasIndex("UserId");

                    b.ToTable("UserFeeds");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("GroupId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.WebSite", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("IconUrl");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("WebSites");
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Article", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Feed", "Feed")
                        .WithMany("Articles")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.Feed", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.WebSite", "WebSite")
                        .WithMany("Feeds")
                        .HasForeignKey("WebSiteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.FeedTag", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Feed", "Feed")
                        .WithMany("FeedTags")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.DataModel.Tag", "Tag")
                        .WithMany("FeedTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.GroupPermission", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Group", "Group")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.DataModel.Permission", "Permission")
                        .WithMany("GroupPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserArticle", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Article", "Article")
                        .WithMany("UserArticles")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.DataModel.User", "User")
                        .WithMany("UserArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserFeed", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Feed", "Feed")
                        .WithMany("UserFeeds")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.DataModel.User", "User")
                        .WithMany("UserFeeds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.DataModel.UserGroup", b =>
                {
                    b.HasOne("NewBoardRestApi.DataModel.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.DataModel.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
