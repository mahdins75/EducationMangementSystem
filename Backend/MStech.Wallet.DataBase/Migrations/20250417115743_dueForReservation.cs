using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class dueForReservation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DueDateTime",
                table: "Transaction",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DueDateTime",
                table: "Transaction");
        }
    }
}
