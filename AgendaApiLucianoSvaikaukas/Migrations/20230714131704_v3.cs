using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AgendaApiLucianoSvaikaukas.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactGroup_Contacts_ContactId",
                table: "ContactGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactGroup_Groups_GroupId",
                table: "ContactGroup");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "ContactGroup",
                newName: "GroupsId");

            migrationBuilder.RenameColumn(
                name: "ContactId",
                table: "ContactGroup",
                newName: "ContactsId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactGroup_GroupId",
                table: "ContactGroup",
                newName: "IX_ContactGroup_GroupsId");

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Natacion" });

            migrationBuilder.InsertData(
                table: "ContactGroup",
                columns: new[] { "ContactsId", "GroupsId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 3, 1 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactGroup_Contacts_ContactsId",
                table: "ContactGroup",
                column: "ContactsId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactGroup_Groups_GroupsId",
                table: "ContactGroup",
                column: "GroupsId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactGroup_Contacts_ContactsId",
                table: "ContactGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_ContactGroup_Groups_GroupsId",
                table: "ContactGroup");

            migrationBuilder.DeleteData(
                table: "ContactGroup",
                keyColumns: new[] { "ContactsId", "GroupsId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ContactGroup",
                keyColumns: new[] { "ContactsId", "GroupsId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "GroupsId",
                table: "ContactGroup",
                newName: "GroupId");

            migrationBuilder.RenameColumn(
                name: "ContactsId",
                table: "ContactGroup",
                newName: "ContactId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactGroup_GroupsId",
                table: "ContactGroup",
                newName: "IX_ContactGroup_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactGroup_Contacts_ContactId",
                table: "ContactGroup",
                column: "ContactId",
                principalTable: "Contacts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContactGroup_Groups_GroupId",
                table: "ContactGroup",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
