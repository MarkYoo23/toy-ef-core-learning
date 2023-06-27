﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Toy.EfCore.Learning.Infrastructures.Contexts;

#nullable disable

namespace Toy.EfCore.Learning.Migrations.Schools
{
    [DbContext(typeof(SchoolContext))]
    partial class SchoolContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.ClassRoomEntity", b =>
                {
                    b.Property<int>("Grade")
                        .HasColumnType("int")
                        .HasColumnName("grade");

                    b.Property<int>("Class")
                        .HasColumnType("int")
                        .HasColumnName("class");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.HasKey("Grade", "Class");

                    b.ToTable("class_room", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.ClassRoomScheduleEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("end_time");

                    b.Property<int>("Grade")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("start_time");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("Grade", "Class");

                    b.ToTable("class_room_schedule", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolActionLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("ChangeAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("change_action");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("SchoolId")
                        .HasColumnType("bigint")
                        .HasColumnName("school_id");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("school_action", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolEntity", b =>
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

                    b.Property<Guid>("GloballyUniqueIdentifier")
                        .HasColumnType("uniqueidentifier")
                        .HasColumnName("globally_unique_identifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.HasIndex("GloballyUniqueIdentifier")
                        .IsUnique();

                    b.ToTable("school", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolSecretActionLogEntity", b =>
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

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("message");

                    b.Property<Guid>("SchoolGuid")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("SchoolGuid");

                    b.ToTable("school_secret_action", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentActionLogEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("ChangeAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("change_action");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("student_action", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentCommuteEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<int>("CommuteType")
                        .HasColumnType("int")
                        .HasColumnName("commute_type");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasColumnName("created")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Fingerprint")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("fingerprint");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id")
                        .HasName("PK_StudentCommuteId");

                    b.HasIndex("StudentId");

                    b.ToTable("student_commute", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", b =>
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

                    b.Property<DateTime>("EnrollmentDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("enrollment_date");

                    b.Property<long?>("SchoolId")
                        .HasColumnType("bigint")
                        .HasColumnName("school_id");

                    b.Property<byte[]>("TimeStampVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion")
                        .HasColumnName("timestamp_version");

                    b.HasKey("Id")
                        .HasName("PK_StudentId");

                    b.HasIndex("SchoolId");

                    b.ToTable("student", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentNameEntity", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"), 1L, 1);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("first_name")
                        .HasColumnOrder(0)
                        .HasComment("This is personal name");

                    b.Property<string>("FullName")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasMaxLength(60)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(60)")
                        .HasColumnName("full_name")
                        .HasComputedColumnSql("[first_name] + ' ' + [mid_name] + ' ' + [last_name]")
                        .HasComment("This is full name");

                    b.Property<int>("FullNameLength")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("int")
                        .HasColumnName("full_name_length")
                        .HasComputedColumnSql("LEN([first_name]) + LEN([mid_name]) + LEN([mid_name])", true)
                        .HasComment("This is full name length");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("last_name")
                        .HasColumnOrder(2)
                        .HasComment("This is family name");

                    b.Property<string>("MidName")
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("nvarchar(20)")
                        .HasColumnName("mid_name")
                        .HasColumnOrder(1)
                        .HasComment("What is mid name?");

                    b.Property<long>("StudentId")
                        .HasColumnType("bigint")
                        .HasColumnName("student_id");

                    b.HasKey("Id");

                    b.HasIndex("StudentId")
                        .IsUnique();

                    b.ToTable("student_name", (string)null);
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.ClassRoomScheduleEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.ClassRoomEntity", "ClassRoom")
                        .WithMany("ClassRoomSchedules")
                        .HasForeignKey("Grade", "Class")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ClassRoom");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolActionLogEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.SchoolEntity", null)
                        .WithMany()
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolSecretActionLogEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.SchoolEntity", "School")
                        .WithMany("SecretActionLogs")
                        .HasForeignKey("SchoolGuid")
                        .HasPrincipalKey("GloballyUniqueIdentifier")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("School");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentActionLogEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentCommuteEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", "Student")
                        .WithMany("StudentCommutes")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.SchoolEntity", "School")
                        .WithMany("Students")
                        .HasForeignKey("SchoolId");

                    b.Navigation("School");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentNameEntity", b =>
                {
                    b.HasOne("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", "Student")
                        .WithOne("StudentName")
                        .HasForeignKey("Toy.EfCore.Learning.Domains.Models.Schools.StudentNameEntity", "StudentId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.ClassRoomEntity", b =>
                {
                    b.Navigation("ClassRoomSchedules");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.SchoolEntity", b =>
                {
                    b.Navigation("SecretActionLogs");

                    b.Navigation("Students");
                });

            modelBuilder.Entity("Toy.EfCore.Learning.Domains.Models.Schools.StudentEntity", b =>
                {
                    b.Navigation("StudentCommutes");

                    b.Navigation("StudentName");
                });
#pragma warning restore 612, 618
        }
    }
}