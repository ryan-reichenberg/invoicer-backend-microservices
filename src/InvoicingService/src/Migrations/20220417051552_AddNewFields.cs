using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InvoicingService.Migrations
{
    public partial class AddNewFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvoicedFrom",
                table: "Invoice",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "InvoicedTo",
                table: "Invoice",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvoicedFrom",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "InvoicedTo",
                table: "Invoice");
        }
    }
}
