using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDeletebehavior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Expenses__UserId__4E88ABD4",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK__Expenses__Wallet__4F7CD00D",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__RoleI__403A8C7D",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__UserI__3F466844",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__Wallets__UserId__47DBAE45",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK__Wallets__WalletT__48CFD27E",
                table: "Wallets");

            migrationBuilder.AddForeignKey(
                name: "FK__Expenses__UserId__4E88ABD4",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Expenses__Wallet__4F7CD00D",
                table: "Expenses",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__RoleI__403A8C7D",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__UserI__3F466844",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK__Wallets__UserId__47DBAE45",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Wallets__WalletT__48CFD27E",
                table: "Wallets",
                column: "WalletTypeId",
                principalTable: "WalletTypes",
                principalColumn: "WalletTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Expenses__UserId__4E88ABD4",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK__Expenses__Wallet__4F7CD00D",
                table: "Expenses");

            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__RoleI__403A8C7D",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__UserRoles__UserI__3F466844",
                table: "UserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK__Wallets__UserId__47DBAE45",
                table: "Wallets");

            migrationBuilder.DropForeignKey(
                name: "FK__Wallets__WalletT__48CFD27E",
                table: "Wallets");

            migrationBuilder.AddForeignKey(
                name: "FK__Expenses__UserId__4E88ABD4",
                table: "Expenses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Expenses__Wallet__4F7CD00D",
                table: "Expenses",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "WalletId");

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__RoleI__403A8C7D",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK__UserRoles__UserI__3F466844",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Wallets__UserId__47DBAE45",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK__Wallets__WalletT__48CFD27E",
                table: "Wallets",
                column: "WalletTypeId",
                principalTable: "WalletTypes",
                principalColumn: "WalletTypeId");
        }
    }
}
