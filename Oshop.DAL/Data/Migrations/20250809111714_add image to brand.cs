using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Oshop.DAL.Data.Migrations
{
    /// <inheritdoc />
    public partial class addimagetobrand : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "image",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "image",
                table: "Brands");
        }
    }
}
