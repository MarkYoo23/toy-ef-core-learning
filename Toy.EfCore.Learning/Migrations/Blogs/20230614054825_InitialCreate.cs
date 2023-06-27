using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toy.EfCore.Learning.Migrations.Blogs
{
    public partial class InitialCreate : Migration
    {
        private const string AddProcedureQuery = @"
/* USP : user storage procedure */
CREATE PROCEDURE usp_get_blog
    @Count INTEGER
AS
SELECT TOP(@Count)
   *
FROM
    blog
ORDER BY
    id DESC
";
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    blog_type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    community_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    source_code = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tag",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tag", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "post_tag_mapping",
                columns: table => new
                {
                    post_id = table.Column<long>(type: "bigint", nullable: false),
                    tag_id = table.Column<long>(type: "bigint", nullable: false),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_post_tag_mapping", x => new { x.post_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_post_tag_mapping_post_post_id",
                        column: x => x.post_id,
                        principalTable: "post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_post_tag_mapping_tag_tag_id",
                        column: x => x.tag_id,
                        principalTable: "tag",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_post_tag_mapping_tag_id",
                table: "post_tag_mapping",
                column: "tag_id");
            
            migrationBuilder.Sql(AddProcedureQuery);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog");

            migrationBuilder.DropTable(
                name: "post_tag_mapping");

            migrationBuilder.DropTable(
                name: "post");

            migrationBuilder.DropTable(
                name: "tag");
            
            migrationBuilder.Sql("DROP PROCEDURE usp_get_blog");
        }
    }
}
