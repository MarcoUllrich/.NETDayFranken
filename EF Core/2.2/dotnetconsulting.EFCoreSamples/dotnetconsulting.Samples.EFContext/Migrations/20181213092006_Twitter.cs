using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public partial class Twitter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                schema: "dnc",
                table: "Speakers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Twitter",
                schema: "dnc",
                table: "Speakers");
        }
    }
}
