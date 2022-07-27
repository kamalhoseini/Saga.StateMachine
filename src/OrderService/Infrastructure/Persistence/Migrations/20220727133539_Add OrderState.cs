using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Persistence.Migrations
{
    public partial class AddOrderState : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderStates",
                columns: table => new
                {
                    CorrelationId = table.Column<Guid>(type: "uuid", nullable: false),
                    CurrentState = table.Column<string>(type: "text", nullable: false),
                    OrderId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Price = table.Column<int>(type: "integer", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ConcurrencyToken = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStates", x => x.CorrelationId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderStates_CorrelationId",
                table: "OrderStates",
                column: "CorrelationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderStates");
        }
    }
}
