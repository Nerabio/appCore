using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addSKtoTask : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SectionKeyId",
                table: "Tasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_SectionKeyId",
                table: "Tasks",
                column: "SectionKeyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_SectionKeys_SectionKeyId",
                table: "Tasks",
                column: "SectionKeyId",
                principalTable: "SectionKeys",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_SectionKeys_SectionKeyId",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_SectionKeyId",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "SectionKeyId",
                table: "Tasks");
        }
    }
}
