using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Relationships.Migrations
{
    /// <inheritdoc />
    public partial class UserCharacter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Character_USers_UserId",
                table: "Character");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Character",
                table: "Character");

            migrationBuilder.RenameTable(
                name: "Character",
                newName: "Characters");

            migrationBuilder.RenameIndex(
                name: "IX_Character_UserId",
                table: "Characters",
                newName: "IX_Characters_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Characters",
                table: "Characters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Characters_USers_UserId",
                table: "Characters",
                column: "UserId",
                principalTable: "USers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characters_USers_UserId",
                table: "Characters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Characters",
                table: "Characters");

            migrationBuilder.RenameTable(
                name: "Characters",
                newName: "Character");

            migrationBuilder.RenameIndex(
                name: "IX_Characters_UserId",
                table: "Character",
                newName: "IX_Character_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Character",
                table: "Character",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Character_USers_UserId",
                table: "Character",
                column: "UserId",
                principalTable: "USers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
