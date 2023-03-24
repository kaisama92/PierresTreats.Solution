using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PierresTreats.Migrations
{
    public partial class AddTentativeFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Treats",
                keyColumn: "TreatName",
                keyValue: null,
                column: "TreatName",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "TreatName",
                table: "Treats",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "StockQuantity",
                table: "Treats",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTreats",
                columns: table => new
                {
                    UserTreatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TreatId = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTreats", x => x.UserTreatId);
                    table.ForeignKey(
                        name: "FK_UserTreats_Treats_TreatId",
                        column: x => x.TreatId,
                        principalTable: "Treats",
                        principalColumn: "TreatId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTreats_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "UserId");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UserTreats_TreatId",
                table: "UserTreats",
                column: "TreatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTreats_UserId",
                table: "UserTreats",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTreats");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropColumn(
                name: "StockQuantity",
                table: "Treats");

            migrationBuilder.AlterColumn<string>(
                name: "TreatName",
                table: "Treats",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
