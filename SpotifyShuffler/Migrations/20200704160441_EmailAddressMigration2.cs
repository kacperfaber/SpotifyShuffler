using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SpotifyShuffler.Migrations
{
    public partial class EmailAddressMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmailAddresses_EmailAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmailAddressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailAddressId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmailId",
                table: "AspNetUsers",
                column: "EmailId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmailAddresses_EmailId",
                table: "AspNetUsers",
                column: "EmailId",
                principalTable: "EmailAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_EmailAddresses_EmailId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_EmailId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<Guid>(
                name: "EmailAddressId",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_EmailAddressId",
                table: "AspNetUsers",
                column: "EmailAddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_EmailAddresses_EmailAddressId",
                table: "AspNetUsers",
                column: "EmailAddressId",
                principalTable: "EmailAddresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
