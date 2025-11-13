using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.AiHelper.Data.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class AddAiLogEntityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EntityType",
                table: "AiRequestLog",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EntityType",
                table: "AiRequestLog");
        }
    }
}
