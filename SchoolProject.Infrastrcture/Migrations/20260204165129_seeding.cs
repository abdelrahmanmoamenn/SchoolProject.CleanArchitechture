using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "DID", "DName" },
                values: new object[] { 1, "Computer Science" });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "StudID", "Address", "DID", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Cairo", 1, "Ahmed Mohamed", "01012345678" },
                    { 2, "Giza", 1, "Sara Ali", "01198765432" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "StudID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "departments",
                keyColumn: "DID",
                keyValue: 1);
        }
    }
}
