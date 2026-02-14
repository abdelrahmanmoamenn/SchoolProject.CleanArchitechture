using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SchoolProject.Infrastrcture.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SubjectName",
                table: "subjects",
                newName: "SubjectNameEn");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "students",
                newName: "NameEn");

            migrationBuilder.RenameColumn(
                name: "DName",
                table: "departments",
                newName: "DNameEn");

            migrationBuilder.AddColumn<string>(
                name: "SubjectNameAr",
                table: "subjects",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NameAr",
                table: "students",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DNameAr",
                table: "departments",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "departments",
                keyColumn: "DID",
                keyValue: 1,
                column: "DNameAr",
                value: "علوم الحاسب");

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "StudID",
                keyValue: 1,
                column: "NameAr",
                value: "احمد محمد");

            migrationBuilder.UpdateData(
                table: "students",
                keyColumn: "StudID",
                keyValue: 2,
                column: "NameAr",
                value: "سارة علي");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubjectNameAr",
                table: "subjects");

            migrationBuilder.DropColumn(
                name: "NameAr",
                table: "students");

            migrationBuilder.DropColumn(
                name: "DNameAr",
                table: "departments");

            migrationBuilder.RenameColumn(
                name: "SubjectNameEn",
                table: "subjects",
                newName: "SubjectName");

            migrationBuilder.RenameColumn(
                name: "NameEn",
                table: "students",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "DNameEn",
                table: "departments",
                newName: "DName");
        }
    }
}
