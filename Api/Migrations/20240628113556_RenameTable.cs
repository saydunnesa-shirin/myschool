using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicSessionTemplates_Institutions_InstitutionId",
                table: "AcademicSessionTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicSessionTemplates",
                table: "AcademicSessionTemplates");

            migrationBuilder.RenameTable(
                name: "AcademicSessionTemplates",
                newName: "AcademicClassTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicSessionTemplates_InstitutionId",
                table: "AcademicClassTemplate",
                newName: "IX_AcademicClassTemplate_InstitutionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicClassTemplate",
                table: "AcademicClassTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicClassTemplate_Institutions_InstitutionId",
                table: "AcademicClassTemplate",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicClassTemplate_Institutions_InstitutionId",
                table: "AcademicClassTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicClassTemplate",
                table: "AcademicClassTemplate");

            migrationBuilder.RenameTable(
                name: "AcademicClassTemplate",
                newName: "AcademicSessionTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicClassTemplate_InstitutionId",
                table: "AcademicSessionTemplates",
                newName: "IX_AcademicSessionTemplates_InstitutionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicSessionTemplates",
                table: "AcademicSessionTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicSessionTemplates_Institutions_InstitutionId",
                table: "AcademicSessionTemplates",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }
    }
}
