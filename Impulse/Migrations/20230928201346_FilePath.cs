using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impulse.Migrations
{
    /// <inheritdoc />
    public partial class FilePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Vacancies",
                newName: "LogoFilePath");

            migrationBuilder.AddColumn<int>(
                name: "ExperienceId",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Experiences_ExperienceId",
                table: "Vacancies",
                column: "ExperienceId",
                principalTable: "Experiences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Experiences_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_ExperienceId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "ExperienceId",
                table: "Vacancies");

            migrationBuilder.RenameColumn(
                name: "LogoFilePath",
                table: "Vacancies",
                newName: "Experience");
        }
    }
}
