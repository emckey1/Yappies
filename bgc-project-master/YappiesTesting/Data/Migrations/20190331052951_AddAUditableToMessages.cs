using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class AddAUditableToMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                schema: "YT",
                table: "Messages",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                schema: "YT",
                table: "Messages",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "YT",
                table: "Messages",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                schema: "YT",
                table: "Messages",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                schema: "YT",
                table: "Messages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                schema: "YT",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                schema: "YT",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "YT",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                schema: "YT",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                schema: "YT",
                table: "Messages");
        }
    }
}
