using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookLoanApp.Migrations
{
    public partial class AddDemoUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Inserir dados de demonstração
            var adminPassword = "AdminPassword123!";
            var clientPassword = "ClientPassword123!";

            // Utilizar o método de hashing e salting do seu serviço de autenticação
            var (adminHash, adminSalt) = PasswordUtils.CreateHashAndSalt(adminPassword);
            var (clientHash, clientSalt) = PasswordUtils.CreateHashAndSalt(clientPassword);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "FullName", "UserName", "Email", "Situation", "HashPass", "SaltPass", "RegisterDate", "LastAlterationDate", "Profile", "Turno" },
                values: new object[]
                {
                    "Admin User", "admin", "admin@example.com", true, adminHash, adminSalt, DateTime.Now, DateTime.Now, 1, 0
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "FullName", "UserName", "Email", "Situation", "HashPass", "SaltPass", "RegisterDate", "LastAlterationDate", "Profile", "Turno" },
                values: new object[]
                {
                    "Client User", "client", "client@example.com", true, clientHash, clientSalt, DateTime.Now, DateTime.Now, 0, 0
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "admin@example.com");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Email",
                keyValue: "client@example.com");
        }
    }
}