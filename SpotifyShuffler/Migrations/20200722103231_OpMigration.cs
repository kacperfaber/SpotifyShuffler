using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyShuffler.Migrations
{
    public partial class OpMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PlaylistPrototype",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistPrototype", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    IsCanceled = table.Column<bool>(nullable: false),
                    CanceledAt = table.Column<DateTime>(nullable: true),
                    OriginalPlaylistId = table.Column<string>(nullable: true),
                    OriginalPlaylistName = table.Column<string>(nullable: true),
                    OriginalPlaylistDescription = table.Column<string>(nullable: true),
                    IsSubmitted = table.Column<bool>(nullable: false),
                    SubmittedAt = table.Column<DateTime>(nullable: true),
                    CreatedPlaylistId = table.Column<string>(nullable: true),
                    PlaylistName = table.Column<string>(nullable: true),
                    PlaylistDescription = table.Column<string>(nullable: true),
                    OwnerId = table.Column<Guid>(nullable: false),
                    PlaylistPrototypeId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Operations_PlaylistPrototype_PlaylistPrototypeId",
                        column: x => x.PlaylistPrototypeId,
                        principalTable: "PlaylistPrototype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrackPrototypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PlaylistPrototypeId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Author = table.Column<string>(nullable: true),
                    Album = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    SpotifyId = table.Column<string>(nullable: true),
                    SpotifyUri = table.Column<string>(nullable: true),
                    DurationMs = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackPrototypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrackPrototypes_PlaylistPrototype_PlaylistPrototypeId",
                        column: x => x.PlaylistPrototypeId,
                        principalTable: "PlaylistPrototype",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Operations_PlaylistPrototypeId",
                table: "Operations",
                column: "PlaylistPrototypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackPrototypes_PlaylistPrototypeId",
                table: "TrackPrototypes",
                column: "PlaylistPrototypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Operations");

            migrationBuilder.DropTable(
                name: "TrackPrototypes");

            migrationBuilder.DropTable(
                name: "PlaylistPrototype");
        }
    }
}
