using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FamilyHistorySystem.API.Migrations
{
    /// <inheritdoc />
    public partial class addIsActiveUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                schema: "Auth",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                schema: "Auth",
                table: "Users");
        }
    }
}
