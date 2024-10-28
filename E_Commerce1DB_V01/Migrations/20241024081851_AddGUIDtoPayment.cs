using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class AddGUIDtoPayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PaymentLog_Payments_PaymentId",
                table: "PaymentLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PaymentLog",
                table: "PaymentLog");

            migrationBuilder.RenameTable(
                name: "PaymentLog",
                newName: "paymentLogs");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentLog_PaymentId",
                table: "paymentLogs",
                newName: "IX_paymentLogs_PaymentId");

            migrationBuilder.AddColumn<string>(
                name: "UniqueIdentifier",
                table: "Payments",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_paymentLogs",
                table: "paymentLogs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_paymentLogs_Payments_PaymentId",
                table: "paymentLogs",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_paymentLogs_Payments_PaymentId",
                table: "paymentLogs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_paymentLogs",
                table: "paymentLogs");

            migrationBuilder.DropColumn(
                name: "UniqueIdentifier",
                table: "Payments");

            migrationBuilder.RenameTable(
                name: "paymentLogs",
                newName: "PaymentLog");

            migrationBuilder.RenameIndex(
                name: "IX_paymentLogs_PaymentId",
                table: "PaymentLog",
                newName: "IX_PaymentLog_PaymentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PaymentLog",
                table: "PaymentLog",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentLog_Payments_PaymentId",
                table: "PaymentLog",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
