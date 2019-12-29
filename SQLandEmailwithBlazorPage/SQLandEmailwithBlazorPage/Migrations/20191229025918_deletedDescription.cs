using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLandEmailwithBlazorPage.Migrations
{
    public partial class deletedDescription : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "People");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "People",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
