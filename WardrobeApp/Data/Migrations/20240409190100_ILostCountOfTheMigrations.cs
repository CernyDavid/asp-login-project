using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WardrobeApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ILostCountOfTheMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nick",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nick",
                table: "AspNetUsers");
        }
    }
}
