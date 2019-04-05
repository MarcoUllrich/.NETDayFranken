using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace dotnetconsulting.Samples.EFContext.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dnc");

            migrationBuilder.CreateTable(
                name: "Speakers",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Homepage = table.Column<string>(nullable: true),
                    Infos = table.Column<string>(nullable: false, defaultValue: "(Keine Infos)"),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Speakers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TechEvents",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Venue = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SecretCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(nullable: true),
                    ContentDescription = table.Column<string>(maxLength: 300, nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: true),
                    SpeakerId = table.Column<int>(nullable: false),
                    TechEventId = table.Column<int>(nullable: false),
                    Begin = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Updated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_TechEvents_TechEventId",
                        column: x => x.TechEventId,
                        principalSchema: "dnc",
                        principalTable: "TechEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VenueSetup",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    TechEventId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VenueSetup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VenueSetup_TechEvents_TechEventId",
                        column: x => x.TechEventId,
                        principalSchema: "dnc",
                        principalTable: "TechEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpeakerSessions",
                schema: "dnc",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SpeakerId = table.Column<int>(nullable: false),
                    SessionId = table.Column<int>(nullable: false),
                    Tag = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpeakerSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpeakerSessions_Sessions_SessionId",
                        column: x => x.SessionId,
                        principalSchema: "dnc",
                        principalTable: "Sessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpeakerSessions_Speakers_SpeakerId",
                        column: x => x.SpeakerId,
                        principalSchema: "dnc",
                        principalTable: "Speakers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_TechEventId",
                schema: "dnc",
                table: "Sessions",
                column: "TechEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Title",
                schema: "dnc",
                table: "Sessions",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_Created_Updated",
                schema: "dnc",
                table: "Sessions",
                columns: new[] { "Created", "Updated" },
                unique: true,
                filter: "[Updated] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSessions_SessionId",
                schema: "dnc",
                table: "SpeakerSessions",
                column: "SessionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpeakerSessions_SpeakerId",
                schema: "dnc",
                table: "SpeakerSessions",
                column: "SpeakerId");

            migrationBuilder.CreateIndex(
                name: "IX_VenueSetup_TechEventId",
                schema: "dnc",
                table: "VenueSetup",
                column: "TechEventId",
                unique: true,
                filter: "[TechEventId] IS NOT NULL");

            migrationBuilder.ApplyCustomUp();
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpeakerSessions",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "VenueSetup",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "Sessions",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "Speakers",
                schema: "dnc");

            migrationBuilder.DropTable(
                name: "TechEvents",
                schema: "dnc");

            migrationBuilder.ApplyCustomUp();
        }
    }
}
