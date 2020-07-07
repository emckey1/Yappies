using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class RemigrateChldren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Child_Parents_ParentID",
                schema: "YT",
                table: "Child");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Child_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Child",
                schema: "YT",
                table: "Child");

            migrationBuilder.RenameTable(
                name: "Child",
                schema: "YT",
                newName: "Children",
                newSchema: "YT");

            migrationBuilder.RenameIndex(
                name: "IX_Child_ParentID",
                schema: "YT",
                table: "Children",
                newName: "IX_Children_ParentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Children",
                schema: "YT",
                table: "Children",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Children_Parents_ParentID",
                schema: "YT",
                table: "Children",
                column: "ParentID",
                principalSchema: "YT",
                principalTable: "Parents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Children_Parents_ParentID",
                schema: "YT",
                table: "Children");

            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Children_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Children",
                schema: "YT",
                table: "Children");

            migrationBuilder.RenameTable(
                name: "Children",
                schema: "YT",
                newName: "Child",
                newSchema: "YT");

            migrationBuilder.RenameIndex(
                name: "IX_Children_ParentID",
                schema: "YT",
                table: "Child",
                newName: "IX_Child_ParentID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Child",
                schema: "YT",
                table: "Child",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Child_Parents_ParentID",
                schema: "YT",
                table: "Child",
                column: "ParentID",
                principalSchema: "YT",
                principalTable: "Parents",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Programs_Child_ChildID",
                schema: "YT",
                table: "Programs",
                column: "ChildID",
                principalSchema: "YT",
                principalTable: "Child",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
