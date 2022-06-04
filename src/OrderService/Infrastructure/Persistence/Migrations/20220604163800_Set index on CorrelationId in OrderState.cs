using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Persistence.Migrations
{
    public partial class SetindexonCorrelationIdinOrderState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStates_ConcurrencyToken",
                table: "OrderStates");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStates_CorrelationId",
                table: "OrderStates",
                column: "CorrelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStates_CorrelationId",
                table: "OrderStates");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStates_ConcurrencyToken",
                table: "OrderStates",
                column: "ConcurrencyToken");
        }
    }
}
