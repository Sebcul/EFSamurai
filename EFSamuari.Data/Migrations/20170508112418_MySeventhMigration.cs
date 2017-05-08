using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EFSamuari.Data.Migrations
{
    public partial class MySeventhMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AliasId",
                table: "Samurais",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SecretIdentity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RealName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SecretIdentity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Samurais_AliasId",
                table: "Samurais",
                column: "AliasId");

            migrationBuilder.AddForeignKey(
                name: "FK_Samurais_SecretIdentity_AliasId",
                table: "Samurais",
                column: "AliasId",
                principalTable: "SecretIdentity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Samurais_SecretIdentity_AliasId",
                table: "Samurais");

            migrationBuilder.DropTable(
                name: "SecretIdentity");

            migrationBuilder.DropIndex(
                name: "IX_Samurais_AliasId",
                table: "Samurais");

            migrationBuilder.DropColumn(
                name: "AliasId",
                table: "Samurais");
        }
    }
}
