using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MStech.Wallet.DataBase.Migrations
{
    /// <inheritdoc />
    public partial class institutiondocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstitutionDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    InstitutionClassId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatorId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifierId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstitutionDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstitutionDocuments_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InstitutionDocuments_InstitutionClasses_InstitutionClassId",
                        column: x => x.InstitutionClassId,
                        principalTable: "InstitutionClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionDocuments_InstitutionClassId",
                table: "InstitutionDocuments",
                column: "InstitutionClassId");

            migrationBuilder.CreateIndex(
                name: "IX_InstitutionDocuments_OwnerId",
                table: "InstitutionDocuments",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstitutionDocuments");
        }
    }
}
