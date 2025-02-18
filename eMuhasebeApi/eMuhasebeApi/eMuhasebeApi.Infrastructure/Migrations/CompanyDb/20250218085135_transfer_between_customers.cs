using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class transfer_between_customers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CashRegisterDetailId",
                table: "CustomerDetails",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashRegisterDetailId",
                table: "CustomerDetails");

            migrationBuilder.DropColumn(
                name: "CustomerDetailId",
                table: "CashRegisterDetails");
        }
    }
}
