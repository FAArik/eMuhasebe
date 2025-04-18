﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class renaming_cashregister_colums : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CashRegisterDetailOppositeId",
                table: "CashRegisterDetails",
                newName: "CashRegisterDetailId");

            migrationBuilder.AddColumn<Guid>(
                name: "BankDetailId",
                table: "CashRegisterDetails",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankDetailId",
                table: "CashRegisterDetails");

            migrationBuilder.RenameColumn(
                name: "CashRegisterDetailId",
                table: "CashRegisterDetails",
                newName: "CashRegisterDetailOppositeId");
        }
    }
}
