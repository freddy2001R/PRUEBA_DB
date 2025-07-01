using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 

namespace PRUEBA.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreationFull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    FieldID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DataType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Procedures",
                columns: table => new
                {
                    ProcedureID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcedureName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedByUserID = table.Column<int>(type: "int", nullable: false),
                    LastModifiedUserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Procedures", x => x.ProcedureID);
                    table.ForeignKey(
                        name: "FK_Procedures_Users_CreatedByUserID",
                        column: x => x.CreatedByUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Procedures_Users_LastModifiedUserID",
                        column: x => x.LastModifiedUserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false),
                    ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserID, x.RoleID });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DataSets",
                columns: table => new
                {
                    DataSetID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataSetName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProcedureID = table.Column<int>(type: "int", nullable: false),
                    FieldId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataSets", x => x.DataSetID);
                    table.ForeignKey(
                        name: "FK_DataSets_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "FieldID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DataSets_Procedures_ProcedureID",
                        column: x => x.ProcedureID,
                        principalTable: "Procedures",
                        principalColumn: "ProcedureID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fields",
                columns: new[] { "FieldID", "DataType", "FieldName" },
                values: new object[,]
                {
                    { 1, "String", "SampleField1" },
                    { 2, "Number", "SampleField2" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleID", "Description", "RoleName" },
                values: new object[,]
                {
                    { 1, null, "Admin" },
                    { 2, null, "User" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserID", "Email", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "admin@example.com", "PEGA_AQUÍ_EL_HASH_DE_ADMIN", "admin" },
                    { 2, "user1@example.com", "PEGA_AQUÍ_EL_HASH_DE_USER1", "user1" }
                });

            migrationBuilder.InsertData(
                table: "Procedures",
                columns: new[] { "ProcedureID", "CreatedByUserID", "CreatedDate", "Description", "LastModifiedDate", "LastModifiedUserID", "ProcedureName" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), "Process for cleaning raw data.", new DateTime(2023, 1, 10, 8, 0, 0, 0, DateTimeKind.Utc), 1, "Data Cleaning" },
                    { 2, 1, new DateTime(2023, 2, 15, 9, 30, 0, 0, DateTimeKind.Utc), "Performing statistical tests on data.", new DateTime(2023, 2, 15, 9, 30, 0, 0, DateTimeKind.Utc), 1, "Statistical Analysis" },
                    { 3, 2, new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Creating charts and dashboards.", new DateTime(2023, 3, 20, 10, 0, 0, 0, DateTimeKind.Utc), 2, "Data Visualization" },
                    { 4, 1, new DateTime(2023, 4, 25, 11, 45, 0, 0, DateTimeKind.Utc), "Developing and training ML models.", new DateTime(2023, 4, 25, 11, 45, 0, 0, DateTimeKind.Utc), 1, "Machine Learning Model" },
                    { 5, 2, new DateTime(2023, 5, 1, 13, 0, 0, 0, DateTimeKind.Utc), "Combining data from various sources.", new DateTime(2023, 5, 1, 13, 0, 0, 0, DateTimeKind.Utc), 2, "Data Integration" }
                });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleID", "UserID", "ID" },
                values: new object[,]
                {
                    { 1, 1, 0 },
                    { 2, 2, 0 }
                });

            migrationBuilder.InsertData(
                table: "DataSets",
                columns: new[] { "DataSetID", "CreatedDate", "DataSetName", "Description", "FieldId", "LastModifiedDate", "ProcedureID" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 10, 8, 15, 0, 0, DateTimeKind.Utc), "Sales Data 2023", "Annual sales records.", 1, new DateTime(2023, 1, 10, 8, 15, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, new DateTime(2023, 2, 15, 9, 45, 0, 0, DateTimeKind.Utc), "Customer Feedback", "Survey responses from Q1.", 2, new DateTime(2023, 2, 15, 9, 45, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, new DateTime(2023, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc), "Product Inventory", "Current stock levels.", 1, new DateTime(2023, 1, 20, 10, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 4, new DateTime(2023, 3, 25, 10, 30, 0, 0, DateTimeKind.Utc), "Website Traffic", "Visitor data for Q2.", 2, new DateTime(2023, 3, 25, 10, 30, 0, 0, DateTimeKind.Utc), 3 },
                    { 5, new DateTime(2023, 2, 20, 11, 0, 0, 0, DateTimeKind.Utc), "Employee Performance", "Quarterly performance reviews.", 1, new DateTime(2023, 2, 20, 11, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 6, new DateTime(2023, 5, 5, 14, 0, 0, 0, DateTimeKind.Utc), "Financial Records", "Annual financial statements.", 1, new DateTime(2023, 5, 5, 14, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 7, new DateTime(2023, 3, 30, 9, 0, 0, 0, DateTimeKind.Utc), "Customer Demographics", "Demographic data of customer base.", 2, new DateTime(2023, 3, 30, 9, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 8, new DateTime(2023, 4, 30, 12, 0, 0, 0, DateTimeKind.Utc), "Server Logs", "Server activity and error logs.", 1, new DateTime(2023, 4, 30, 12, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 9, new DateTime(2023, 5, 10, 10, 0, 0, 0, DateTimeKind.Utc), "HR Data", "Human Resources employee data.", 2, new DateTime(2023, 5, 10, 10, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 10, new DateTime(2023, 4, 5, 15, 0, 0, 0, DateTimeKind.Utc), "Marketing Campaign Data", "Performance data for recent campaigns.", 1, new DateTime(2023, 4, 5, 15, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 11, new DateTime(2023, 2, 1, 9, 0, 0, 0, DateTimeKind.Utc), "Supply Chain Data", "Logistics and inventory flow.", 2, new DateTime(2023, 2, 1, 9, 0, 0, 0, DateTimeKind.Utc), 1 },
                    { 12, new DateTime(2023, 5, 15, 16, 0, 0, 0, DateTimeKind.Utc), "IoT Sensor Readings", "Data from various IoT devices.", 1, new DateTime(2023, 5, 15, 16, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 13, new DateTime(2023, 3, 1, 10, 0, 0, 0, DateTimeKind.Utc), "Customer Support Tickets", "Records of customer support interactions.", 2, new DateTime(2023, 3, 1, 10, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 14, new DateTime(2023, 5, 20, 11, 0, 0, 0, DateTimeKind.Utc), "Research Study Results", "Data from scientific research.", 1, new DateTime(2023, 5, 20, 11, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 15, new DateTime(2023, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), "Social Media Engagement", "Metrics from social media platforms.", 2, new DateTime(2023, 4, 10, 13, 0, 0, 0, DateTimeKind.Utc), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DataSets_FieldId",
                table: "DataSets",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DataSets_ProcedureID",
                table: "DataSets",
                column: "ProcedureID");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_CreatedByUserID",
                table: "Procedures",
                column: "CreatedByUserID");

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_LastModifiedUserID",
                table: "Procedures",
                column: "LastModifiedUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleID",
                table: "UserRoles",
                column: "RoleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataSets");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "Procedures");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
