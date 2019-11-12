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
                    ParentBucketId = table.Column<Guid>(nullable: true),
                    ItemDescription = table.Column<string>(maxLength: 50, nullable: false),
                    CompletionDate = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BucketlistItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                        column: x => x.ParentBucketId,
                        principalTable: "Bucketlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BucketlistItems_ParentBucketId",
                table: "BucketlistItems",
                column: "ParentBucketId");

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
