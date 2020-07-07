using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class AddedMessagesThatWork : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Conversations",
                schema: "YT",
                columns: table => new
                {
                    ParentID = table.Column<int>(nullable: false),
                    ProgramSupervisorID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conversations", x => new { x.ParentID, x.ProgramSupervisorID });
                    table.ForeignKey(
                        name: "FK_Conversations_Parents_ParentID",
                        column: x => x.ParentID,
                        principalSchema: "YT",
                        principalTable: "Parents",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conversations_ProgramSupervisors_ProgramSupervisorID",
                        column: x => x.ProgramSupervisorID,
                        principalSchema: "YT",
                        principalTable: "ProgramSupervisors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Message",
                schema: "YT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MessageText = table.Column<string>(maxLength: 500, nullable: false),
                    ConversationID = table.Column<int>(nullable: false),
                    ConversationParentID = table.Column<int>(nullable: true),
                    ConversationProgramSupervisorID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Message", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Message_Conversations_ConversationParentID_ConversationProgramSupervisorID",
                        columns: x => new { x.ConversationParentID, x.ConversationProgramSupervisorID },
                        principalSchema: "YT",
                        principalTable: "Conversations",
                        principalColumns: new[] { "ParentID", "ProgramSupervisorID" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Conversations_ProgramSupervisorID",
                schema: "YT",
                table: "Conversations",
                column: "ProgramSupervisorID");

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationParentID_ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message",
                columns: new[] { "ConversationParentID", "ConversationProgramSupervisorID" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Message",
                schema: "YT");

            migrationBuilder.DropTable(
                name: "Conversations",
                schema: "YT");
        }
    }
}
