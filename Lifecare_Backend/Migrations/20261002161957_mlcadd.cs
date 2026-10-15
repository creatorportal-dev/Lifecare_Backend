using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lifecare_Backend.Migrations
{
    /// <inheritdoc />
    public partial class mlcadd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMlc",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MlcAnyOfHospitalisedPastHO",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcCauseOfMatter",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcCurrentCondition",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcDateOfMatter",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcDetailOfMatter",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcDrIncharge",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcFillerMoNo",
                table: "Patients",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcMedicalOfficer",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcNameOfFiller",
                table: "Patients",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcPlaceOfMatter",
                table: "Patients",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MlcTimeOfMatter",
                table: "Patients",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMlc",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcAnyOfHospitalisedPastHO",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcCauseOfMatter",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcCurrentCondition",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcDateOfMatter",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcDetailOfMatter",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcDrIncharge",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcFillerMoNo",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcMedicalOfficer",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcNameOfFiller",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcPlaceOfMatter",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MlcTimeOfMatter",
                table: "Patients");
        }
    }
}
