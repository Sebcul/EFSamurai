using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamuari.Data.Migrations
{
    public partial class MyEleventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog");

            migrationBuilder.DropIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog");

            migrationBuilder.DropColumn(
                name: "BattleId",
                table: "BattleLog");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BattleLog",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BattleLogId",
                table: "Battles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BattleLogId1",
                table: "Battles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_BattleLogId",
                table: "Battles",
                column: "BattleLogId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_BattleLogId1",
                table: "Battles",
                column: "BattleLogId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId",
                table: "Battles",
                column: "BattleLogId",
                principalTable: "BattleLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId1",
                table: "Battles",
                column: "BattleLogId1",
                principalTable: "BattleLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId",
                table: "Battles");

            migrationBuilder.DropForeignKey(
                name: "FK_Battles_BattleLog_BattleLogId1",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_BattleLogId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_BattleLogId1",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BattleLog");

            migrationBuilder.DropColumn(
                name: "BattleLogId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "BattleLogId1",
                table: "Battles");

            migrationBuilder.AddColumn<int>(
                name: "BattleId",
                table: "BattleLog",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BattleLog_BattleId",
                table: "BattleLog",
                column: "BattleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BattleLog_Battles_BattleId",
                table: "BattleLog",
                column: "BattleId",
                principalTable: "Battles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
