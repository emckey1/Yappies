using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Migrations.yappiesTesting
{
    public partial class AddMichaelTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotificationOptIn",
                schema: "YT",
                table: "Parents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotificationOptIn",
                schema: "YT",
                table: "Parents");
        }
    }
}
