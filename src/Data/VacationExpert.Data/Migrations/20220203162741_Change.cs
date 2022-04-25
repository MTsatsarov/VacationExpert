namespace VacationExpert.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Change : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Properties_ContactId",
                table: "Properties");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Contacts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: string.Empty,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactId",
                table: "Properties",
                column: "ContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Properties_PropertyId",
                table: "Contacts",
                column: "PropertyId",
                principalTable: "Properties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Properties_PropertyId",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Properties_ContactId",
                table: "Properties");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_PropertyId",
                table: "Contacts");

            migrationBuilder.AlterColumn<string>(
                name: "PropertyId",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.CreateIndex(
                name: "IX_Properties_ContactId",
                table: "Properties",
                column: "ContactId",
                unique: true);
        }
    }
}
