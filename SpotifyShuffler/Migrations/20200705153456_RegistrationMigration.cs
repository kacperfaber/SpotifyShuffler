using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyShuffler.Migrations
{
    public partial class RegistrationMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Registrations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EmailAddress = table.Column<string>(nullable: true),
                    ActivationCode = table.Column<string>(nullable: true),
                    SendedAt = table.Column<DateTime>(nullable: true),
                    ActivatedAt = table.Column<DateTime>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserCreatedAt = table.Column<DateTime>(nullable: true),
                    SpotifyAccountId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Registrations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Registrations_SpotifyAccounts_SpotifyAccountId",
                        column: x => x.SpotifyAccountId,
                        principalTable: "SpotifyAccounts",
                        principalColumn: "SpotifyId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Registrations_SpotifyAccountId",
                table: "Registrations",
                column: "SpotifyAccountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Registrations");
        }
    }
}
