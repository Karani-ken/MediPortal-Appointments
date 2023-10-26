using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediPortal_Appointment.Migrations
{
    /// <inheritdoc />
    public partial class updatetable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Symptomps",
                table: "Appointments",
                newName: "Symptoms");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentStatus",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentStatus",
                table: "Appointments");

            migrationBuilder.RenameColumn(
                name: "Symptoms",
                table: "Appointments",
                newName: "Symptomps");
        }
    }
}
