using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTemplateIdToAcademicClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "AcademicClasses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AcademicClasses_TemplateId",
                table: "AcademicClasses",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicClasses_AcademicClassTemplates_TemplateId",
                table: "AcademicClasses",
                column: "TemplateId",
                principalTable: "AcademicClassTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicClasses_AcademicClassTemplates_TemplateId",
                table: "AcademicClasses");

            migrationBuilder.DropIndex(
                name: "IX_AcademicClasses_TemplateId",
                table: "AcademicClasses");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "AcademicClasses");
        }
    }
}
