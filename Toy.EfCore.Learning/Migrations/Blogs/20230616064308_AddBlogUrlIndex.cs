using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Toy.EfCore.Learning.Migrations.Blogs
{
    public partial class AddBlogUrlIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "blog",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_blog_url",
                table: "blog",
                column: "url",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_blog_url",
                table: "blog");

            migrationBuilder.AlterColumn<string>(
                name: "url",
                table: "blog",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
