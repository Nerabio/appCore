using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsActive = table.Column<bool>(nullable: false),
                    IsConnected = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeviceType",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeKeys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeKeyValues",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeKeyValues", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SectionKeys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeviceId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionKeys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SectionKeys_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionKeyId = table.Column<int>(nullable: false),
                    TypeKeyValueId = table.Column<int>(nullable: false),
                    TypeKeyId = table.Column<int>(nullable: false),
                    ValueString = table.Column<string>(nullable: true),
                    ValueInteger = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Keys_SectionKeys_SectionKeyId",
                        column: x => x.SectionKeyId,
                        principalTable: "SectionKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keys_TypeKeys_TypeKeyId",
                        column: x => x.TypeKeyId,
                        principalTable: "TypeKeys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Keys_TypeKeyValues_TypeKeyValueId",
                        column: x => x.TypeKeyValueId,
                        principalTable: "TypeKeyValues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Keys_SectionKeyId",
                table: "Keys",
                column: "SectionKeyId");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_TypeKeyId",
                table: "Keys",
                column: "TypeKeyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Keys_TypeKeyValueId",
                table: "Keys",
                column: "TypeKeyValueId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SectionKeys_DeviceId",
                table: "SectionKeys",
                column: "DeviceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceType");

            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "SectionKeys");

            migrationBuilder.DropTable(
                name: "TypeKeys");

            migrationBuilder.DropTable(
                name: "TypeKeyValues");

            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
