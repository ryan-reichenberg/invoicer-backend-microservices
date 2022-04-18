using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InvoicingService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Invoice",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BillTo_BusinessName = table.Column<string>(type: "text", nullable: true),
                    BillTo_Name = table.Column<string>(type: "text", nullable: true),
                    BillTo_StreetAddress = table.Column<string>(type: "text", nullable: true),
                    BillTo_PostalCode = table.Column<string>(type: "text", nullable: true),
                    BillTo_City = table.Column<string>(type: "text", nullable: true),
                    BillTo_MobileNumber = table.Column<string>(type: "text", nullable: true),
                    BillTo_EmailAddress = table.Column<string>(type: "text", nullable: true),
                    BillFrom_BusinessName = table.Column<string>(type: "text", nullable: true),
                    BillFrom_Name = table.Column<string>(type: "text", nullable: true),
                    BillFrom_StreetAddress = table.Column<string>(type: "text", nullable: true),
                    BillFrom_PostalCode = table.Column<string>(type: "text", nullable: true),
                    BillFrom_City = table.Column<string>(type: "text", nullable: true),
                    BillFrom_MobileNumber = table.Column<string>(type: "text", nullable: true),
                    BillFrom_EmailAddress = table.Column<string>(type: "text", nullable: true),
                    IssuedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DueDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Recurring = table.Column<bool>(type: "boolean", nullable: false),
                    TaxRate = table.Column<double>(type: "double precision", nullable: false),
                    Tax = table.Column<decimal>(type: "numeric", nullable: false),
                    SubTotal = table.Column<decimal>(type: "numeric", nullable: false),
                    AdditionalNotes = table.Column<string>(type: "text", nullable: true),
                    ProjectId = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LineItemDto",
                columns: table => new
                {
                    InvoiceDtoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Quantity = table.Column<int>(type: "integer", nullable: true),
                    Price = table.Column<decimal>(type: "numeric", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LineItemDto", x => new { x.InvoiceDtoId, x.Id });
                    table.ForeignKey(
                        name: "FK_LineItemDto_Invoice_InvoiceDtoId",
                        column: x => x.InvoiceDtoId,
                        principalTable: "Invoice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LineItemDto");

            migrationBuilder.DropTable(
                name: "Invoice");
        }
    }
}
