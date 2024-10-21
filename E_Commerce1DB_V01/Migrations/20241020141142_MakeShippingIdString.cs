using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class MakeShippingIdString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint from the "Carts" table
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts");

            // Drop the primary key constraint from the "ShippingMethods" table
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods");

            // Drop the old "Id" column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShippingMethods");

            // Add the new "Id" column
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "ShippingMethods",
                type: "nvarchar(450)",
                nullable: false);

            // Recreate the primary key constraint on the new "Id" column
            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods",
                column: "Id");

            // Modify the related column in the "Carts" table
            migrationBuilder.AlterColumn<string>(
                name: "ShippingMethodID",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            // Recreate the foreign key constraint with the updated "Id" column
            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts",
                column: "ShippingMethodID",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop the foreign key constraint from the "Carts" table
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts");

            // Drop the primary key constraint from the "ShippingMethods" table
            migrationBuilder.DropPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods");

            // Drop the new "Id" column
            migrationBuilder.DropColumn(
                name: "Id",
                table: "ShippingMethods");

            // Recreate the original "Id" column with the identity specification
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ShippingMethods",
                type: "int",
                nullable: false)
                .Annotation("SqlServer:Identity", "1, 1");

            // Recreate the primary key on the original "Id" column
            migrationBuilder.AddPrimaryKey(
                name: "PK_ShippingMethods",
                table: "ShippingMethods",
                column: "Id");

            // Revert changes to the "Carts" table
            migrationBuilder.AlterColumn<int>(
                name: "ShippingMethodID",
                table: "Carts",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            // Recreate the original foreign key constraint
            migrationBuilder.AddForeignKey(
                name: "FK_Carts_ShippingMethods_ShippingMethodID",
                table: "Carts",
                column: "ShippingMethodID",
                principalTable: "ShippingMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
