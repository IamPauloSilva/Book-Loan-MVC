using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLoanApp.Migrations
{
    /// <inheritdoc />
    public partial class modifiedLoansTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoanModel_Books_BooksId",
                table: "LoanModel");

            migrationBuilder.DropForeignKey(
                name: "FK_LoanModel_Users_UserId",
                table: "LoanModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoanModel",
                table: "LoanModel");

            migrationBuilder.RenameTable(
                name: "LoanModel",
                newName: "Loans");

            migrationBuilder.RenameIndex(
                name: "IX_LoanModel_UserId",
                table: "Loans",
                newName: "IX_Loans_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoanModel_BooksId",
                table: "Loans",
                newName: "IX_Loans_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Loans",
                table: "Loans",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Books_BooksId",
                table: "Loans");

            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Users_UserId",
                table: "Loans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Loans",
                table: "Loans");

            migrationBuilder.RenameTable(
                name: "Loans",
                newName: "LoanModel");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_UserId",
                table: "LoanModel",
                newName: "IX_LoanModel_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Loans_BooksId",
                table: "LoanModel",
                newName: "IX_LoanModel_BooksId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoanModel",
                table: "LoanModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoanModel_Books_BooksId",
                table: "LoanModel",
                column: "BooksId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LoanModel_Users_UserId",
                table: "LoanModel",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
