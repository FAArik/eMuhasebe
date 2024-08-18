using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class refactoring_foreign_keys_for_tables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashRegisterDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.CreateTable(
                name: "BankDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositAmount = table.Column<decimal>(type: "money", nullable: false),
                    WithdrawalAmount = table.Column<decimal>(type: "money", nullable: false),
                    BankDetailOppositeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankDetails_BankDetails_BankDetailOppositeId",
                        column: x => x.BankDetailOppositeId,
                        principalTable: "BankDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BankDetails_Banks_BankId",
                        column: x => x.BankId,
                        principalTable: "Banks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankDetailOppositeId",
                table: "BankDetails",
                column: "BankDetailOppositeId");

            migrationBuilder.CreateIndex(
                name: "IX_BankDetails_BankId",
                table: "BankDetails",
                column: "BankId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);
        }
    }
}
