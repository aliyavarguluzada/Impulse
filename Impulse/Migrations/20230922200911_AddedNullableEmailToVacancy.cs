using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Impulse.Migrations
{
    /// <inheritdoc />
    public partial class AddedNullableEmailToVacancy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vacancies",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vacancies");
        }
    }
}
