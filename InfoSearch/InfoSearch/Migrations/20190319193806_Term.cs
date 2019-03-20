using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoSearch.Migrations
{
    public partial class Term : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Term_Lists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Term_Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Term_Lists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article_Terms",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ArticleId = table.Column<Guid>(nullable: false),
                    TermId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article_Terms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_Terms_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_Terms_Term_Lists_TermId",
                        column: x => x.TermId,
                        principalTable: "Term_Lists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_Terms_ArticleId",
                table: "Article_Terms",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_Terms_TermId",
                table: "Article_Terms",
                column: "TermId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article_Terms");

            migrationBuilder.DropTable(
                name: "Term_Lists");
        }
    }
}
