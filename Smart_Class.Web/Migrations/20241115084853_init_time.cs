using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Smart_Class.Web.Migrations
{
    /// <inheritdoc />
    public partial class init_time : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b60b54a3-bda5-4091-b9aa-5dc02afe9d32"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("bae59d25-04ae-4be2-b888-6540f8d09344"));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "start_Time",
                table: "Classes",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End_Time",
                table: "Classes",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Persian_Name" },
                values: new object[,]
                {
                    { new Guid("a0cf1bde-5a49-4a4a-b595-be9e0385c6fd"), null, "admin", "admin", "مدیر سیستم" },
                    { new Guid("a415868e-15e3-41ad-a83b-042c4d0bfcee"), null, "teacher", "teacher", "استاد" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a0cf1bde-5a49-4a4a-b595-be9e0385c6fd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a415868e-15e3-41ad-a83b-042c4d0bfcee"));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_Time",
                table: "Classes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "End_Time",
                table: "Classes",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Persian_Name" },
                values: new object[,]
                {
                    { new Guid("b60b54a3-bda5-4091-b9aa-5dc02afe9d32"), null, "teacher", "teacher", "استاد" },
                    { new Guid("bae59d25-04ae-4be2-b888-6540f8d09344"), null, "admin", "admin", "مدیر سیستم" }
                });
        }
    }
}
