using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InfoSearch.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Mygroup = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Keywords = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    StudentId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Words_MyStems",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Term = table.Column<string>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words_MyStems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_MyStems_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Words_Porters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Term = table.Column<string>(nullable: true),
                    ArticleId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words_Porters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Words_Porters_Articles_ArticleId",
                        column: x => x.ArticleId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Articles_StudentId",
                table: "Articles",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_MyStems_ArticleId",
                table: "Words_MyStems",
                column: "ArticleId");

            migrationBuilder.CreateIndex(
                name: "IX_Words_Porters_ArticleId",
                table: "Words_Porters",
                column: "ArticleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words_MyStems");

            migrationBuilder.DropTable(
                name: "Words_Porters");

            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
