using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientWardFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ward",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WardNumber",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ward",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "WardNumber",
                table: "Patients");
        }
    }
}
