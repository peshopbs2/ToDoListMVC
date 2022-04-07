using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoListMVC.DAL.Migrations
{
    public partial class ToDoItemsAssigns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_ToDoLists_ToDoListId",
                table: "ToDoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem");

            migrationBuilder.RenameTable(
                name: "ToDoItem",
                newName: "ToDoItems");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItem_ToDoListId",
                table: "ToDoItems",
                newName: "IX_ToDoItems_ToDoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ToDoItemAssign",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToDoItemId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDoItemAssign", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToDoItemAssign_ToDoItems_ToDoItemId",
                        column: x => x.ToDoItemId,
                        principalTable: "ToDoItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItemAssign_ToDoItemId",
                table: "ToDoItemAssign",
                column: "ToDoItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItems_ToDoLists_ToDoListId",
                table: "ToDoItems");

            migrationBuilder.DropTable(
                name: "ToDoItemAssign");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToDoItems",
                table: "ToDoItems");

            migrationBuilder.RenameTable(
                name: "ToDoItems",
                newName: "ToDoItem");

            migrationBuilder.RenameIndex(
                name: "IX_ToDoItems_ToDoListId",
                table: "ToDoItem",
                newName: "IX_ToDoItem_ToDoListId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToDoItem",
                table: "ToDoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_ToDoLists_ToDoListId",
                table: "ToDoItem",
                column: "ToDoListId",
                principalTable: "ToDoLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
