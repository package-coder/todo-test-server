using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace todo_test_server.Migrations
{
    /// <inheritdoc />
    public partial class addedtagmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tag",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagTodo",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false),
                    TodoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagTodo", x => new { x.TagId, x.TodoId });
                    table.ForeignKey(
                        name: "FK_TagTodo_Tag_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Tag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TagTodo_Todos_TagId",
                        column: x => x.TagId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagTodo_TodoId",
                table: "TagTodo",
                column: "TodoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagTodo");

            migrationBuilder.DropTable(
                name: "Tag");
        }
    }
}
