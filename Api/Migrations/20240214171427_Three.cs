using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class Three : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_InstitutionId",
                table: "Employees",
                column: "InstitutionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Institutions_InstitutionId",
                table: "Employees",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Institutions_InstitutionId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_InstitutionId",
                table: "Employees");
        }
    }
}
