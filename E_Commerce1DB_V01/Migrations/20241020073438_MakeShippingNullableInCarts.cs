using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class MakeShippingNullableInCarts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingMethodID",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts",
                column: "ShippingMethodID",
                principalTable: "ShippingMethods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts");

            migrationBuilder.AlterColumn<int>(
                name: "ShippingMethodID",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts",
                column: "ShippingMethodID",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
