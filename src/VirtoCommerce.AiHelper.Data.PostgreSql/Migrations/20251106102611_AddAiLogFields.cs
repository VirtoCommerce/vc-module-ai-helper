using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.AiHelper.Data.PostgreSql.Migrations
{
    /// <inheritdoc />
    public partial class AddAiLogFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ErrorText",
                table: "AiRequestLog",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "AiRequestLog",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequestDuration",
                table: "AiRequestLog",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ErrorText",
                table: "AiRequestLog");

            migrationBuilder.DropColumn(
                name: "IsSuccess",
                table: "AiRequestLog");

            migrationBuilder.DropColumn(
                name: "RequestDuration",
                table: "AiRequestLog");
        }
    }
}
