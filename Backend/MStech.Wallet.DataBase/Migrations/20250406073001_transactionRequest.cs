using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class transactionRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmationCode",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "WaitForConfirmation",
                table: "Transaction");

            migrationBuilder.CreateTable(
                name: "TransactionRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<int>(type: "int", nullable: false),
                    WalletTransactionCalculationType = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    JsonTransActionDataFromClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: true),
                    WalletId = table.Column<int>(type: "int", nullable: true),
                    IsWalletValue = table.Column<bool>(type: "bit", nullable: true),
                    NotMentionedInReport = table.Column<bool>(type: "bit", nullable: true),
                    InvioceId = table.Column<int>(type: "int", nullable: true),
                    InvioceTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConfirmationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WaitForConfirmation = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionRequest", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TransactionRequest");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmationCode",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WaitForConfirmation",
                table: "Transaction",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
