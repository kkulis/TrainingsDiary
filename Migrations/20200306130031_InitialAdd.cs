using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainingDiary.Migrations
{
    public partial class InitialAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises");

            migrationBuilder.DeleteData(
                table: "TrainingExercises",
                keyColumns: new[] { "TrainingId", "ExerciseID" },
                keyValues: new object[] { new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"), new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca") });

            migrationBuilder.DropColumn(
                name: "Reps",
                table: "TrainingExercises");

            migrationBuilder.DropColumn(
                name: "Series",
                table: "TrainingExercises");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "TrainingExercises");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TrainingExercises",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises",
                column: "Id");

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
                table: "TrainingExercises",
                columns: new[] { "Id", "ExerciseID", "TrainingId" },
                values: new object[] { new Guid("94f9ca0f-2d3c-4c10-98b4-9f81ac9ee7c3"), new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca"), new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5") });

            migrationBuilder.InsertData(
                table: "Series",
                columns: new[] { "Id", "ExerciseTrainingId", "Reps", "Weight" },
                values: new object[] { new Guid("2573adeb-2aaa-43a6-830a-baecb6586a4d"), new Guid("94f9ca0f-2d3c-4c10-98b4-9f81ac9ee7c3"), 12, 60f });

            migrationBuilder.CreateIndex(
                name: "IX_TrainingExercises_TrainingId",
                table: "TrainingExercises",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_ExerciseTrainingId",
                table: "Series",
                column: "ExerciseTrainingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises");

            migrationBuilder.DropIndex(
                name: "IX_TrainingExercises_TrainingId",
                table: "TrainingExercises");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TrainingExercises");

            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "TrainingExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Series",
                table: "TrainingExercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "TrainingExercises",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrainingExercises",
                table: "TrainingExercises",
                columns: new[] { "TrainingId", "ExerciseID" });

            migrationBuilder.UpdateData(
                table: "TrainingExercises",
                keyColumns: new[] { "TrainingId", "ExerciseID" },
                keyValues: new object[] { new Guid("6be892a5-12ca-493d-bb74-4ef5b9175bf5"), new Guid("38b381c8-fd1f-408c-ad25-6401fd6f40ca") },
                columns: new[] { "Reps", "Series", "Weight" },
                values: new object[] { 12, 4, 60f });
        }
    }
}
