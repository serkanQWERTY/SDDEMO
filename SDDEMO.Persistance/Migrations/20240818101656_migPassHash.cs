using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDDEMO.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class migPassHash : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "passwordHash");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "passwordHash",
                table: "Users",
                newName: "password");
        }
    }
}
