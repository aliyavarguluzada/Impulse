using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impulse.Migrations
{
    /// <inheritdoc />
    public partial class ForeignStatusId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Vacancies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Vacancies_StatusId",
                table: "Vacancies",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_Statuses_StatusId",
                table: "Vacancies",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_Statuses_StatusId",
                table: "Vacancies");

            migrationBuilder.DropIndex(
                name: "IX_Vacancies_StatusId",
                table: "Vacancies");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Vacancies");
        }
    }
}
