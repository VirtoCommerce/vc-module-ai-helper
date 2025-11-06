using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VirtoCommerce.AiHelper.Data.MySql.Migrations
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
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "IsSuccess",
                table: "AiRequestLog",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RequestDuration",
                table: "AiRequestLog",
                type: "int",
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
