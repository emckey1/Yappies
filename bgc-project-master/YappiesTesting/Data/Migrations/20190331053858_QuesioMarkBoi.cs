using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class QuesioMarkBoi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Programs_ProgramID",
                schema: "YT",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                schema: "YT",
                table: "Announcements",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Programs_ProgramID",
                schema: "YT",
                table: "Announcements",
                column: "ProgramID",
                principalSchema: "YT",
                principalTable: "Programs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Programs_ProgramID",
                schema: "YT",
                table: "Announcements");

            migrationBuilder.AlterColumn<int>(
                name: "ProgramID",
                schema: "YT",
                table: "Announcements",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Programs_ProgramID",
                schema: "YT",
                table: "Announcements",
                column: "ProgramID",
                principalSchema: "YT",
                principalTable: "Programs",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
