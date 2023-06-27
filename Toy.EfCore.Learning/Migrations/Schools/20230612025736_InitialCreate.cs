using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toy.EfCore.Learning.Migrations.Schools
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "class_room",
                columns: table => new
                {
                    grade = table.Column<int>(type: "int", nullable: false),
                    @class = table.Column<int>(name: "class", type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_room", x => new { x.grade, x.@class });
                });

            migrationBuilder.CreateTable(
                name: "school",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    globally_unique_identifier = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school", x => x.id);
                    table.UniqueConstraint("AK_school_globally_unique_identifier", x => x.globally_unique_identifier);
                });

            migrationBuilder.CreateTable(
                name: "class_room_schedule",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    Grade = table.Column<int>(type: "int", nullable: false),
                    Class = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class_room_schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_class_room_schedule_class_room_Grade_Class",
                        columns: x => new { x.Grade, x.Class },
                        principalTable: "class_room",
                        principalColumns: new[] { "grade", "class" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "school_action",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    change_action = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    school_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_action", x => x.id);
                    table.ForeignKey(
                        name: "FK_school_action_school_school_id",
                        column: x => x.school_id,
                        principalTable: "school",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "school_secret_action",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    SchoolGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_school_secret_action", x => x.id);
                    table.ForeignKey(
                        name: "FK_school_secret_action_school_SchoolGuid",
                        column: x => x.SchoolGuid,
                        principalTable: "school",
                        principalColumn: "globally_unique_identifier",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    enrollment_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    timestamp_version = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    school_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentId", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_school_school_id",
                        column: x => x.school_id,
                        principalTable: "school",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "student_action",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    change_action = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_action", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_action_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_commute",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    fingerprint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    commute_type = table.Column<int>(type: "int", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    StudentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentCommuteId", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_commute_student_StudentId",
                        column: x => x.StudentId,
                        principalTable: "student",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "student_name",
                columns: table => new
                {
                    first_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "This is personal name"),
                    mid_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "What is mid name?"),
                    last_name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false, comment: "This is family name"),
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true, computedColumnSql: "[first_name] + ' ' + [mid_name] + ' ' + [last_name]", comment: "This is full name"),
                    full_name_length = table.Column<int>(type: "int", nullable: false, computedColumnSql: "LEN([first_name]) + LEN([mid_name]) + LEN([mid_name])", stored: true, comment: "This is full name length"),
                    student_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_student_name", x => x.id);
                    table.ForeignKey(
                        name: "FK_student_name_student_student_id",
                        column: x => x.student_id,
                        principalTable: "student",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_class_room_schedule_Grade_Class",
                table: "class_room_schedule",
                columns: new[] { "Grade", "Class" });

            migrationBuilder.CreateIndex(
                name: "IX_school_globally_unique_identifier",
                table: "school",
                column: "globally_unique_identifier",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_school_action_school_id",
                table: "school_action",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_school_secret_action_SchoolGuid",
                table: "school_secret_action",
                column: "SchoolGuid");

            migrationBuilder.CreateIndex(
                name: "IX_student_school_id",
                table: "student",
                column: "school_id");

            migrationBuilder.CreateIndex(
                name: "IX_student_action_StudentId",
                table: "student_action",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_commute_StudentId",
                table: "student_commute",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_student_name_student_id",
                table: "student_name",
                column: "student_id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "class_room_schedule");

            migrationBuilder.DropTable(
                name: "school_action");

            migrationBuilder.DropTable(
                name: "school_secret_action");

            migrationBuilder.DropTable(
                name: "student_action");

            migrationBuilder.DropTable(
                name: "student_commute");

            migrationBuilder.DropTable(
                name: "student_name");

            migrationBuilder.DropTable(
                name: "class_room");

            migrationBuilder.DropTable(
                name: "student");

            migrationBuilder.DropTable(
                name: "school");
        }
    }
}
