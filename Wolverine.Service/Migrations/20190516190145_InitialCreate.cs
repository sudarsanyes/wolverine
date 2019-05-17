using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wolverine.Service.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cards",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    GroupID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cards", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(nullable: false),
                    Author = table.Column<string>(nullable: true),
                    UnsortedGroupID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ProjectID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Groups_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SortSessions",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    Participant = table.Column<string>(nullable: true),
                    SessionDate = table.Column<DateTimeOffset>(nullable: false),
                    ProjectID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SortSessions", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SortSessions_Projects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "Projects",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cards_GroupID",
                table: "Cards",
                column: "GroupID");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ProjectID",
                table: "Groups",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_UnsortedGroupID",
                table: "Projects",
                column: "UnsortedGroupID");

            migrationBuilder.CreateIndex(
                name: "IX_SortSessions_ProjectID",
                table: "SortSessions",
                column: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cards_Groups_GroupID",
                table: "Cards",
                column: "GroupID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Groups_UnsortedGroupID",
                table: "Projects",
                column: "UnsortedGroupID",
                principalTable: "Groups",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Groups_UnsortedGroupID",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Cards");

            migrationBuilder.DropTable(
                name: "SortSessions");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
