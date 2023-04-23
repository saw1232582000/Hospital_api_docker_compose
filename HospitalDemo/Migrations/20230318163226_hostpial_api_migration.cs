using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HospitalDemo.Migrations
{
    /// <inheritdoc />
    public partial class hostpial_api_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    Contact_Detail = table.Column<string>(type: "text", nullable: false),
                    created_time = table.Column<DateTime>(type: "date", nullable: false),
                    updated_time = table.Column<DateTime>(type: "date", nullable: false),
                    Created_user_id = table.Column<int>(type: "integer", nullable: false),
                    Updated_user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "patients");
        }
    }
}
