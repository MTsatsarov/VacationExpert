namespace VacationExpert.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class RemoveRequredForProperty1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Properties_PropertyId",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Properties_PropertyId",
                table: "Contacts");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_Properties_PropertyId",
                table: "Facilities");

            migrationBuilder.DropIndex(
                name: "IX_Properties_AddressId",
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

            migrationBuilder.DropIndex(
                name: "IX_Addresses_PropertyId",
                table: "Addresses");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Facilities",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Addresses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AddressId",
                table: "Properties",
                column: "AddressId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactId",
                table: "Properties",
                column: "ContactId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_FacilityId",
                table: "Properties",
                column: "FacilityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_AddressId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ContactId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Properties_FacilityId",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Facilities",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_AddressId",
                table: "Properties",
                column: "AddressId");

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

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_PropertyId",
                table: "Addresses",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Properties_PropertyId",
                table: "Addresses",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Properties_PropertyId",
                table: "Contacts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_Properties_PropertyId",
                table: "Facilities",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
