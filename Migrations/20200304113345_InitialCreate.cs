using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TrainingNumber = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainingStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrainingEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.UniqueConstraint("AK_Trainings_TrainingNumber", x => x.TrainingNumber);
                    table.ForeignKey(
                        name: "FK_Trainings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainingExercises",
                columns: table => new
                {
                    ExerciseID = table.Column<Guid>(nullable: false),
                    TrainingId = table.Column<Guid>(nullable: false),
                    Series = table.Column<int>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercises", x => new { x.TrainingId, x.ExerciseID });
                    table.ForeignKey(
                        name: "FK_TrainingExercises_Exercises_ExerciseID",
                        column: x => x.ExerciseID,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingExercises_Trainings_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Trainings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"), "Klata" });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "TrainingEnd", "TrainingNumber", "TrainingStart", "UserId" },
                values: new object[] { new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"), new DateTime(2020, 1, 1, 12, 23, 48, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 1, 1, 11, 23, 44, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[] { new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"), new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"), "Wyciskanie na ławce płaskiej" });

            migrationBuilder.InsertData(
                table: "TrainingExercises",
                columns: new[] { "TrainingId", "ExerciseID", "Reps", "Series", "Weight" },
                values: new object[] { new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"), new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"), 12, 4, 60f });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_ExerciseID",
                table: "TrainingExercises",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_UserId",
                table: "Trainings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingExercises");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
