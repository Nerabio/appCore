using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addTableDeviceRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeviceRelations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceOutId = table.Column<int>(nullable: false),
                    KeyOutId = table.Column<int>(nullable: false),
                    DeviceInId = table.Column<int>(nullable: false),
                    KeyInId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceRelations_Devices_DeviceInId",
                        column: x => x.DeviceInId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeviceRelations_Devices_DeviceOutId",
                        column: x => x.DeviceOutId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeviceRelations_Keys_KeyInId",
                        column: x => x.KeyInId,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DeviceRelations_Keys_KeyOutId",
                        column: x => x.KeyOutId,
                        principalTable: "Keys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRelations_DeviceInId",
                table: "DeviceRelations",
                column: "DeviceInId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRelations_DeviceOutId",
                table: "DeviceRelations",
                column: "DeviceOutId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRelations_KeyInId",
                table: "DeviceRelations",
                column: "KeyInId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceRelations_KeyOutId",
                table: "DeviceRelations",
                column: "KeyOutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceRelations");
        }
    }
}
