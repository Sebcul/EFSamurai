using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamuari.Data.Migrations
{
    public partial class MyTwelvethMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId1",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_BattleLogId1",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "BattleLogId1",
                table: "Battles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BattleLogId1",
                table: "Battles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_BattleLogId1",
                table: "Battles",
                column: "BattleLogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId1",
                table: "Battles",
                column: "BattleLogId1",
                principalTable: "BattleLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
