using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFSamuari.Data.Migrations
{
    public partial class MySixteenthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Battles_Samurais_WinnerId",
                table: "Battles");

            migrationBuilder.DropIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "WinnerId",
                table: "Battles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WinnerId",
                table: "Battles",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Battles_Samurais_WinnerId",
                table: "Battles",
                column: "WinnerId",
                principalTable: "Samurais",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
