using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class discountCodeTablemodified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferralCodeText",
                table: "DiscountCode");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodeBankId",
                table: "DiscountCode",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCode_DiscountCodeBankId",
                table: "DiscountCode",
                column: "DiscountCodeBankId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCode_DiscountCodeBanks_DiscountCodeBankId",
                table: "DiscountCode",
                column: "DiscountCodeBankId",
                principalTable: "DiscountCodeBanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCode_DiscountCodeBanks_DiscountCodeBankId",
                table: "DiscountCode");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCode_DiscountCodeBankId",
                table: "DiscountCode");

            migrationBuilder.DropColumn(
                name: "DiscountCodeBankId",
                table: "DiscountCode");

            migrationBuilder.AddColumn<string>(
                name: "ReferralCodeText",
                table: "DiscountCode",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
