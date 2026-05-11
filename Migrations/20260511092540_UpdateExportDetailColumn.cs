using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExportDetailColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportDetails_ImportReceipts_ImportReceiptId",
                table: "ExportDetails");

            migrationBuilder.RenameColumn(
                name: "ImportReceiptId",
                table: "ExportDetails",
                newName: "ExportReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_ExportDetails_ImportReceiptId",
                table: "ExportDetails",
                newName: "IX_ExportDetails_ExportReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportDetails_ExportReceipts_ExportReceiptId",
                table: "ExportDetails",
                column: "ExportReceiptId",
                principalTable: "ExportReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExportDetails_ExportReceipts_ExportReceiptId",
                table: "ExportDetails");

            migrationBuilder.RenameColumn(
                name: "ExportReceiptId",
                table: "ExportDetails",
                newName: "ImportReceiptId");

            migrationBuilder.RenameIndex(
                name: "IX_ExportDetails_ExportReceiptId",
                table: "ExportDetails",
                newName: "IX_ExportDetails_ImportReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExportDetails_ImportReceipts_ImportReceiptId",
                table: "ExportDetails",
                column: "ImportReceiptId",
                principalTable: "ImportReceipts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
