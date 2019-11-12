using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XrnCourse.BucketList.WebApi.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems");

            migrationBuilder.DropColumn(
                name: "BucketId",
                table: "BucketlistItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentBucketId",
                table: "BucketlistItems",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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
                columns: new[] { "Id", "CompletionDate", "ItemDescription", "ParentBucketId" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-0000-0000-000000000001"), new DateTime(2019, 5, 4, 13, 12, 11, 112, DateTimeKind.Local).AddTicks(4216), "Become better in C#", new Guid("11111111-0000-0000-0000-000000000001") },
                    { new Guid("11111111-1111-0000-0000-000000000002"), new DateTime(2019, 10, 4, 13, 12, 11, 115, DateTimeKind.Local).AddTicks(668), "Learn Xamarin", new Guid("11111111-0000-0000-0000-000000000001") },
                    { new Guid("11111111-1111-0000-0000-000000000003"), null, "Publish my first mobile app", new Guid("11111111-0000-0000-0000-000000000001") },
                    { new Guid("22222222-1111-0000-0000-000000000001"), null, "Hiking New Zealand", new Guid("11111111-0000-0000-0000-000000000002") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems",
                column: "ParentBucketId",
                principalTable: "Bucketlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems");

            migrationBuilder.DeleteData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Bucketlists",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "Bucketlists",
                keyColumn: "Id",
                keyValue: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ParentBucketId",
                table: "BucketlistItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "BucketId",
                table: "BucketlistItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems",
                column: "ParentBucketId",
                principalTable: "Bucketlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
