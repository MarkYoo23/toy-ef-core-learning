﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toy.EfCore.Learning.Infrastructures.Contexts;

#nullable disable

namespace Toy.EfCore.Learning.Migrations.Blogs
{
    [DbContext(typeof(BlogContext))]
    [Migration("20230614054825_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.BlogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("url");

                    b.Property<string>("blog_type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("blog", (string)null);

                    b.HasDiscriminator<string>("blog_type").HasValue("blog_default");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.PostEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.ToTable("post", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.PostTagEntity", b =>
                {
                    b.Property<long>("PostId")
                        .HasColumnType("bigint")
                        .HasColumnName("post_id");

                    b.Property<long>("TagId")
                        .HasColumnType("bigint")
                        .HasColumnName("tag_id");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.HasKey("PostId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("post_tag_mapping", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.TagEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("content");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Id");

                    b.ToTable("tag", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.CommunityBlogEntity", b =>
                {
                    b.HasBaseType("Toy.EfCore.Learning.Domains.Models.Blogs.BlogEntity");

                    b.Property<string>("CommunityName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("community_name");

                    b.HasDiscriminator().HasValue("blog_community");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.DevelopBlogEntity", b =>
                {
                    b.HasBaseType("Toy.EfCore.Learning.Domains.Models.Blogs.BlogEntity");

                    b.Property<string>("SourceCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("source_code");

                    b.HasDiscriminator().HasValue("blog_develop");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.PostTagEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Blogs.PostEntity", "Post")
                        .WithMany("PostTags")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Blogs.TagEntity", "Tag")
                        .WithMany("PostTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.PostEntity", b =>
                {
                    b.Navigation("PostTags");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Blogs.TagEntity", b =>
                {
                    b.Navigation("PostTags");
                });
#pragma warning restore 612, 618
        }
    }
}
