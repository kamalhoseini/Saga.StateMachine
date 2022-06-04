using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrderService.Infrastructure.Persistence.Migrations
{
    public partial class SetOrderStateconfigasfluentapi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "OrderStates");

            migrationBuilder.AddColumn<int>(
                name: "ConcurrencyToken",
                table: "OrderStates",
                type: "integer",
                rowVersion: true,
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_OrderStates_ConcurrencyToken",
                table: "OrderStates",
                column: "ConcurrencyToken");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_OrderStates_ConcurrencyToken",
                table: "OrderStates");

            migrationBuilder.DropColumn(
                name: "ConcurrencyToken",
                table: "OrderStates");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "OrderStates",
                type: "bytea",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
