using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations.CompanyDb
{
    /// <inheritdoc />
    public partial class product_detail_price_col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ProductDetails",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ProductDetails");
        }
    }
}
