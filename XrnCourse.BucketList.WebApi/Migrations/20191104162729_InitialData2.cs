using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XrnCourse.BucketList.WebApi.Migrations
{
    public partial class InitialData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems");

            migrationBuilder.DropIndex(
                name: "IX_BucketlistItems_ParentBucketId",
                table: "BucketlistItems");

            migrationBuilder.DropColumn(
                name: "ParentBucketId",
                table: "BucketlistItems");

            migrationBuilder.AddColumn<Guid>(
                name: "BucketId",
                table: "BucketlistItems",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000001"),
                columns: new[] { "BucketId", "CompletionDate" },
                values: new object[] { new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2019, 5, 4, 17, 27, 28, 576, DateTimeKind.Local).AddTicks(8584) });

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000002"),
                columns: new[] { "BucketId", "CompletionDate" },
                values: new object[] { new Guid("11111111-0000-0000-0000-000000000001"), new DateTime(2019, 10, 4, 17, 27, 28, 579, DateTimeKind.Local).AddTicks(6975) });

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000003"),
                column: "BucketId",
                value: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-0000-0000-000000000001"),
                column: "BucketId",
                value: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.CreateIndex(
                name: "IX_BucketlistItems_BucketId",
                table: "BucketlistItems",
                column: "BucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketlistItems_Bucketlists_BucketId",
                table: "BucketlistItems",
                column: "BucketId",
                principalTable: "Bucketlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BucketlistItems_Bucketlists_BucketId",
                table: "BucketlistItems");

            migrationBuilder.DropIndex(
                name: "IX_BucketlistItems_BucketId",
                table: "BucketlistItems");

            migrationBuilder.DropColumn(
                name: "BucketId",
                table: "BucketlistItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ParentBucketId",
                table: "BucketlistItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000001"),
                columns: new[] { "CompletionDate", "ParentBucketId" },
                values: new object[] { new DateTime(2019, 5, 4, 13, 12, 11, 112, DateTimeKind.Local).AddTicks(4216), new Guid("11111111-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000002"),
                columns: new[] { "CompletionDate", "ParentBucketId" },
                values: new object[] { new DateTime(2019, 10, 4, 13, 12, 11, 115, DateTimeKind.Local).AddTicks(668), new Guid("11111111-0000-0000-0000-000000000001") });

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-0000-0000-000000000003"),
                column: "ParentBucketId",
                value: new Guid("11111111-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "BucketlistItems",
                keyColumn: "Id",
                keyValue: new Guid("22222222-1111-0000-0000-000000000001"),
                column: "ParentBucketId",
                value: new Guid("11111111-0000-0000-0000-000000000002"));

            migrationBuilder.CreateIndex(
                name: "IX_BucketlistItems_ParentBucketId",
                table: "BucketlistItems",
                column: "ParentBucketId");

            migrationBuilder.AddForeignKey(
                name: "FK_BucketlistItems_Bucketlists_ParentBucketId",
                table: "BucketlistItems",
                column: "ParentBucketId",
                principalTable: "Bucketlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
