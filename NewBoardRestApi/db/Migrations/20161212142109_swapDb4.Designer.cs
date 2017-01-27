using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using NewBoardRestApi.DataModel;

namespace NewBoardRestApi.Migrations
{
    [DbContext(typeof(NewsBoardContext))]
    [Migration("20161212142109_swapDb4")]
    partial class swapDb4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("NewBoardRestApi.Model.Article", b =>
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

            modelBuilder.Entity("NewBoardRestApi.Model.Feed", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<bool>("IsActive");

                    b.Property<DateTime>("LastTimeFetched");

                    b.Property<string>("Title");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Feeds");
                });

            modelBuilder.Entity("NewBoardRestApi.Model.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("NewBoardRestApi.Model.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.Property<string>("Label2");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("NewBoardRestApi.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Label");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("NewBoardRestApi.Model.UserArticle", b =>
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

            modelBuilder.Entity("NewBoardRestApi.Model.UserFeed", b =>
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

            modelBuilder.Entity("NewBoardRestApi.Model.Article", b =>
                {
                    b.HasOne("NewBoardRestApi.Model.Feed", "Feed")
                        .WithMany("Articles")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.Model.UserArticle", b =>
                {
                    b.HasOne("NewBoardRestApi.Model.Article", "Article")
                        .WithMany("UserArticlesList")
                        .HasForeignKey("ArticleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.Model.User", "User")
                        .WithMany("UserArticles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("NewBoardRestApi.Model.UserFeed", b =>
                {
                    b.HasOne("NewBoardRestApi.Model.Feed", "Feed")
                        .WithMany("UserFeeds")
                        .HasForeignKey("FeedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("NewBoardRestApi.Model.User", "User")
                        .WithMany("UserFeeds")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
