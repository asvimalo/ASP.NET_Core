using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Gec.Migrations
{
    public partial class NewFeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Feeds");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateArchived",
                table: "Feeds",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stars",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UnLikes",
                table: "Feeds",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Comments",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateArchived",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "Stars",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "UnLikes",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Comments");

            migrationBuilder.AddColumn<DateTime>(
                name: "Archived",
                table: "Feeds",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
