using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Magnum_Managment_and_Shop.Migrations
{
    /// <inheritdoc />
    public partial class AddingImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Members",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Members");
        }
    }
}
