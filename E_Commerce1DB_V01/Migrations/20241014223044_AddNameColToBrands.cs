using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class AddNameColToBrands : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Brands");
        }
    }
}
