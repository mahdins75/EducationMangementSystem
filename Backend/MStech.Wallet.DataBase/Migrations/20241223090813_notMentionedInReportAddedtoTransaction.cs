using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class notMentionedInReportAddedtoTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NotMentionedInReport",
                table: "Transaction",
                type: "bit",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotMentionedInReport",
                table: "Transaction");
        }
    }
}
