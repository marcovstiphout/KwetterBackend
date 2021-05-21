using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kwetter.Services.KweetService.Persistence.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kweets",
                columns: table => new
                {
                    KweetId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SenderProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    KweetMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KweetPostDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kweets", x => x.KweetId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Kweets");
        }
    }
}
