using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddEmergencyDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmergencyBp",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyCondition",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyDoctorsNote",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyInvestigations",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyMedicalOfficer",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyNote",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPastHistory",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyPulse",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyRr",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencySpo2",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyStateInjection",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmergencyTemp",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsEmergency",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmergencyBp",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyCondition",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyDoctorsNote",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyInvestigations",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyMedicalOfficer",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyNote",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyPastHistory",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyPulse",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyRr",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencySpo2",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyStateInjection",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "EmergencyTemp",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "IsEmergency",
                table: "Patients");
        }
    }
}
