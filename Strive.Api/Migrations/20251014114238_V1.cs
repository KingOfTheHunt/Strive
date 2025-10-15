using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Strive.Api.Migrations
{
    /// <inheritdoc />
    public partial class V1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(50)", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(200)", nullable: false),
                    Category = table.Column<string>(type: "VARCHAR(15)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exercises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "NVARCHAR(20)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    VerificationCode = table.Column<string>(type: "CHAR(6)", nullable: false),
                    VerificationCodeExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    VerificationVerifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PasswordHash = table.Column<string>(type: "VARCHAR(100)", nullable: false),
                    PasswordResetCode = table.Column<string>(type: "CHAR(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Workouts",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutExercises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkoutId = table.Column<int>(type: "int", nullable: false),
                    ExerciseId = table.Column<int>(type: "int", nullable: false),
                    Sets = table.Column<byte>(type: "TINYINT", nullable: false),
                    Repetitions = table.Column<byte>(type: "TINYINT", nullable: true),
                    Weight = table.Column<float>(type: "REAL", nullable: true),
                    Duration = table.Column<int>(type: "INT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutExercises", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Exercises_WorkoutExercises",
                        column: x => x.ExerciseId,
                        principalTable: "Exercises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Workouts_WorkoutsExercises",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Commentary = table.Column<string>(type: "NVARCHAR(200)", nullable: true),
                    ScheduledAt = table.Column<DateTime>(type: "DATETIME2", nullable: true),
                    Done = table.Column<bool>(type: "BIT", nullable: false, defaultValue: false),
                    WorkoutId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_WorkoutSessions",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Exercises",
                columns: new[] { "Id", "Category", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Cardio", "High-intensity activity that improves cardiovascular capacity and quickly burns calories.", "Running" },
                    { 2, "Cardio", "Aerobic exercise involving opening and closing arms and legs in sync, stimulating the heart and lungs.", "Jumping Jacks" },
                    { 3, "Strength", "Knee and hip flexion movement that strengthens legs and glutes.", "Squat" },
                    { 4, "Strength", "Exercise that uses body weight to strengthen chest, triceps, and shoulders.", "Push-up" },
                    { 5, "Strength", "Isometric position that activates abs, lower back, and shoulders, strengthening the core.", "Plank" },
                    { 6, "Strength", "Step forward with knee flexion, strengthening legs and glutes.", "Lunge" },
                    { 7, "Flexibility", "Bending the torso forward to stretch the back of the thighs.", "Hamstring Stretch" },
                    { 8, "Flexibility", "Pulling the arm across the chest to stretch deltoids and trapezius.", "Shoulder Stretch" },
                    { 9, "Flexibility", "Sequence of spinal flexion and extension movements that improve mobility and relaxation.", "Cat-Cow Pose" },
                    { 10, "Cardio", "A variation of jumping jacks combined with squats, working both cardiovascular endurance and leg strength.", "Squat Jack" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_Name",
                table: "Exercises",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "email");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_ExerciseId",
                table: "WorkoutExercises",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutExercises_WorkoutId_ExerciseId",
                table: "WorkoutExercises",
                columns: new[] { "WorkoutId", "ExerciseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_UserId",
                table: "Workouts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutSessions_WorkoutId",
                table: "WorkoutSessions",
                column: "WorkoutId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutExercises");

            migrationBuilder.DropTable(
                name: "WorkoutSessions");

            migrationBuilder.DropTable(
                name: "Exercises");

            migrationBuilder.DropTable(
                name: "Workouts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
