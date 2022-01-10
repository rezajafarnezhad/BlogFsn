using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fsn.Infrastructure.EF.Migrations
{
    public partial class CommnetUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "TComment",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_TComment_UserId",
                table: "TComment",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TComment_AspNetUsers_UserId",
                table: "TComment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TComment_AspNetUsers_UserId",
                table: "TComment");

            migrationBuilder.DropIndex(
                name: "IX_TComment_UserId",
                table: "TComment");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TComment");
        }
    }
}
