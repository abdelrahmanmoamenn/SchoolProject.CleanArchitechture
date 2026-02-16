using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolProject.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithSeedingAndConfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "subjects",
                columns: table => new
                {
                    SubID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubjectNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    SubjectNameEn = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Period = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_subjects", x => x.SubID);
                });

            migrationBuilder.CreateTable(
                name: "departments",
                columns: table => new
                {
                    DID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DNameAr = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DNameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    InsManager = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departments", x => x.DID);
                });

            migrationBuilder.CreateTable(
                name: "departmentSubjects",
                columns: table => new
                {
                    DID = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departmentSubjects", x => new { x.SubID, x.DID });
                    table.ForeignKey(
                        name: "FK_departmentSubjects_departments_DID",
                        column: x => x.DID,
                        principalTable: "departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_departmentSubjects_subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "instructors",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ENameAr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ENameEn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Position = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_instructors", x => x.InsId);
                    table.ForeignKey(
                        name: "FK_instructors_departments_DID",
                        column: x => x.DID,
                        principalTable: "departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_instructors_instructors_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    StudID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameAr = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    NameEn = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.StudID);
                    table.ForeignKey(
                        name: "FK_students_departments_DID",
                        column: x => x.DID,
                        principalTable: "departments",
                        principalColumn: "DID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ins_Subjects",
                columns: table => new
                {
                    InsId = table.Column<int>(type: "int", nullable: false),
                    SubId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ins_Subjects", x => new { x.SubId, x.InsId });
                    table.ForeignKey(
                        name: "FK_ins_Subjects_instructors_InsId",
                        column: x => x.InsId,
                        principalTable: "instructors",
                        principalColumn: "InsId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ins_Subjects_subjects_SubId",
                        column: x => x.SubId,
                        principalTable: "subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "studentSubjects",
                columns: table => new
                {
                    StudID = table.Column<int>(type: "int", nullable: false),
                    SubID = table.Column<int>(type: "int", nullable: false),
                    grade = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_studentSubjects", x => new { x.SubID, x.StudID });
                    table.ForeignKey(
                        name: "FK_studentSubjects_students_StudID",
                        column: x => x.StudID,
                        principalTable: "students",
                        principalColumn: "StudID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_studentSubjects_subjects_SubID",
                        column: x => x.SubID,
                        principalTable: "subjects",
                        principalColumn: "SubID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "departments",
                columns: new[] { "DID", "DNameAr", "DNameEn", "InsManager" },
                values: new object[,]
                {
                    { 1, "علوم الحاسب", "Computer Science", null },
                    { 2, "الهندسة", "Engineering", null },
                    { 3, "العلوم", "Science", null }
                });

            migrationBuilder.InsertData(
                table: "subjects",
                columns: new[] { "SubID", "Period", "SubjectNameAr", "SubjectNameEn" },
                values: new object[,]
                {
                    { 1, 60, "البرمجة بلغة C#", "C# Programming" },
                    { 2, 45, "قواعد البيانات", "Databases" },
                    { 3, 50, "الويب", "Web Development" },
                    { 4, 40, "الفيزياء", "Physics" },
                    { 5, 40, "الكيمياء", "Chemistry" }
                });

            migrationBuilder.InsertData(
                table: "departmentSubjects",
                columns: new[] { "DID", "SubID" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 1, 2 },
                    { 2, 2 },
                    { 1, 3 },
                    { 3, 4 },
                    { 3, 5 }
                });

            migrationBuilder.InsertData(
                table: "instructors",
                columns: new[] { "InsId", "Address", "DID", "ENameAr", "ENameEn", "Image", "Position", "Salary", "SupervisorId" },
                values: new object[,]
                {
                    { 1, "Cairo", 1, "أحمد علي", "Ahmed Ali", null, "Senior Instructor", 5000m, null },
                    { 4, "Cairo", 3, "سارة إبراهيم", "Sarah Ibrahim", null, "Senior Instructor", 5000m, null }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "StudID", "Address", "DID", "NameAr", "NameEn", "Phone" },
                values: new object[,]
                {
                    { 1, "Cairo", 1, "علي محمد", "Ali Muhammad", "01012345678" },
                    { 2, "Giza", 1, "سارة أحمد", "Sara Ahmed", "01198765432" },
                    { 3, "Alexandria", 2, "محمود علي", "Mahmoud Ali", "01556789012" },
                    { 4, "Mansoura", 3, "فاطمة حسن", "Fatima Hassan", "01667890123" },
                    { 5, "Cairo", 1, "إبراهيم سالم", "Ibrahim Salem", "01778901234" }
                });

            migrationBuilder.InsertData(
                table: "ins_Subjects",
                columns: new[] { "InsId", "SubId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 4, 4 },
                    { 4, 5 }
                });

            migrationBuilder.InsertData(
                table: "instructors",
                columns: new[] { "InsId", "Address", "DID", "ENameAr", "ENameEn", "Image", "Position", "Salary", "SupervisorId" },
                values: new object[,]
                {
                    { 2, "Giza", 1, "فاطمة محمد", "Fatima Muhammad", null, "Instructor", 4000m, 1 },
                    { 3, "Alexandria", 2, "محمود حسن", "Mahmoud Hassan", null, "Instructor", 4000m, 1 }
                });

            migrationBuilder.InsertData(
                table: "studentSubjects",
                columns: new[] { "StudID", "SubID", "grade" },
                values: new object[,]
                {
                    { 1, 1, 85.5m },
                    { 2, 1, 78.0m },
                    { 3, 1, 92.0m },
                    { 5, 1, 88.0m },
                    { 1, 2, 90.0m },
                    { 3, 2, 86.0m },
                    { 2, 3, 88.5m },
                    { 5, 3, 85.0m },
                    { 4, 4, 81.0m },
                    { 4, 5, 79.5m }
                });

            migrationBuilder.InsertData(
                table: "ins_Subjects",
                columns: new[] { "InsId", "SubId" },
                values: new object[,]
                {
                    { 3, 1 },
                    { 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_departments_InsManager",
                table: "departments",
                column: "InsManager",
                unique: true,
                filter: "[InsManager] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_departmentSubjects_DID",
                table: "departmentSubjects",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_ins_Subjects_InsId",
                table: "ins_Subjects",
                column: "InsId");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_DID",
                table: "instructors",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_instructors_SupervisorId",
                table: "instructors",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_students_DID",
                table: "students",
                column: "DID");

            migrationBuilder.CreateIndex(
                name: "IX_studentSubjects_StudID",
                table: "studentSubjects",
                column: "StudID");

            migrationBuilder.AddForeignKey(
                name: "FK_departments_instructors_InsManager",
                table: "departments",
                column: "InsManager",
                principalTable: "instructors",
                principalColumn: "InsId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_departments_instructors_InsManager",
                table: "departments");

            migrationBuilder.DropTable(
                name: "departmentSubjects");

            migrationBuilder.DropTable(
                name: "ins_Subjects");

            migrationBuilder.DropTable(
                name: "studentSubjects");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "subjects");

            migrationBuilder.DropTable(
                name: "instructors");

            migrationBuilder.DropTable(
                name: "departments");
        }
    }
}
