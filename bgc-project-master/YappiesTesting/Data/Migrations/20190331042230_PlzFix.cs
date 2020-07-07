using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class PlzFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversations_ConversationParentID_ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ConversationParentID_ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ConversationParentID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ConversationID",
                schema: "YT",
                table: "Message",
                newName: "ProgramSupervisorID");

            migrationBuilder.AddColumn<int>(
                name: "ParentID",
                schema: "YT",
                table: "Message",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message",
                columns: new[] { "ParentID", "ProgramSupervisorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversations_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message",
                columns: new[] { "ParentID", "ProgramSupervisorID" },
                principalSchema: "YT",
                principalTable: "Conversations",
                principalColumns: new[] { "ParentID", "ProgramSupervisorID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversations_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropIndex(
                name: "IX_Message_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropColumn(
                name: "ParentID",
                schema: "YT",
                table: "Message");

            migrationBuilder.RenameColumn(
                name: "ProgramSupervisorID",
                schema: "YT",
                table: "Message",
                newName: "ConversationID");

            migrationBuilder.AddColumn<int>(
                name: "ConversationParentID",
                schema: "YT",
                table: "Message",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Message_ConversationParentID_ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message",
                columns: new[] { "ConversationParentID", "ConversationProgramSupervisorID" });

            migrationBuilder.AddForeignKey(
                name: "FK_Message_Conversations_ConversationParentID_ConversationProgramSupervisorID",
                schema: "YT",
                table: "Message",
                columns: new[] { "ConversationParentID", "ConversationProgramSupervisorID" },
                principalSchema: "YT",
                principalTable: "Conversations",
                principalColumns: new[] { "ParentID", "ProgramSupervisorID" },
                onDelete: ReferentialAction.Restrict);
        }
    }
}
