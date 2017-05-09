using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFSamuari.Data.Migrations
{
    public partial class MyTenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BattleLog",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BattleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleLog_Battles_BattleId",
                        column: x => x.BattleId,
                        principalTable: "Battles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BattleEvent",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BattleLogId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EventSummary = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BattleEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BattleEvent_BattleLog_BattleLogId",
                        column: x => x.BattleLogId,
                        principalTable: "BattleLog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BattleEvent_BattleLogId",
                table: "BattleEvent",
                column: "BattleLogId");

            migrationBuilder.CreateIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog",
                column: "BattleId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BattleEvent");

            migrationBuilder.DropTable(
                name: "BattleLog");
        }
    }
}
