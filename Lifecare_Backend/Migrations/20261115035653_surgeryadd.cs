using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class surgeryadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSurgery",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "SurgeryPastHistory",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurgeryPostOpStatus",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurgeryPreOpStatus",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurgeryRiskNote",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SurgeryType",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSurgery",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SurgeryPastHistory",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SurgeryPostOpStatus",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SurgeryPreOpStatus",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SurgeryRiskNote",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "SurgeryType",
                table: "Patients");
        }
    }
}
