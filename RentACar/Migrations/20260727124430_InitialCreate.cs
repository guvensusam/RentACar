using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Marka",
                columns: table => new
                {
                    MarkaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarkaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marka", x => x.MarkaId);
                });

            migrationBuilder.CreateTable(
                name: "Vites",
                columns: table => new
                {
                    VitesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VitesTuru = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vites", x => x.VitesId);
                });

            migrationBuilder.CreateTable(
                name: "Yakit",
                columns: table => new
                {
                    YakitId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YakitAdi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yakit", x => x.YakitId);
                });

            migrationBuilder.CreateTable(
                name: "CarModeli",
                columns: table => new
                {
                    ModelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    markaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarModeli", x => x.ModelId);
                    table.ForeignKey(
                        name: "FK_CarModeli_Marka_markaId",
                        column: x => x.markaId,
                        principalTable: "Marka",
                        principalColumn: "MarkaId");
                });

            migrationBuilder.CreateTable(
                name: "Arabalar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ArabaYasi = table.Column<int>(type: "int", nullable: false),
                    ArabaAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ArabaFiyat = table.Column<double>(type: "float", nullable: false),
                    ToplamKm = table.Column<float>(type: "real", nullable: false),
                    MarkaId = table.Column<int>(type: "int", nullable: false),
                    YakitId = table.Column<int>(type: "int", nullable: false),
                    VitesId = table.Column<int>(type: "int", nullable: false),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Arabalar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Arabalar_CarModeli_ModelId",
                        column: x => x.ModelId,
                        principalTable: "CarModeli",
                        principalColumn: "ModelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arabalar_Marka_MarkaId",
                        column: x => x.MarkaId,
                        principalTable: "Marka",
                        principalColumn: "MarkaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arabalar_Vites_VitesId",
                        column: x => x.VitesId,
                        principalTable: "Vites",
                        principalColumn: "VitesId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Arabalar_Yakit_YakitId",
                        column: x => x.YakitId,
                        principalTable: "Yakit",
                        principalColumn: "YakitId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Arabalar_MarkaId",
                table: "Arabalar",
                column: "MarkaId");

            migrationBuilder.CreateIndex(
                name: "IX_Arabalar_ModelId",
                table: "Arabalar",
                column: "ModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Arabalar_VitesId",
                table: "Arabalar",
                column: "VitesId");

            migrationBuilder.CreateIndex(
                name: "IX_Arabalar_YakitId",
                table: "Arabalar",
                column: "YakitId");

            migrationBuilder.CreateIndex(
                name: "IX_CarModeli_markaId",
                table: "CarModeli",
                column: "markaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Arabalar");

            migrationBuilder.DropTable(
                name: "CarModeli");

            migrationBuilder.DropTable(
                name: "Vites");

            migrationBuilder.DropTable(
                name: "Yakit");

            migrationBuilder.DropTable(
                name: "Marka");
        }
    }
}
