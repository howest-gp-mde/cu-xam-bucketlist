using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XrnCourse.BucketList.WebApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bucketlists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(maxLength: 30, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    IsFavorite = table.Column<bool>(nullable: false),
                    OwnerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bucketlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bucketlists_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BucketlistItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    BucketId = table.Column<Guid>(nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 50, nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BucketlistItems_Bucketlists_BucketId",
                        column: x => x.BucketId,
                        principalTable: "Bucketlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "siegfried@bucketlists.test", "Siegfried" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "UserName" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000002"), "deidre@bucketlists.test", "Deidre" });

            migrationBuilder.InsertData(
                table: "Bucketlists",
                columns: new[] { "Id", "Description", "ImageUrl", "IsFavorite", "OwnerId", "Title" },
                values: new object[] { new Guid("11111111-0000-0000-0000-000000000001"), "Advancing my programming skills", null, true, new Guid("00000000-0000-0000-0000-000000000001"), "Programming Dreams" });

            migrationBuilder.InsertData(
                table: "Bucketlists",
                columns: new[] { "Id", "Description", "ImageUrl", "IsFavorite", "OwnerId", "Title" },
                values: new object[] { new Guid("11111111-0000-0000-0000-000000000002"), "How I'm gonna spend the money earned from programming", null, true, new Guid("00000000-0000-0000-0000-000000000001"), "World Travels" });

            migrationBuilder.InsertData(
                table: "BucketlistItems",
                columns: new[] { "Id", "BucketId", "CompletionDate", "ItemDescription" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-0000-0000-000000000001"), new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2019, 5, 17, 16, 49, 53, 777, DateTimeKind.Local).AddTicks(8522), "Become better in C#" },
                    { new Guid("11111111-1111-0000-0000-000000000002"), new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2019, 10, 17, 16, 49, 53, 780, DateTimeKind.Local).AddTicks(6502), "Learn Xamarin" },
                    { new Guid("11111111-1111-0000-0000-000000000003"), new Guid("11111111-0000-0000-0000-000000000001"), null, "Publish my first mobile app" },
                    { new Guid("22222222-1111-0000-0000-000000000001"), new Guid("11111111-0000-0000-0000-000000000002"), null, "Hiking New Zealand" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BucketlistItems_BucketId",
                table: "BucketlistItems",
                column: "BucketId");

            migrationBuilder.CreateIndex(
                name: "IX_Bucketlists_OwnerId",
                table: "Bucketlists",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BucketlistItems");

            migrationBuilder.DropTable(
                name: "Bucketlists");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
