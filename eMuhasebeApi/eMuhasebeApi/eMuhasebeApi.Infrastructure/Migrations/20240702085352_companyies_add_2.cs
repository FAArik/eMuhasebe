using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eMuhasebeApi.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class companyies_add_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Companies",
                schema: "muh",
                newName: "Companies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "muh");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "Companies",
                newSchema: "muh");
        }
    }
}
