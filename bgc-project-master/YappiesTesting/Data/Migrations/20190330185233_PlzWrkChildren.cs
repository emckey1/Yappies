using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class PlzWrkChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Children_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ChildID",
                schema: "YT",
                table: "Programs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildID",
                schema: "YT",
                table: "Programs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ChildID",
                schema: "YT",
                table: "Programs",
                column: "ChildID");

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Children_ChildID",
                schema: "YT",
                table: "Programs",
                column: "ChildID",
                principalSchema: "YT",
                principalTable: "Children",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
