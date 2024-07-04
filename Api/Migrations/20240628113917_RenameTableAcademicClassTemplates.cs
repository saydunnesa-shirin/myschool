using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class RenameTableAcademicClassTemplates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicClassTemplate_Institutions_InstitutionId",
                table: "AcademicClassTemplate");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicClassTemplate",
                table: "AcademicClassTemplate");

            migrationBuilder.RenameTable(
                name: "AcademicClassTemplate",
                newName: "AcademicClassTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicClassTemplate_InstitutionId",
                table: "AcademicClassTemplates",
                newName: "IX_AcademicClassTemplates_InstitutionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicClassTemplates",
                table: "AcademicClassTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicClassTemplates_Institutions_InstitutionId",
                table: "AcademicClassTemplates",
                column: "InstitutionId",
                principalTable: "Institutions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicClassTemplates_Institutions_InstitutionId",
                table: "AcademicClassTemplates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicClassTemplates",
                table: "AcademicClassTemplates");

            migrationBuilder.RenameTable(
                name: "AcademicClassTemplates",
                newName: "AcademicClassTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicClassTemplates_InstitutionId",
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
    }
}
