using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BankingSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddBankAccountManagement : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccount_Customers_CustomerId",
                table: "BankAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount");

            migrationBuilder.RenameTable(
                name: "BankAccount",
                newName: "BankAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccount_CustomerId",
                table: "BankAccounts",
                newName: "IX_BankAccounts_CustomerId");

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "BankAccounts",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountNumber",
                table: "BankAccounts",
                column: "AccountNumber",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccounts_Customers_CustomerId",
                table: "BankAccounts",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BankAccounts_Customers_CustomerId",
                table: "BankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DropIndex(
                name: "IX_BankAccounts_AccountNumber",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BankAccounts");

            migrationBuilder.RenameTable(
                name: "BankAccounts",
                newName: "BankAccount");

            migrationBuilder.RenameIndex(
                name: "IX_BankAccounts_CustomerId",
                table: "BankAccount",
                newName: "IX_BankAccount_CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccount",
                table: "BankAccount",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_BankAccount_Customers_CustomerId",
                table: "BankAccount",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "CustomerId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
