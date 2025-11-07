using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentIdToStudentActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "StudentActivities",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StudentActivities_StudentId",
                table: "StudentActivities",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentActivities_AspNetUsers_StudentId",
                table: "StudentActivities",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentActivities_AspNetUsers_StudentId",
                table: "StudentActivities");

            migrationBuilder.DropIndex(
                name: "IX_StudentActivities_StudentId",
                table: "StudentActivities");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "StudentActivities");
        }
    }
}