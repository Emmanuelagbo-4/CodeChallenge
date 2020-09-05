using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeChallenge.Migrations
{
    public partial class TotalLikeCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TotalLikeCount",
                table: "Likes",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalLikeCount",
                table: "Likes");
        }
    }
}
