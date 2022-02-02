using Microsoft.EntityFrameworkCore.Migrations;

namespace VacationExpert.Data.Migrations
{
    public partial class changeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Facilities_PropertyId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts");

            migrationBuilder.AlterColumn<int>(
                name: "ContactId",
                table: "Properties",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactId",
                table: "Properties",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_FacilityId",
                table: "Properties",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_PropertyId",
                table: "Facilities",
                column: "PropertyId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Contacts_ContactId",
                table: "Properties",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Facilities_FacilityId",
                table: "Properties",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Contacts_ContactId",
                table: "Properties");

            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Facilities_FacilityId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ContactId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_FacilityId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_PropertyId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "ContactId",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_PropertyId",
                table: "Facilities",
                column: "PropertyId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts",
                column: "PropertyId",
                unique: true);
        }
    }
}
