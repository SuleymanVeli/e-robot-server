using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace e_robot.Infrastructure.Migrations
{
    public partial class userexam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Country",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "SolutionCount",
                table: "Users",
                newName: "Level");

            migrationBuilder.RenameColumn(
                name: "TaskText",
                table: "Exams",
                newName: "Level");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Users",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Solutions",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ComplateMinute",
                table: "Solutions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Complated",
                table: "Solutions",
                type: "tinyint(1)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ComplatedDate",
                table: "Solutions",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "WarningRates",
                table: "Solutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "XmlBlock",
                table: "Solutions",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "BlockTools",
                table: "Exams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Exams",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Exams",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ComplateMinute",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "Complated",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "ComplatedDate",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "WarningRates",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "XmlBlock",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "BlockTools",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Exams");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Exams");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Users",
                newName: "SolutionCount");

            migrationBuilder.RenameColumn(
                name: "Level",
                table: "Exams",
                newName: "TaskText");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "Users",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Solutions",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
