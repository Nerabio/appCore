using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class dropUniqIndexForKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(name: "IX_Keys_TypeKeyId", table: "Keys");
            migrationBuilder.DropIndex(name: "IX_Keys_TypeKeyValueId", table: "Keys");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_TypeKeyId",
                table: "Keys",
                column: "TypeKeyId",
                unique: false);

            migrationBuilder.CreateIndex(
                name: "IX_Keys_TypeKeyValueId",
                table: "Keys",
                column: "TypeKeyValueId",
                unique: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
