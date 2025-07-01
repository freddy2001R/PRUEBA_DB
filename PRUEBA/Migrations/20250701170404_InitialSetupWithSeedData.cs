using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRUEBA.Migrations
{
    /// <inheritdoc />
    public partial class InitialSetupWithSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "TU_HASH_REAL_PARA_ADMIN123_AQUI");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "TU_HASH_REAL_PARA_USER123_AQUI");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "freddy12345");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "freddy123456");
        }
    }
}
