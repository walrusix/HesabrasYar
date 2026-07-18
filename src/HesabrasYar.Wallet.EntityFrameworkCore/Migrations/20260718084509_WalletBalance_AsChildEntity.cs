using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HesabrasYar.Wallet.Migrations
{
    /// <inheritdoc />
    public partial class WalletBalance_AsChildEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "AppWalletBalances");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "AppWalletBalances");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "AppWalletBalances",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "AppWalletBalances",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
