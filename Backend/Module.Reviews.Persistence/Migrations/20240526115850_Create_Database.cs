using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Module.Reviews.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Create_Database : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "reviews");

            migrationBuilder.CreateTable(
                name: "Reviews",
                schema: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    RevieweeId = table.Column<int>(type: "int", nullable: false),
                    RequestorId = table.Column<int>(type: "int", nullable: false),
                    ReviewerId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReviewResults",
                schema: "reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReviewResults_Reviews_Id",
                        column: x => x.Id,
                        principalSchema: "reviews",
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReviewTasks",
                schema: "reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(type: "int", nullable: false),
                    TaskId = table.Column<int>(type: "int", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewTasks", x => new { x.ReviewId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_ReviewTasks_Reviews_ReviewId",
                        column: x => x.ReviewId,
                        principalSchema: "reviews",
                        principalTable: "Reviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewResults",
                schema: "reviews");

            migrationBuilder.DropTable(
                name: "ReviewTasks",
                schema: "reviews");

            migrationBuilder.DropTable(
                name: "Reviews",
                schema: "reviews");
        }
    }
}
