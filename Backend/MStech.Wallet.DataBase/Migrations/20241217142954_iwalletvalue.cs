using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class iwalletvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWalletValue",
                table: "Transaction",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWalletValue",
                table: "Transaction");
        }
    }
}
