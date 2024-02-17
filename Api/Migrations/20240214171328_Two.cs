using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Two : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Institutions_CountryId",
                table: "Institutions",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Institutions_Countries_CountryId",
                table: "Institutions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Institutions_Countries_CountryId",
                table: "Institutions");

            migrationBuilder.DropIndex(
                name: "IX_Institutions_CountryId",
                table: "Institutions");
        }
    }
}
