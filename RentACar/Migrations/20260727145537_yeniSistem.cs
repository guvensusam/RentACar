using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentACar.Migrations
{
    /// <inheritdoc />
    public partial class yeniSistem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_CarModeli_ModelId",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Vites_VitesId",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Yakit_YakitId",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModeli_Marka_markaId",
                table: "CarModeli");

            migrationBuilder.DropIndex(
                name: "IX_Arabalar_ModelId",
                table: "Arabalar");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Arabalar");

            migrationBuilder.RenameColumn(
                name: "markaId",
                table: "CarModeli",
                newName: "MarkaId");

            migrationBuilder.RenameColumn(
                name: "ModelId",
                table: "CarModeli",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_CarModeli_markaId",
                table: "CarModeli",
                newName: "IX_CarModeli_MarkaId");

            migrationBuilder.RenameColumn(
                name: "YakitId",
                table: "Arabalar",
                newName: "YakitID");

            migrationBuilder.RenameColumn(
                name: "VitesId",
                table: "Arabalar",
                newName: "VitesID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_YakitId",
                table: "Arabalar",
                newName: "IX_Arabalar_YakitID");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_VitesId",
                table: "Arabalar",
                newName: "IX_Arabalar_VitesID");

            migrationBuilder.AlterColumn<int>(
                name: "MarkaId",
                table: "CarModeli",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Vites_VitesID",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_Arabalar_Yakit_YakitID",
                table: "Arabalar");

            migrationBuilder.DropForeignKey(
                name: "FK_CarModeli_Marka_MarkaId",
                table: "CarModeli");

            migrationBuilder.RenameColumn(
                name: "MarkaId",
                table: "CarModeli",
                newName: "markaId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "CarModeli",
                newName: "ModelId");

            migrationBuilder.RenameIndex(
                name: "IX_CarModeli_MarkaId",
                table: "CarModeli",
                newName: "IX_CarModeli_markaId");

            migrationBuilder.RenameColumn(
                name: "YakitID",
                table: "Arabalar",
                newName: "YakitId");

            migrationBuilder.RenameColumn(
                name: "VitesID",
                table: "Arabalar",
                newName: "VitesId");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_YakitID",
                table: "Arabalar",
                newName: "IX_Arabalar_YakitId");

            migrationBuilder.RenameIndex(
                name: "IX_Arabalar_VitesID",
                table: "Arabalar",
                newName: "IX_Arabalar_VitesId");

            migrationBuilder.AlterColumn<int>(
                name: "markaId",
                table: "CarModeli",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "YakitId",
                table: "Arabalar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "VitesId",
                table: "Arabalar",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Arabalar",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Arabalar_ModelId",
                table: "Arabalar",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_CarModeli_ModelId",
                table: "Arabalar",
                column: "ModelId",
                principalTable: "CarModeli",
                principalColumn: "ModelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_Vites_VitesId",
                table: "Arabalar",
                column: "VitesId",
                principalTable: "Vites",
                principalColumn: "VitesId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Arabalar_Yakit_YakitId",
                table: "Arabalar",
                column: "YakitId",
                principalTable: "Yakit",
                principalColumn: "YakitId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CarModeli_Marka_markaId",
                table: "CarModeli",
                column: "markaId",
                principalTable: "Marka",
                principalColumn: "MarkaId");
        }
    }
}
