using Microsoft.EntityFrameworkCore.Migrations;

namespace Fsn.Infrastructure.EF.Migrations
{
    public partial class Comment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TComment_TArticle_ArticleId",
                table: "TComment");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "TComment");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "TComment");

            migrationBuilder.RenameColumn(
                name: "Statuse",
                table: "TComment",
                newName: "Status");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "TComment",
                type: "nvarchar(2000)",
                maxLength: 2000,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TComment_TArticle_ArticleId",
                table: "TComment",
                column: "ArticleId",
                principalTable: "TArticle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TComment_TArticle_ArticleId",
                table: "TComment");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TComment",
                newName: "Statuse");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "TComment",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(2000)",
                oldMaxLength: 2000);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "TComment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "TComment",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TComment_TArticle_ArticleId",
                table: "TComment",
                column: "ArticleId",
                principalTable: "TArticle",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
