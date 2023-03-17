using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GamingVIVE.Data.Migrations
{
    /// <inheritdoc />
    public partial class GenreIsntWorking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0c53707f-3605-4597-be09-fc086281975a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7cdad5a3-fc04-4950-93cb-fd70630f80d4");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "051f5cb1-aaa2-4902-b5d5-e6fdf54ab933", null, "Administrator", "ADMINSTRATOR" },
                    { "b42d02a4-99a0-42b3-a3a3-a15e71bd9fb9", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "051f5cb1-aaa2-4902-b5d5-e6fdf54ab933");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b42d02a4-99a0-42b3-a3a3-a15e71bd9fb9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0c53707f-3605-4597-be09-fc086281975a", null, "Administrator", "ADMINSTRATOR" },
                    { "7cdad5a3-fc04-4950-93cb-fd70630f80d4", null, "User", "USER" }
                });
        }
    }
}
