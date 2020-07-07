using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class WhyDoIHaveToMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Message_Conversations_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Message",
                schema: "YT",
                table: "Message");

            migrationBuilder.RenameTable(
                name: "Message",
                schema: "YT",
                newName: "Messages",
                newSchema: "YT");

            migrationBuilder.RenameIndex(
                name: "IX_Message_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Messages",
                newName: "IX_Messages_ParentID_ProgramSupervisorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                schema: "YT",
                table: "Messages",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Conversations_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Messages",
                columns: new[] { "ParentID", "ProgramSupervisorID" },
                principalSchema: "YT",
                principalTable: "Conversations",
                principalColumns: new[] { "ParentID", "ProgramSupervisorID" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Conversations_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                schema: "YT",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                schema: "YT",
                newName: "Message",
                newSchema: "YT");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ParentID_ProgramSupervisorID",
                schema: "YT",
                table: "Message",
                newName: "IX_Message_ParentID_ProgramSupervisorID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Message",
                schema: "YT",
                table: "Message",
                column: "ID");

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
    }
}
