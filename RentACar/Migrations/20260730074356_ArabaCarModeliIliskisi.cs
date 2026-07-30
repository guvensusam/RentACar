using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class ArabaCarModeliIliskisi : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Marka_MarkaId",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Vites_VitesID",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Yakit_YakitID",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModeli_Marka_MarkaId",
                table: "CarModeli");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Yakit",
                table: "Yakit");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vites",
                table: "Vites");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Marka",
                table: "Marka");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModeli",
                table: "CarModeli");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arabalar",
                table: "Arabalar");

            migrationBuilder.RenameTable(
                name: "Yakit",
                newName: "Yakitlar");

            migrationBuilder.RenameTable(
                name: "Vites",
                newName: "Vitesler");

            migrationBuilder.RenameTable(
                name: "Marka",
                newName: "Markalar");

            migrationBuilder.RenameTable(
                name: "CarModeli",
                newName: "CarModelleri");

            migrationBuilder.RenameTable(
                name: "Arabalar",
                newName: "Arabalarr");

            migrationBuilder.RenameIndex(
                name: "IX_CarModeli_MarkaId",
                table: "CarModelleri",
                newName: "IX_CarModelleri_MarkaId");

            migrationBuilder.RenameColumn(
                name: "MarkaId",
                table: "Arabalarr",
                newName: "CarModeliID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_YakitID",
                table: "Arabalarr",
                newName: "IX_Arabalarr_YakitID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_VitesID",
                table: "Arabalarr",
                newName: "IX_Arabalarr_VitesID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_MarkaId",
                table: "Arabalarr",
                newName: "IX_Arabalarr_CarModeliID");

            migrationBuilder.AlterColumn<int>(
                name: "YakitID",
                table: "Arabalarr",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VitesID",
                table: "Arabalarr",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Yakitlar",
                table: "Yakitlar",
                column: "YakitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vitesler",
                table: "Vitesler",
                column: "VitesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Markalar",
                table: "Markalar",
                column: "MarkaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModelleri",
                table: "CarModelleri",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arabalarr",
                table: "Arabalarr",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalarr_CarModelleri_CarModeliID",
                table: "Arabalarr",
                column: "CarModeliID",
                principalTable: "CarModelleri",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalarr_Vitesler_VitesID",
                table: "Arabalarr",
                column: "VitesID",
                principalTable: "Vitesler",
                principalColumn: "VitesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalarr_Yakitlar_YakitID",
                table: "Arabalarr",
                column: "YakitID",
                principalTable: "Yakitlar",
                principalColumn: "YakitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModelleri_Markalar_MarkaId",
                table: "CarModelleri",
                column: "MarkaId",
                principalTable: "Markalar",
                principalColumn: "MarkaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arabalarr_CarModelleri_CarModeliID",
                table: "Arabalarr");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalarr_Vitesler_VitesID",
                table: "Arabalarr");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalarr_Yakitlar_YakitID",
                table: "Arabalarr");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModelleri_Markalar_MarkaId",
                table: "CarModelleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Yakitlar",
                table: "Yakitlar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Vitesler",
                table: "Vitesler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Markalar",
                table: "Markalar");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarModelleri",
                table: "CarModelleri");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Arabalarr",
                table: "Arabalarr");

            migrationBuilder.RenameTable(
                name: "Yakitlar",
                newName: "Yakit");

            migrationBuilder.RenameTable(
                name: "Vitesler",
                newName: "Vites");

            migrationBuilder.RenameTable(
                name: "Markalar",
                newName: "Marka");

            migrationBuilder.RenameTable(
                name: "CarModelleri",
                newName: "CarModeli");

            migrationBuilder.RenameTable(
                name: "Arabalarr",
                newName: "Arabalar");

            migrationBuilder.RenameIndex(
                name: "IX_CarModelleri_MarkaId",
                table: "CarModeli",
                newName: "IX_CarModeli_MarkaId");

            migrationBuilder.RenameColumn(
                name: "CarModeliID",
                table: "Arabalar",
                newName: "MarkaId");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalarr_YakitID",
                table: "Arabalar",
                newName: "IX_Arabalar_YakitID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalarr_VitesID",
                table: "Arabalar",
                newName: "IX_Arabalar_VitesID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalarr_CarModeliID",
                table: "Arabalar",
                newName: "IX_Arabalar_MarkaId");

            migrationBuilder.AlterColumn<int>(
                name: "YakitID",
                table: "Arabalar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "VitesID",
                table: "Arabalar",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Yakit",
                table: "Yakit",
                column: "YakitId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Vites",
                table: "Vites",
                column: "VitesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Marka",
                table: "Marka",
                column: "MarkaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarModeli",
                table: "CarModeli",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Arabalar",
                table: "Arabalar",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_Marka_MarkaId",
                table: "Arabalar",
                column: "MarkaId",
                principalTable: "Marka",
                principalColumn: "MarkaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_Vites_VitesID",
                table: "Arabalar",
                column: "VitesID",
                principalTable: "Vites",
                principalColumn: "VitesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_Yakit_YakitID",
                table: "Arabalar",
                column: "YakitID",
                principalTable: "Yakit",
                principalColumn: "YakitId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarModeli_Marka_MarkaId",
                table: "CarModeli",
                column: "MarkaId",
                principalTable: "Marka",
                principalColumn: "MarkaId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
