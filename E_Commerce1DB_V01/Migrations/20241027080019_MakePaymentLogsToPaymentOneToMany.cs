using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce1DB_V01.Migrations
{
    /// <inheritdoc />
    public partial class MakePaymentLogsToPaymentOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_paymentLogs_PaymentId",
                table: "paymentLogs");

            migrationBuilder.CreateIndex(
                name: "IX_paymentLogs_PaymentId",
                table: "paymentLogs",
                column: "PaymentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_paymentLogs_PaymentId",
                table: "paymentLogs");

            migrationBuilder.CreateIndex(
                name: "IX_paymentLogs_PaymentId",
                table: "paymentLogs",
                column: "PaymentId",
                unique: true);
        }
    }
}
