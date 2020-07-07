using Microsoft.EntityFrameworkCore.Migrations;

namespace YappiesTesting.Data.Migrations
{
    public partial class AddedSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "DirectMessageOptIn",
                schema: "YT",
                table: "Parents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "SignOutOptIn",
                schema: "YT",
                table: "Parents",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "TwoFactorOptIn",
                schema: "YT",
                table: "Parents",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DirectMessageOptIn",
                schema: "YT",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "SignOutOptIn",
                schema: "YT",
                table: "Parents");

            migrationBuilder.DropColumn(
                name: "TwoFactorOptIn",
                schema: "YT",
                table: "Parents");
        }
    }
}
