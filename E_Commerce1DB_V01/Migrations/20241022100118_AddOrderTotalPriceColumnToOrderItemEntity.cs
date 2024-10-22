using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderTotalPriceColumnToOrderItemEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.AddColumn<decimal>(
                name: "OrderTotalPrice",
                table: "OrderItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.DropColumn(
                name: "OrderTotalPrice",
                table: "OrderItems");


        }
    }
}
