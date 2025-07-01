using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PRUEBA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleID", "UserID" },
                keyValues: new object[] { 1, 1 },
                column: "ID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleID", "UserID" },
                keyValues: new object[] { 2, 2 },
                column: "ID",
                value: 2);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleID", "UserID" },
                keyValues: new object[] { 1, 1 },
                column: "ID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleID", "UserID" },
                keyValues: new object[] { 2, 2 },
                column: "ID",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 1,
                column: "Password",
                value: "PEGA_AQUÍ_EL_HASH_DE_ADMIN");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserID",
                keyValue: 2,
                column: "Password",
                value: "PEGA_AQUÍ_EL_HASH_DE_USER1");
        }
    }
}
