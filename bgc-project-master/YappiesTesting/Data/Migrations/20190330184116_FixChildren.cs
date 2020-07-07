using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class FixChildren : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Programs_Children",
                schema: "YT",
                columns: table => new
                {
                    ChildID = table.Column<int>(nullable: false),
                    ProgramID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Programs_Children", x => new { x.ProgramID, x.ChildID });
                    table.ForeignKey(
                        name: "FK_Programs_Children_Children_ChildID",
                        column: x => x.ChildID,
                        principalSchema: "YT",
                        principalTable: "Children",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Programs_Children_Programs_ProgramID",
                        column: x => x.ProgramID,
                        principalSchema: "YT",
                        principalTable: "Programs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Programs_Children_ChildID",
                schema: "YT",
                table: "Programs_Children",
                column: "ChildID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Programs_Children",
                schema: "YT");
        }
    }
}
