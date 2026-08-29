using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientRelativeFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Relation",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativeAddress",
                table: "Patients",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativeName",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelativePhone",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Relation",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RelativeAddress",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RelativeName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "RelativePhone",
                table: "Patients");
        }
    }
}
