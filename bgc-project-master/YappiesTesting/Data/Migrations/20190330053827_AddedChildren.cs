using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class AddedChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ChildID",
                schema: "YT",
                table: "Programs",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Child",
                schema: "YT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    ParentID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Child", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Child_Parents_ParentID",
                        column: x => x.ParentID,
                        principalSchema: "YT",
                        principalTable: "Parents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_ChildID",
                schema: "YT",
                table: "Programs",
                column: "ChildID");

            migrationBuilder.CreateIndex(
                name: "IX_Child_ParentID",
                schema: "YT",
                table: "Child",
                column: "ParentID");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Programs_Child_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropTable(
                name: "Child",
                schema: "YT");

            migrationBuilder.DropIndex(
                name: "IX_Programs_ChildID",
                schema: "YT",
                table: "Programs");

            migrationBuilder.DropColumn(
                name: "ChildID",
                schema: "YT",
                table: "Programs");
        }
    }
}
