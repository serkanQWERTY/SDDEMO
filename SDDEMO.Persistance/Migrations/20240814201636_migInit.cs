using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SDDEMO.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class migInit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    username = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    surname = table.Column<string>(type: "text", nullable: false),
                    mailAddress = table.Column<string>(type: "text", nullable: false),
                    password = table.Column<string>(type: "text", nullable: false),
                    creationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    isActive = table.Column<bool>(type: "boolean", nullable: false),
                    isDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    isDefault = table.Column<bool>(type: "boolean", nullable: false),
                    createdBy = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                    table.ForeignKey(
                        name: "FK_Users_Users_createdBy",
                        column: x => x.createdBy,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_createdBy",
                table: "Users",
                column: "createdBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
