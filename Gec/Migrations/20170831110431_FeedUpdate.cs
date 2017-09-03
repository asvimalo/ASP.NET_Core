using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gec.Migrations
{
    public partial class FeedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Pictures_PictureId",
                table: "Feeds");

            migrationBuilder.AddColumn<bool>(
                name: "Approved",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UnLikes",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsArchived",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Pictures_PictureId",
                table: "Feeds",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "PictureId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeds_Pictures_PictureId",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Approved",
                table: "Feeds");

            migrationBuilder.AlterColumn<int>(
                name: "UnLikes",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Stars",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "PictureId",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Likes",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AlterColumn<bool>(
                name: "IsArchived",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "Feeds",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Feeds_Pictures_PictureId",
                table: "Feeds",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "PictureId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
