using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class TransactionPrty1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransactionPartyWalleId",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionPartyWalleId",
                table: "Transaction",
                column: "TransactionPartyWalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Wallet_TransactionPartyWalleId",
                table: "Transaction",
                column: "TransactionPartyWalleId",
                principalTable: "Wallet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Wallet_TransactionPartyWalleId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_TransactionPartyWalleId",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "TransactionPartyWalleId",
                table: "Transaction");
        }
    }
}
