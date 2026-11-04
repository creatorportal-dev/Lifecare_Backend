using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class AddMlcPoliceDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MlcFirOrCcNumber",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcPermanentAddress",
                table: "Patients",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcPoliceInspectorName",
                table: "Patients",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcPoliceStationAddress",
                table: "Patients",
                type: "nvarchar(300)",
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcReportedPoliceStation",
                table: "Patients",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MlcFirOrCcNumber",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcPermanentAddress",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcPoliceInspectorName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcPoliceStationAddress",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcReportedPoliceStation",
                table: "Patients");
        }
    }
}
