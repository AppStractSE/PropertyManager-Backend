using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.EFCore.Migrations
{
    /// <inheritdoc />
    public partial class ReferencePropToChoreEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reference",
                table: "Chores",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reference",
                table: "Chores");
        }
    }
}
