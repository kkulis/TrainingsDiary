using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class Initial : Migration
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
                    TrainingTime = table.Column<TimeSpan>(nullable: false),
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
                    Id = table.Column<Guid>(nullable: false),
                    ExerciseID = table.Column<Guid>(nullable: false),
                    TrainingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingExercises", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Reps = table.Column<int>(nullable: false),
                    Weight = table.Column<float>(nullable: false),
                    ExerciseTrainingId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Series_TrainingExercises_ExerciseTrainingId",
                        column: x => x.ExerciseTrainingId,
                        principalTable: "TrainingExercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"), "Klata" },
                    { new Guid("cf36d573-0160-4252-ab78-b12805ae9c07"), "Plecy" },
                    { new Guid("f52c961d-bd06-4e33-9adf-67f587ccaabe"), "Nogi" }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: new[] { "Id", "TrainingEnd", "TrainingNumber", "TrainingStart", "TrainingTime", "UserId" },
                values: new object[] { new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"), new DateTime(2020, 1, 1, 12, 23, 48, 0, DateTimeKind.Unspecified), 1, new DateTime(2020, 1, 1, 11, 23, 44, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0), null });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"), new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"), "Wyciskanie sztangi na ławce płaskiej" },
                    { new Guid("12f1974d-8ebc-4caa-8d34-7d350b7af440"), new Guid("63cfba80-9041-4994-bbc9-9f0f28b51388"), "Wyciskanie hantli na ławce płaskiej" },
                    { new Guid("170b139a-b929-40de-9644-c590b0507819"), new Guid("cf36d573-0160-4252-ab78-b12805ae9c07"), "Podciąganie" },
                    { new Guid("d4ad5722-7a0b-4a7a-976d-2772e5daa0b2"), new Guid("cf36d573-0160-4252-ab78-b12805ae9c07"), "przyciąganie wyciągu do klatki" },
                    { new Guid("c7f43e99-e859-43ce-b6ad-3dcac667a729"), new Guid("f52c961d-bd06-4e33-9adf-67f587ccaabe"), "Przysiad" }
                });

            migrationBuilder.InsertData(
                table: "TrainingExercises",
                columns: new[] { "Id", "ExerciseID", "TrainingId" },
                values: new object[] { new Guid("94f9ca0f-2d3c-4c10-98b4-9f81ac9ee7c3"), new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"), new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5") });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "ExerciseTrainingId", "Reps", "Weight" },
                values: new object[] { new Guid("2573adeb-2aaa-43a6-830a-baecb6586a4d"), new Guid("94f9ca0f-2d3c-4c10-98b4-9f81ac9ee7c3"), 12, 60f });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_CategoryId",
                table: "Exercises",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ExerciseTrainingId",
                table: "Series",
                column: "ExerciseTrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_ExerciseID",
                table: "TrainingExercises",
                column: "ExerciseID");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_TrainingId",
                table: "TrainingExercises",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_UserId",
                table: "Trainings",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Series");

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
